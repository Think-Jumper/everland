using System;
using eland.model;
using NHibernate.Criterion;

namespace eland.api
{
   public class GameSessionRepository : Repository<GameSession>
   {
      public GameSession FindByUser(User user)
      {
         var gameSession = base.Session.CreateCriteria(typeof(GameSession))
              .Add(Restrictions.Eq("User", user)).UniqueResult<GameSession>();

         return gameSession;
      }

      public GameSession FindByUserId(String openId)
      {
         return base.Session.CreateCriteria(typeof(GameSession))
          .CreateCriteria("User").Add(Restrictions.Eq("OpenId", openId))
          .UniqueResult<GameSession>();
      }

   }
}