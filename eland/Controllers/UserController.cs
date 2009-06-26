using System;
using System.Web.Mvc;
using eland.api;
using eland.Filters;
using eland.model;
using eland.ViewData;

namespace eland.Controllers
{
    [Authentication]
    public class UserController : BaseController
    {
        public ActionResult Index()
        {
            if (((UserRepository)DataContext.UserRepository).Exists(HttpContext.User.Identity.Name))
            {
                return RedirectToAction("ViewUser");
            }
            if (TempData != null)
            {
                ViewData["Email"] = TempData["Email"];
                ViewData["FirstName"] = TempData["Nickname"];
            }
            return RedirectToAction("New");
        }

        public ActionResult New()
        {
            return View("New");
        }

        [AcceptVerbs(HttpVerbs.Post), UserName]
        public ActionResult CreateUser(string firstName, string lastName, string email, string userName)
        {
            var user = new User();
            using (var tran = DataContext.UserRepository.Session.BeginTransaction())
            {
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Email = email;
                user.OpenId = userName;

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
            var viewUserData = new ViewUserData { UserData = ((UserRepository)DataContext.UserRepository).FindByOpenId(openId) };

            return View("ViewUser", viewUserData);
        }

    }
}
