using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace eland.api
{
    public interface IRepository <T>
    {
        bool Exists(object id);
        bool Exists(object id, String ColumnName);
        T Get(object id);
        IList<T> FindAll();
        IList<T> FindByCriteria(params ICriterion[] criterion);
        T Save(T entity);
        void Delete(T entity);
        void Delete(object id);
        ISession Session { get; }
    }
}