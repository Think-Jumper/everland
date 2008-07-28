using MbUnit.Framework;

using eland.Controllers;

using System.Web.Mvc;

namespace eland.unittests.UnitTests
{
   [TestFixture]
   public class UserTests
   {
      [Test]
      public void Login_Correct_View()
      {
         UserController userController = new UserController();
         var result = userController.Login() as ViewResult;

         Assert.AreEqual("Login", result.ViewName);        
      }

   }
}
