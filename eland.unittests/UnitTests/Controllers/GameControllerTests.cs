using System.Web.Mvc;
using System.Web.Routing;
using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using eland.Controllers;
using eland.model;
using eland.unittests.Helpers;
using eland.ViewData;
using MbUnit.Framework;

namespace eland.unittests.UnitTests.Controllers
{
   [TestFixture]
   public class GameControllerTests
   {
      private GameController gameController;
      private readonly WindsorContainer container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));

      [TestFixtureSetUp]
      public void Setup_Tests()
      {
         gameController = container.Resolve<GameController>();
      }

      [Test]
      public void Index_Correct_View()
      {
         var mockedhttpContext = TestDataHelper.SetupHttpContextMocks(true, TestDataHelper.OPEN_ID);
         gameController.ControllerContext = new ControllerContext(mockedhttpContext, new RouteData(), gameController);

         var res = gameController.Index() as ViewResult;

         Assert.AreEqual("Index", res.ViewName);
      }

      [Test]
      public void Index_Correct_Data()
      {
         var mockedhttpContext = TestDataHelper.SetupHttpContextMocks(true, TestDataHelper.OPEN_ID);
         gameController.ControllerContext = new ControllerContext(mockedhttpContext, new RouteData(), gameController);

         var res = gameController.Index() as ViewResult;

         Assert.AreEqual(typeof(GameIndexData), res.ViewData.Model.GetType());
         Assert.AreEqual(typeof(GameSession), ((GameIndexData) res.ViewData.Model).GameSessionData.GetType());
      }

      
   }
}
