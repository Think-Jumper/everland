using System.Web.Mvc;
using System.Web.Routing;
using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using eland.Controllers;
using eland.model;
using eland.ViewData;
using MbUnit.Framework;
using Rhino.Mocks;

namespace eland.unittests.UnitTests.Controllers
{
   [TestFixture]
   public class GameControllerTests
   {
      private GameController gameController;
      private readonly WindsorContainer container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
      private MockRepository mocks;

      [TestFixtureSetUp]
      public void Setup_Tests()
      {
         mocks = new MockRepository();
         gameController = container.Resolve<GameController>();
      }

      [Test]
      public void Index_Correct_View()
      {
         var mockedhttpContext = TestDataHelper.SetupHttpContextMocks(mocks, true, TestDataHelper.OPEN_ID);
         gameController.ControllerContext = new ControllerContext(mockedhttpContext, new RouteData(), gameController);

         mocks.ReplayAll();
         var res = gameController.Index() as ViewResult;
         mocks.VerifyAll();

         Assert.AreEqual("Index", res.ViewName);
      }

      [Test]
      public void Index_Correct_Data()
      {
         var mockedhttpContext = TestDataHelper.SetupHttpContextMocks(mocks, true, TestDataHelper.OPEN_ID);
         gameController.ControllerContext = new ControllerContext(mockedhttpContext, new RouteData(), gameController);

         mocks.ReplayAll();
         var res = gameController.Index() as ViewResult;
         mocks.VerifyAll();

         Assert.AreEqual(typeof(GameIndexData), res.ViewData.Model.GetType());
         Assert.AreEqual(typeof(GameSession), ((GameIndexData) res.ViewData.Model).GameSessionData.GetType());
      }

      
   }
}
