using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using NHibernate.Cfg;
using NHibernate;

namespace eland.api
{
   public sealed class NHibHelper
   {
      private const string CurrentSessionKey = "nhibernate.current_session";
      private static readonly ISessionFactory sessionFactory;

      static NHibHelper()
      {
         sessionFactory = new Configuration().Configure().BuildSessionFactory();
      }

      public static ISession GetCurrentSession()
      {
         ISession currentSession = HttpContext.Current.Items[CurrentSessionKey] as ISession;

         if (currentSession == null)
         {
            currentSession = sessionFactory.OpenSession();
            HttpContext.Current.Items[CurrentSessionKey] = currentSession;
         }
         return currentSession;
      }

      public static void CloseSession()
      {
         ISession currentSession = HttpContext.Current.Items[CurrentSessionKey] as ISession;

         if (currentSession == null)
         {
            // No current session
            return;
         }
         currentSession.Close();
         HttpContext.Current.Items.Remove(CurrentSessionKey);
      }

      public static void CloseSessionFactory()
      {
         if (sessionFactory != null)
         {
            sessionFactory.Close();
         }
      }
   }
}