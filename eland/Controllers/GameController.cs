using System;
using System.Collections.Generic;
using System.Linq;
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
         GameIndexData gameIndexData = new GameIndexData();
         gameIndexData.GameSessionData = (DataContext.GameSessionRepository as GameSessionRepository).FindByUserId(HttpContext.User.Identity.Name);

         return View("Index", gameIndexData);
      }
   }
}
