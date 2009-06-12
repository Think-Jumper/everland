using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using eland.Controllers;
using eland.unittests.Helpers;
using eland.ViewData;
using MbUnit.Framework;

namespace eland.unittests.UnitTests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController userController;
        private readonly WindsorContainer container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));

        [TestFixtureSetUp]
        public void Setup_Tests()
        {
            userController = container.Resolve<UserController>();
        }

        //private void SetupMocks(out HttpContextBase mockedhttpContext, bool isAuthenticated, string IdentityName)
        //{
        //   mockedhttpContext = mocks.DynamicMock<HttpContextBase>();
        //   var mockedUser = mocks.DynamicMock<IPrincipal>();
        //   var mockedIdentity = mocks.DynamicMock<IIdentity>();

        //   SetupResult.For(mockedhttpContext.User).Return(mockedUser);
        //   SetupResult.For(mockedUser.Identity).Return(mockedIdentity);
        //   SetupResult.For(mockedIdentity.IsAuthenticated).Return(isAuthenticated);
        //   SetupResult.For(mockedIdentity.Name).Return(IdentityName);

        //}

        [Test]
        public void Index_Correct_View_Existing_User()
        {
            HttpContextBase mockedhttpContext = TestDataHelper.SetupHttpContextMocks(true, TestDataHelper.OPEN_ID);
            userController.ControllerContext = new ControllerContext(mockedhttpContext, new RouteData(), userController);

            var result = userController.Index() as ViewResult;

            Assert.AreEqual("ViewUser", result.ViewName);
            Assert.AreEqual(TestDataHelper.OPEN_ID, ((ViewUserData)result.ViewData.Model).UserData.OpenId);
            Assert.AreEqual(TestDataHelper.EMAIL, ((ViewUserData)result.ViewData.Model).UserData.Email);
            Assert.AreEqual(TestDataHelper.FIRST_NAME, ((ViewUserData)result.ViewData.Model).UserData.FirstName);
            Assert.AreEqual(TestDataHelper.LAST_NAME, ((ViewUserData)result.ViewData.Model).UserData.LastName);
        }

        [Test]
        public void Index_Correct_View_Not_Authenticated()
        {
            HttpContextBase mockedhttpContext = TestDataHelper.SetupHttpContextMocks(false, string.Empty);
            userController.ControllerContext = new ControllerContext(mockedhttpContext, new RouteData(), userController);

            var result = userController.Index() as RedirectToRouteResult;

            Assert.AreEqual("Home", result.RouteValues["Controller"]);
            Assert.AreEqual( "Index", result.RouteValues[ "Action" ] );
        }

        [Test]
        public void New_Correct_View()
        {
            var result = userController.New(TestDataHelper.OPEN_ID) as ViewResult;
            Assert.AreEqual("New", result.ViewName);
        }

        [Test]
        public void Edit_Correct_View()
        {
            var result = userController.Edit(TestDataHelper.OPEN_ID) as ViewResult;
            Assert.AreEqual("Edit", result.ViewName);
        }

        [Test]
        public void ViewUser_Correct_View()
        {
            var result = userController.ViewUser(TestDataHelper.OPEN_ID) as ViewResult;
            Assert.AreEqual("ViewUser", result.ViewName);
        }

        [Test]
        public void ViewUser_Correct_Data()
        {
            var result = userController.ViewUser(TestDataHelper.OPEN_ID) as ViewResult;

            Assert.AreNotEqual(null, ((ViewUserData)result.ViewData.Model).UserData);
            Assert.AreNotEqual(null, ((ViewUserData)result.ViewData.Model).GameSessionData);

            Assert.AreEqual(TestDataHelper.OPEN_ID, ((ViewUserData)result.ViewData.Model).UserData.OpenId);
            Assert.AreEqual(TestDataHelper.EMAIL, ((ViewUserData)result.ViewData.Model).UserData.Email);
            Assert.AreEqual(TestDataHelper.FIRST_NAME, ((ViewUserData)result.ViewData.Model).UserData.FirstName);
            Assert.AreEqual(TestDataHelper.LAST_NAME, ((ViewUserData)result.ViewData.Model).UserData.LastName);
        }

        [Test]
        public void ViewUser_Non_Existent_User()
        {
            var result = userController.ViewUser(TestDataHelper.OPEN_ID + Guid.NewGuid().ToString()) as RedirectToRouteResult;

            Assert.AreEqual( "Home", result.RouteValues[ "Controller" ] );
            Assert.AreEqual( "Index", result.RouteValues[ "Action" ] );
        }

    }
}
