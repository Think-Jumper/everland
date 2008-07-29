using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using eland.model;
using NHibernate.Criterion;

namespace eland.api
{
   public class UserRepository : Repository<User>
   {
      public User FindByOpenId(String openId)
      {
         User user = base.Session.CreateCriteria(typeof(User))
            .Add(Expression.Eq("OpenId", openId)).UniqueResult<User>();

         return user;
      }

      public bool Exists(String openId)
      {
         return base.Exists(openId, "OpenId");
      }
   }
}
