using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Core.Resource;

namespace eland.api
{
   class IoC
   {
      private static WindsorContainer _container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));

      public static T Resolve<T>() 
      {
         return _container.Resolve<T>();
      
      }
   }
}
