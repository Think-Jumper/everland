using System;
using System.Web.Mvc;

using eland.Controllers;

using MbUnit.Framework;
using Rhino.Mocks;
using System.Web;
using System.Collections.Specialized;
using System.Web.Routing;
using System.Security.Principal;

namespace eland.unittests.UnitTests.Controllers
{
   [TestFixture]
   public class UserControllerTests
   {
      private const string OPEN_ID = "http://jamie.shortbet.org";
      private UserController userController;

      [TestFixtureSetUp]
      public void Setup_Tests()
      {
         userController = new UserController();
      }

      [Test]
      public void Index_Correct_View()
      {
         var mocks = new MockRepository();
         var mockedhttpContext = mocks.DynamicMock<HttpContextBase>();
         var mockedUser = mocks.DynamicMock<IPrincipal>();
         var mockedIdentity = mocks.DynamicMock<IIdentity>();

         SetupResult.For(mockedhttpContext.User).Return(mockedUser);
         SetupResult.For(mockedUser.Identity).Return(mockedIdentity);
         SetupResult.For(mockedIdentity.IsAuthenticated).Return(true);

         userController.ControllerContext = new ControllerContext(mockedhttpContext, new RouteData(), userController);

         mocks.ReplayAll();
         var result = userController.Index() as ViewResult;
         mocks.VerifyAll();

         Assert.AreEqual("Index", result.ViewName);
      }

      [Test]
      public void New_Correct_View()
      {
         var result = userController.New(OPEN_ID) as ViewResult;
         Assert.AreEqual("ViewUser", result.ViewName);
      }

      [Test]
      public void Edit_Correct_View()
      {
         var result = userController.Edit(OPEN_ID) as ViewResult;
         Assert.AreEqual("Edit", result.ViewName);
      }

      [Test]
      public void ViewUser_Correct_View()
      {
         var result = userController.ViewUser(OPEN_ID) as ViewResult;
         Assert.AreEqual("ViewUser", result.ViewName);
      }

   }
}
