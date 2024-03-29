﻿using System;
using System.Collections;
using System.Collections.Generic;

using NHibernate;
using NHibernate.Criterion;

using eland.api.Helpers;

namespace eland.api
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        private readonly ISession _session;

        public Repository()
        {
            _session = NHibHelper.GetCurrentSession();
        }

        public virtual ISession Session
        {
            get { return _session; }
        }

        public bool Exists(object id)
        {
            return Exists(id, "Id");
        }

        public bool Exists(object id, String ColumnName)
        {
            var criteria = Session.CreateCriteria(typeof(T));
            criteria.SetProjection(Projections.RowCount()).Add(Restrictions.Eq(ColumnName, id));

            return 0 != Convert.ToInt32(criteria.UniqueResult());
        }

        public T Load(object id)
        {
            return Session.Load<T>(id);
        }

        public T Get(object id)
        {
            return Session.Get<T>(id);
        }

        public IList<T> FindAll()
        {
            return FindByCriteria();
        }

        public IList<T> FindByCriteria(params ICriterion[] criterion)
        {
            var criteria = Session.CreateCriteria(typeof(T));

            foreach (var criterium in criterion)
            {
                criteria.Add(criterium);
            }

            return criteria.List<T>();
        }

        public List<T> GetByExample(T exampleInstance, params string[] propertiesToExclude)
        {
            var criteria = Session.CreateCriteria(typeof(T));
            var example = Example.Create(exampleInstance);

            foreach (var propertyToExclude in propertiesToExclude)
                example.ExcludeProperty(propertyToExclude);

            criteria.Add(example);

            return criteria.List<T>() as List<T>;
        }

        public T GetUniqueByExample(T exampleInstance, params string[] propertiesToExclude)
        {
            var foundList = GetByExample(exampleInstance, propertiesToExclude);

            if (foundList.Count > 1)
                throw new NonUniqueResultException(foundList.Count);

            return foundList.Count > 0 ? foundList[0] : default(T);
        }

        public T Save(T entity)
        {
            if (!Session.Transaction.IsActive)
            {
                throw new InvalidOperationException("Must be within a transaction to call this method.");
            }

            _session.Save(entity);

            return entity;
        }

        public void Evict(T entity)
        {
            Session.Evict(entity);
        }

        public void Delete(T entity)
        {
            if (!Session.Transaction.IsActive)
            {
                throw new InvalidOperationException("Must be within a transaction to call this method.");
            }

            _session.Delete(entity);
        }

        public void Delete(object id)
        {
            if (!Session.Transaction.IsActive)
            {
                throw new InvalidOperationException("Must be within a transaction to call this method.");
            }

            _session.Delete(_session.Get<T>(id));
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_session != null)
            {
                _session.Close();
            }
        }

        #endregion
    }
}
