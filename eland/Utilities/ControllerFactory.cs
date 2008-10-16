using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.Windsor;


namespace eland
{
   public class ControllerFactory : IControllerFactory
   {
      public void DisposeController(IController controller)
      {
          if (!(controller is IDisposable)) return;
          ((IDisposable)controller).Dispose();
          var accessor = HttpContext.Current.ApplicationInstance as IContainerAccessor;
          if (accessor != null) accessor.Container.Release(controller);
      }

      public IController CreateController(RequestContext context, string controllerName)
      {
          var accessor = HttpContext.Current.ApplicationInstance as IContainerAccessor;
          var controller = accessor.Container.Resolve<IController>(controllerName);
          return controller == null ? controller : null;
      }
   }
}
