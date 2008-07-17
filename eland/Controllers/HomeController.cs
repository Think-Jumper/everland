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


         Repository<World> worldRep = new Repository<World>();

         World world = new World();

         world.Name = "default";
         world.Height = 10;
         world.Width = 30;

         worldRep.Save(world);

         Repository<Hex> hexRep = new Repository<Hex>();

         Hex hex = new Hex();
         hex.world = world;
         hex.X = 0;
         hex.Y = 0;

         hexRep.Save(hex);

         hexRep.Delete(hex);





         return View();
      }

      public ActionResult About()
      {
         ViewData["Title"] = "About Page";

         return View();
      }
   }
}
