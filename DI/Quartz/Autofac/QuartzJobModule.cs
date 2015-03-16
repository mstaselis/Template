using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.DI.Quartz.Jobs
{
    public class QuartzJobModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //register items that should be available for quartz jobs

            
        }
    }
}