using NHibernate.Cfg;
using NHibernate;

namespace eland.api.Helpers
{
    public sealed class NHibHelper
    {
        private static readonly ISessionFactory sessionFactory;
        private static ISession currentSession;

        static NHibHelper()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();
        }

        public static ISession GetCurrentSession()
        {
            if (currentSession == null)
            {
                currentSession = sessionFactory.OpenSession();
            }

            return currentSession;
            //ISession currentSession = HttpContext.Current.Items[CurrentSessionKey] as ISession;

            //if (currentSession == null)
            //{
            //   currentSession = sessionFactory.OpenSession();
            //   HttpContext.Current.Items[CurrentSessionKey] = currentSession;
            //}
            //return currentSession;
        }

        public static void CloseSession()
        {
            if (currentSession != null)
            {
                currentSession.Close();
            }
            //ISession currentSession = HttpContext.Current.Items[CurrentSessionKey] as ISession;

            //if (currentSession == null)
            //{
            //   // No current session
            //   return;
            //}
            //currentSession.Close();
            //HttpContext.Current.Items.Remove(CurrentSessionKey);
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