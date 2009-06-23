using System;
using System.Web.Mvc;

using eland.api;
using eland.api.Services;
using eland.Filters;
using eland.model;
using eland.ViewData;

namespace eland.Controllers
{
    [Authentication]
    public class UserController : BaseController
    {

        [UserName]
        public ActionResult Index(String userName)
        {
            var openId = userName;

            if (((UserRepository)DataContext.UserRepository).Exists(openId))
            {
                return RedirectToAction("ViewUser");
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

        public ActionResult Create(string firstName, string lastName, string email)
        {
            var user = new User();

            using (var tran = DataContext.UserRepository.Session.BeginTransaction())
            {
                user.OpenId = HttpContext.User.Identity.Name;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Email = email;

                DataContext.UserRepository.Save(user);

                tran.Commit();
            }

            return RedirectToAction("ViewUser");
        }

        public ActionResult Edit(String openId)
        {
            return View("Edit");
        }

        [UserName]
        public ActionResult ViewUser(string userName)
        {
            var openId = userName;
            var viewUserData = new ViewUserData{ UserData = ((UserRepository)DataContext.UserRepository).FindByOpenId(openId) };

            viewUserData.GameSessionData = GameService.CreateSession(GameService.Create(WorldService.Create()), viewUserData.UserData);
            
            if (viewUserData.UserData == null)
                return RedirectToAction("Index", "Home");
            
            return View("ViewUser", viewUserData);
        }
    }
}