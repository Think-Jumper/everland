using MbUnit.Framework;

using eland.Controllers;

using System.Web.Mvc;

namespace eland.unittests.UnitTests
{
   [TestFixture]
   public class LoginControllerTests
   {
      [Test]
      public void Login_Correct_View()
      {
         var loginController = new LoginController();
         var result = loginController.Login() as ViewResult;

         Assert.AreEqual("Login", result.ViewName);        
      }

   }
}
