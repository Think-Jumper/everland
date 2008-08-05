using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using eland.Controllers;
using MbUnit.Framework;
using Rhino.Mocks;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Core.Resource;

namespace eland.unittests.UnitTests.Controllers
{
   [TestFixture]
   public class UserControllerTests
   {
      private const string OPEN_ID = "http://jamie.shortbet.org/";
      private UserController userController;
      private MockRepository mocks;
      private WindsorContainer container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));

      [TestFixtureSetUp]
      public void Setup_Tests()
      {
         mocks = new MockRepository();
         userController = container.Resolve<UserController>();
      }

      private void SetupMocks(out HttpContextBase mockedhttpContext, bool isAuthenticated, string IdentityName)
      {
         mockedhttpContext = mocks.DynamicMock<HttpContextBase>();
         var mockedUser = mocks.DynamicMock<IPrincipal>();
         var mockedIdentity = mocks.DynamicMock<IIdentity>();

         SetupResult.For(mockedhttpContext.User).Return(mockedUser);
         SetupResult.For(mockedUser.Identity).Return(mockedIdentity);
         SetupResult.For(mockedIdentity.IsAuthenticated).Return(isAuthenticated);
         SetupResult.For(mockedIdentity.Name).Return(IdentityName);

      }

      [Test]
      public void Index_Correct_View_New_User()
      {
         HttpContextBase mockedhttpContext;
         this.SetupMocks(out mockedhttpContext, true, OPEN_ID);
         userController.ControllerContext = new ControllerContext(mockedhttpContext, new RouteData(), userController);

         mocks.ReplayAll();
         var result = userController.Index() as ViewResult;
         mocks.VerifyAll();

         Assert.AreEqual("New", result.ViewName);
      }

      [Test]
      public void Index_Correct_View_Not_Authenticated()
      {
         HttpContextBase mockedhttpContext;
         this.SetupMocks(out mockedhttpContext, false, string.Empty);
         userController.ControllerContext = new ControllerContext(mockedhttpContext, new RouteData(), userController);

         mocks.ReplayAll();
         var result = userController.Index() as RedirectToRouteResult;
         mocks.VerifyAll();

         Assert.AreEqual("Home", result.Values["Controller"]);
         Assert.AreEqual("Index", result.Values["Action"]);
      }
      
      [Test]
      public void New_Correct_View()
      {
         var result = userController.New(OPEN_ID) as ViewResult;
         Assert.AreEqual("New", result.ViewName);
      }

      [Test]
      public void Create_Correct_View()
      {
         //var result = userController.Create() as ViewResult;
         //Assert.AreEqual("ViewUser", result.ViewName);
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
