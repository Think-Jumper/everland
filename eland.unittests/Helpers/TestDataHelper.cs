using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Rhino.Mocks;
using System.Security.Principal;

namespace eland.unittests
{
   public class TestDataHelper
   {
      public const string OPEN_ID = "http://jamief00.myopenid.com/";
      public const string FIRST_NAME = "Jamie";
      public const string LAST_NAME = "Fraser";
      public const string EMAIL = "jamie.fraser@gmail.com";

      public static HttpContextBase SetupHttpContextMocks(MockRepository mocks, bool isAuthenticated, string IdentityName)
      {
         var mockedhttpContext = mocks.DynamicMock<HttpContextBase>();
         var mockedUser = mocks.DynamicMock<IPrincipal>();
         var mockedIdentity = mocks.DynamicMock<IIdentity>();

         SetupResult.For(mockedhttpContext.User).Return(mockedUser);
         SetupResult.For(mockedUser.Identity).Return(mockedIdentity);
         SetupResult.For(mockedIdentity.IsAuthenticated).Return(isAuthenticated);
         SetupResult.For(mockedIdentity.Name).Return(IdentityName);

         return mockedhttpContext;
      }

   }
}
