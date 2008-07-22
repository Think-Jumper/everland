using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using eland.api;
using eland.model;
using System.Collections;

namespace eland.Controllers
{
   public class HomeController : Controller
   {
      public ActionResult Index()
      {
         ViewData["Title"] = "Home Page";
         ViewData["Message"] = "Test";

         return View();
      }

      public ActionResult About()
      {
         ViewData["Title"] = "About Page";

         return View();
      }
   }
}
