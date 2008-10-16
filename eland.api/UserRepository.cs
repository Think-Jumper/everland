using System;

using eland.model;
using NHibernate.Criterion;

namespace eland.api
{
   public class UserRepository : Repository<User>
   {
      public User FindByOpenId(String openId)
      {
         var user = base.Session.CreateCriteria(typeof(User))
            .Add(Restrictions.Eq("OpenId", openId)).UniqueResult<User>();

         return user;
      }

      public bool Exists(String openId)
      {
         return Exists(openId, "OpenId");
      }
   }
}
