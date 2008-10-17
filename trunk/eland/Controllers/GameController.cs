using System.Web;
using System.Web.Mvc;
using eland.ViewData;
using eland.api.Interfaces;
using eland.api;

namespace eland.Controllers
{
   public class GameController : Controller
   {
      public IDataContext DataContext { get; set; }

      public ActionResult Index()
      {
         var gameIndexData = new GameIndexData
                                           {
                                               GameSessionData =
                                                   ((GameSessionRepository) DataContext.GameSessionRepository).
                                                   FindByUserId(HttpContext.User.Identity.Name)
                                           };

          return View("Index", gameIndexData);
      }
   }
}
