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
               return this.ViewUser(openId);
            }
            else
            {
               return this.New(openId);
            }
         }
         else
         {
            return RedirectToAction("Index", "Home");
         }
      }

      public ActionResult New(String openId)
      {
         return this.ViewUser(openId);
      }

      public ActionResult Edit(String openId)
      {
         return View("Edit");
      }

      public ActionResult ViewUser(String openId)
      {
         return View("ViewUser");
      }
   }
}
