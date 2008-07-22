using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Criterion;

namespace eland.api
{
   public class Repository<T> : IRepository<T>, IDisposable where T : class
   {
      private ISession _session;

      public Repository()
      {
         _session = NHibHelper.GetCurrentSession();      
      }

      //protected virtual ISession Session
      //{
      //   get { return NHibHelper.GetCurrentSession(); }
      //}

      public T Get(object id)
      {
         return _session.Get<T>(id) as T;
      }

      public IList GetAll() 
      { 
         return GetByCriteria(); 
      }   
      
      protected IList GetByCriteria(params ICriterion[] criterion) 
      { 
         ICriteria criteria = _session.CreateCriteria(typeof(T)); 
         
         foreach (ICriterion criterium in criterion) { 
            criteria.Add(criterium); 
         } 
         
         return criteria.List(); 
      }

      public T Save(T entity)
      {
         using (ITransaction tran = _session.BeginTransaction())
         {
            _session.Save(entity);
            tran.Commit();
         }
   
         return entity;
      }

      public void Delete(T entity)
      {
         using (ITransaction tran = _session.BeginTransaction())
         {
            _session.Delete(entity);
            tran.Commit();
         }
      }

      #region IDisposable Members

      public void Dispose()
      {
         if (_session != null) {
            _session.Close();
         }
      }

      #endregion
   }
}
