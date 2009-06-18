using System.Web.Mvc;

namespace eland.Controllers
{
   public class HomeController : Controller
   {
      public ActionResult Index()
      {
         ViewData["Title"] = "Home Page";
         ViewData["Message"] = "Test";

         return View();
      }

      public ActionResult About()
      {
         ViewData["Title"] = "About Page";

         return View();
      }
   }
}
