﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Core.Resource;
using eland.Utilities;

namespace eland
{
   public class GlobalApplication : System.Web.HttpApplication, IContainerAccessor
   {
      private static readonly WindsorContainer _container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));

      public static void RegisterRoutes(RouteCollection routes)
      {
         routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

         routes.MapRoute( "GameViewUnit", "Game/ViewUnit/{id}", new { controller = "Game", action = "ViewUnit", id = new Guid() });
         routes.MapRoute( "Default", "{controller}/{action}/{id}",  new { controller = "Home", action = "Index", id = "" } );

      }

      protected void Application_Start()
      {
         RegisterRoutes(RouteTable.Routes);
         ControllerBuilder.Current.SetControllerFactory(typeof(ControllerFactory));
      }

      public IWindsorContainer Container
      {
         get { return _container; }
      }
   }
}