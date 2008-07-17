using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Criterion;

namespace eland.api
{
   public class Repository<T> : IRepository<T> where T : class
   {
      protected virtual ISession Session
      {
         get { return NHibHelper.GetCurrentSession(); }
      }

      public T Get(object id)
      {
         return Session.Get<T>(id) as T;
      }

      public IList GetAll() 
      { 
         return GetByCriteria(); 
      }   
      
      protected IList GetByCriteria(params ICriterion[] criterion) 
      { 
         ICriteria criteria = Session.CreateCriteria(typeof(T)); 
         
         foreach (ICriterion criterium in criterion) { 
            criteria.Add(criterium); 
         } 
         
         return criteria.List(); 
      }

      public T Save(T entity)
      {
         using (ITransaction tran = Session.BeginTransaction())
         {
            Session.Save(entity);
            tran.Commit();
         }
   
         return entity;
      }

      public void Delete(T entity)
      {
         Session.Delete(entity);
      }
   }
}
