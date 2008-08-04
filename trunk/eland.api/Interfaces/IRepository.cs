using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NHibernate.Criterion;

namespace eland.api
{
   public interface IRepository <T>
   {
      bool Exists(object id);
      bool Exists(object id, String ColumnName);
      T Get(object id);
      IList FindAll();
      IList FindByCriteria(params ICriterion[] criterion);
      T Save(T entity);
      void Delete(T entity);
     

   }
}
