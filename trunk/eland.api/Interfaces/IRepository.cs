using System;
using System.Collections;
using NHibernate;
using NHibernate.Criterion;

namespace eland.api.Interfaces
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
        void Delete(object id);
        ISession Session { get; }
    }
}