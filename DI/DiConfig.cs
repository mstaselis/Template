using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Framework.DI
{
    public static class DiConfig
    {
        public static void Init()
        {
            ContainerBuilder builder = new ContainerBuilder();

            //register assembly
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();            

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


        }
    }
}
