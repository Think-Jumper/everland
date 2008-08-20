using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eland.model;
using NHibernate.Criterion;

namespace eland.api
{
   public class GameSessionRepository : Repository<GameSession>
   {
      public GameSession FindByUser(User user)
      {
         GameSession gameSession = base.Session.CreateCriteria(typeof(GameSession))
              .Add(Expression.Eq("User", user)).UniqueResult<GameSession>();

         return gameSession;
      }

      public GameSession FindByUserId(String openId)
      {
         return base.Session.CreateCriteria(typeof(GameSession))
          .CreateCriteria("User").Add(Expression.Eq("OpenId", openId))
          .UniqueResult<GameSession>();
      }

   }
}
