using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace eland.api
{
   public class Repository<T> : IRepository<T>, IDisposable where T : class
   {
      private ISession _session;

      public Repository()
      {
         _session = NHibHelper.GetCurrentSession();      
      }

      protected virtual ISession Session
      {
         get { return _session; }
      }

      public bool Exists(object id)
      {
         return this.Exists(id, "Id");
      }

      public bool Exists(object id, String ColumnName)
      {
         ICriteria criteria = Session.CreateCriteria(typeof(T));
         criteria.SetProjection(Projections.RowCount()).Add(Expression.Eq(ColumnName, id));

         return 0 != Convert.ToInt32(criteria.UniqueResult());
      }

      public T Get(object id)
      {
         return Session.Get<T>(id) as T;
      }

      public IList FindAll() 
      { 
         return FindByCriteria(); 
      }   
      
      public IList FindByCriteria(params ICriterion[] criterion) 
      { 
         ICriteria criteria = Session.CreateCriteria(typeof(T)); 
         
         foreach (ICriterion criterium in criterion) { 
            criteria.Add(criterium); 
         } 
         
         return criteria.List(); 
      }

      //TODO: check is Session.Transaction.IsActive 
      public T Save(T entity)
      {
         using (ITransaction tran = Session.BeginTransaction())
         {
            _session.Save(entity);
            tran.Commit();
         }
   
         return entity;
      }

      public void Delete(T entity)
      {
         using (ITransaction tran = Session.BeginTransaction())
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
