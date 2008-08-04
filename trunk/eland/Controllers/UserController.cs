using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using eland.api;
using eland.model;
using eland.api.Interfaces;

namespace eland.Controllers
{
   public class UserController : Controller
   {
      public IDataContext DataContext { get; set; }

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
               // don't like having to do it this way.
               ViewData["Email"] = TempData["Email"];
               ViewData["FirstName"] = TempData["Nickname"];
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
         return View("New");
      }

      public ActionResult Create(string FirstName, string LastName, string Email)
      {
         User user = new User();
         user.OpenId = HttpContext.User.Identity.Name;
         user.FirstName = FirstName;
         user.LastName = LastName;
         user.Email = Email;

         DataContext.UserRepository.Save(user);

         return this.ViewUser(string.Empty);
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
