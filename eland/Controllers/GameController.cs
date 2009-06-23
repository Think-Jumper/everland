using System.Web.Mvc;
using eland.api.Services;
using eland.ViewData;
using eland.api;

namespace eland.Controllers
{
    public class GameController : BaseController
    {
        public ActionResult Index()
        {
            var gameIndexData = new GameIndexData
                                              {
                                                  GameSessionData =
                                                      ((GameSessionRepository)DataContext.GameSessionRepository).
                                                      FindByUserId(HttpContext.User.Identity.Name)
                                              };

            return View("Index", gameIndexData);
        }

        public ActionResult CreateUnit()
        {
            TempData["CreatedUnit"] = UnitService.Create();

            return RedirectToAction("ViewUser", "User");
        }


    }
}
