using System.Web.Mvc;
using eland.api;
using eland.api.Services;
using eland.Filters;
using eland.ViewData;

namespace eland.Controllers
{
    [Authentication]
    public class GameController : BaseController
    {
        [UserName]
        public ActionResult Index(string userName)
        {
            var gameIndexData = new GameIndexData
                                    {
                                        GameSessionData = ((GameSessionRepository) DataContext.GameSessionRepository).FindByUserId(userName)
                                    };

            return gameIndexData.GameSessionData == null ? New() : View(gameIndexData);
        }

        public ActionResult New()
        {
            return View();
        }


        [UserName]
        public ActionResult Create(string userName)
        {
            var user = ((UserRepository) DataContext.UserRepository).FindByOpenId(userName);
            var gameSession = GameService.CreateSession(user);

            using (var tran = DataContext.WorldRepository.Session.BeginTransaction())
            {
                DataContext.WorldRepository.Save(gameSession.Game.GameWorld);
                DataContext.RaceRepository.Save(gameSession.Nation.Race);
                DataContext.GameSessionRepository.Save(gameSession);
                tran.Commit();
            }

            return RedirectToAction("Index");
        }

        public ActionResult CreateUnit()
        {
            TempData["CreatedUnit"] = UnitService.Create();
            return RedirectToAction("ViewUser", "Users");
        }


    }
}
