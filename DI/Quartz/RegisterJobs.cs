using Autofac;
using Autofac.Integration.Mvc;
using Framework.DI.Quartz.Jobs;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.ID.Quartz
{
    public class RegisterJobs
    {

        public IScheduler Scheduler { get; set; }

        public RegisterJobs(IContainer container)
        {

            var scope = container.BeginLifetimeScope("QuartzInitScope", builder =>
            {
                builder.RegisterModule(new QuartzJobModule());
            });
            try
            {
                //resolve components manually
                Scheduler = scope.Resolve<IScheduler>();

                //call register
                Register();

                //start quartz with 30 seconds delay
                Scheduler.StartDelayed(TimeSpan.FromSeconds(30));

            }
            catch (Exception ex)
            {
                throw new Exception("Quartz init failed", ex);
            }
            finally
            {
                //dispose scope and release all resources in it
                scope.Dispose();
            }


        }

        public void Register()
        {
            //put quartz job registrations here           

        }
    }
}