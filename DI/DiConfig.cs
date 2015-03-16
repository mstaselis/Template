using Autofac;
using Autofac.Integration.Mvc;
using Framework.DI.Quartz.Jobs;
using Framework.DI.Sitemap.Autofac.Modules;
using Framework.ID.Quartz;
using MvcSiteMapProvider.Loader;
using MvcSiteMapProvider.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
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

            // Register modules
            builder.RegisterModule(new ApplicationSiteMapProviderModule()); // Required  
            builder.RegisterModule(new QuartzAutofacFactoryModule());
            builder.RegisterModule(new QuartzAutofacModule(Assembly.GetExecutingAssembly()));

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            MvcSiteMapProvider.SiteMaps.Loader = container.Resolve<ISiteMapLoader>();

            //register quartz
            new RegisterJobs(container);

            // Check all configured .sitemap files to ensure they follow the XSD for MvcSiteMapProvider (optional)
            var validator = container.Resolve<ISiteMapXmlValidator>();
            validator.ValidateXml(HostingEnvironment.MapPath("~/Mvc.sitemap"));
            validator.ValidateXml(HostingEnvironment.MapPath("~/Areas/Admin/Mvc.sitemap"));
        }
    }
}
