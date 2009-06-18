using System;
using System.Web.Mvc;

using eland.api;
using eland.api.Services;
using eland.model;
using eland.api.Interfaces;
using eland.ViewData;

namespace eland.Controllers
{
    public class UserController : Controller
    {
        public IDataContext DataContext { get; set; }

        public ActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var openId = HttpContext.User.Identity.Name;

            if (((UserRepository)DataContext.UserRepository).Exists(openId))
            {
                return ViewUser(openId);
            }
            if (TempData != null)
            {
                ViewData["Email"] = TempData["Email"];
                ViewData["FirstName"] = TempData["Nickname"];
            }
            return New(openId);
        }

        public ActionResult New(String openId)
        {
            return View("New");
        }

        public ActionResult Create(string FirstName, string LastName, string Email)
        {
            var user = new User();

            using (var tran = DataContext.UserRepository.Session.BeginTransaction())
            {
                user.OpenId = HttpContext.User.Identity.Name;
                user.FirstName = FirstName;
                user.LastName = LastName;
                user.Email = Email;

                DataContext.UserRepository.Save(user);

                tran.Commit();
            }

            return ViewUser(string.Empty);
        }

        public ActionResult Edit(String openId)
        {
            return View("Edit");
        }

        public ActionResult ViewUser(String openId)
        {
            var viewUserData = new ViewUserData
                                   {
                                       UserData = ((UserRepository)DataContext.UserRepository).FindByOpenId(openId)
                                   };

            //viewUserData.GameSessionData = ((GameSessionRepository)DataContext.GameSessionRepository).FindByUser(viewUserData.UserData);
            viewUserData.GameSessionData = GameService.CreateSession(GameService.Create(WorldService.Create()),
                                                                     viewUserData.UserData);


            if (viewUserData.UserData == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("ViewUser", viewUserData);
        }
    }
}