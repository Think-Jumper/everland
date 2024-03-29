﻿using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Core.Resource;

namespace eland.api
{
    public sealed class IoC
    {
        private static WindsorContainer container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));

        public static T Resolve<T>() 
        {
            return container.Resolve<T>();
        }
    }
}