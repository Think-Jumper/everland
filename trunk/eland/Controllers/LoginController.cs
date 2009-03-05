﻿using System;
using System.Web.Mvc;

using DotNetOpenId.RelyingParty;
using DotNetOpenId.Extensions.SimpleRegistration;
using System.Web.Security;

namespace eland.Controllers
{
   public class LoginController : Controller
   {
      public ActionResult Login()
      {
         return View("Login");
      }

      public ActionResult Logout()
      {
         FormsAuthentication.SignOut();
         return RedirectToAction("Index", "Home");
      }

      public ActionResult Authenticate()
      {
         var openid = new OpenIdRelyingParty();
         if (openid.Response == null)
         {
            try
            {
               var req = openid.CreateRequest(Request.Form["openid_identifier"]);
               var fields = new ClaimsRequest { Email = DemandLevel.Require, Nickname = DemandLevel.Require };

               req.AddExtension(fields);
               req.RedirectToProvider();
            }
            catch (Exception e)
            {
               ViewData["Message"] = e.Message;
               return View("Login");
            }
         }
         else
         {
            switch (openid.Response.Status)
            {
               case AuthenticationStatus.Authenticated:

                  var fields = openid.Response.GetExtension(typeof(ClaimsResponse)) as ClaimsResponse;

                  if(fields != null) {
                     TempData["Email"] = fields.Email;
                     TempData["Nickname"] = fields.Nickname;
                  }

                  FormsAuthentication.RedirectFromLoginPage(openid.Response.ClaimedIdentifier, false);

                  break;
               case AuthenticationStatus.Canceled:
                  ViewData["Message"] = "Canceled at provider";
                  return View("Login");
               case AuthenticationStatus.Failed:
                  ViewData["Message"] = openid.Response.Exception.Message;
                  return View("Login");
            }
         }

         // need this rather than returning an ActionResult.
         return null;
      }
   }
}