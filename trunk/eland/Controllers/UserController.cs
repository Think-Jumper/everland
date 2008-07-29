using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using eland.api;
using eland.model;

namespace eland.Controllers
{
   public class UserController : Controller
   {
      public ActionResult Index()
      {
         if (HttpContext.User.Identity.IsAuthenticated)
         {
            String openId = HttpContext.User.Identity.Name;

            UserRepository userRep = new UserRepository();

            if (userRep.Exists(openId))
            {
               this.ViewUser(openId);
            }
            else
            {
               this.Create(openId);
            }

            return View();
         }
         else
         {
            return RedirectToAction("Index", "Home");
         }
      }

      public ActionResult Create(String openId)
      {
         return View();
      }

      public ActionResult Edit()
      {
         return View();
      }

      public ActionResult ViewUser(String openId)
      {
         return View();
      }
   }
}
