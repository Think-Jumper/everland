using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Windsor;
using System.Web.Routing;

namespace eland
{
   public class ControllerFactory : IControllerFactory
   {
      public void DisposeController(IController controller)
      {
         if (controller is IDisposable)
         {
            ((IDisposable)controller).Dispose();
            IContainerAccessor accessor = HttpContext.Current.ApplicationInstance as IContainerAccessor;
            accessor.Container.Release(controller);
         }
      }

      public IController CreateController(RequestContext context, string controllerName)
      {
         IContainerAccessor accessor = HttpContext.Current.ApplicationInstance as IContainerAccessor;
         return accessor.Container.Resolve<IController>(controllerName + "Controller");
      }
   }
}
