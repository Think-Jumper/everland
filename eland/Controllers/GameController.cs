using System;
using System.Web.Mvc;
using eland.api;
using eland.api.Services;
using eland.Filters;
using eland.model;
using eland.ViewData;
using eland.ViewData.Game;

namespace eland.Controllers
{
    [Authentication]
    public class GameController : BaseController
    {
        
        public ActionResult Index()
        {
            var gameIndexData = new GameIndexData { GameSessionData = ((GameSessionRepository) DataContext.GameSessionRepository).FindByUserId(HttpContext.User.Identity.Name) };
            if (gameIndexData.GameSessionData == null)
                return RedirectToAction("New");

            return View(gameIndexData);
        }

        public ActionResult New()
        {
            var races = DataContext.RaceRepository.FindAll();
            var selectList = new SelectList(races, "Id", "Name");
            return View(selectList);
        }

        [UserName]
        public ActionResult Create(string userName, Guid raceId)
        {
            var user = ((UserRepository) DataContext.UserRepository).FindByOpenId(userName);
            var race = DataContext.RaceRepository.Get(raceId);
            var gameSession = GameService.CreateSession(user, race);

            using (var tran = DataContext.WorldRepository.Session.BeginTransaction())
            {
                DataContext.WorldRepository.Save(gameSession.Game.GameWorld);
                DataContext.RaceRepository.Save(gameSession.Nation.Race);
                DataContext.GameSessionRepository.Save(gameSession);
                tran.Commit();
            }

            return RedirectToAction("Index");
        }

        public ActionResult ViewUnit(Guid id)
        {
            var unitData = new UnitData {Unit = DataContext.UnitRepository.Get(id)};
            return View("ViewUnit", unitData);
        }


    }
}
