﻿using Autofac;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Framework.DI.Quartz.Jobs
{
    /// <summary>
    ///     Registers <see cref="ISchedulerFactory" /> and default <see cref="IScheduler" />.
    /// </summary>
    public class QuartzAutofacFactoryModule : Module
    {
        /// <summary>
        ///     Default name for nested lifetime scope.
        /// </summary>
        public const string LifetimeScopeName = "quartz.job";

        private readonly string _lifetimeScopeName;


        /// <summary>
        ///     Initializes a new instance of the <see cref="QuartzAutofacFactoryModule" /> class with a default lifetime scope
        ///     name.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">lifetimeScopeName</exception>
        public QuartzAutofacFactoryModule()
            : this(LifetimeScopeName)
        {
        }


        /// <summary>
        ///     Initializes a new instance of the <see cref="QuartzAutofacFactoryModule" /> class.
        /// </summary>
        /// <param name="lifetimeScopeName">Name of the lifetime scope to wrap job resolution and execution.</param>
        /// <exception cref="System.ArgumentNullException">lifetimeScopeName</exception>
        public QuartzAutofacFactoryModule(string lifetimeScopeName)
        {
            if (lifetimeScopeName == null) throw new ArgumentNullException("lifetimeScopeName");
            _lifetimeScopeName = lifetimeScopeName;
        }


        /// <summary>
        ///     Provides custom configuration for Scheduler.
        ///     Returns <see cref="NameValueCollection" /> with custom Quartz settings.       
        ///     <seealso cref="StdSchedulerFactory" /> for some configuration property names.
        /// </summary>
        public Func<NameValueCollection> ConfigurationProvider { get; set; }


        /// <summary>
        ///     Override to add registrations to the container.
        /// </summary>
        /// <remarks>
        ///     Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">
        ///     The builder through which components can be
        ///     registered.
        /// </param>
        protected override void Load(ContainerBuilder builder)
        {


            builder.Register(c => new AutofacJobFactory(c.Resolve<ILifetimeScope>(), _lifetimeScopeName))
                .AsSelf()
                .As<IJobFactory>()
                .InstancePerLifetimeScope();

            builder.Register<ISchedulerFactory>(c =>
            {
                var cfgProvider = ConfigurationProvider;

                var autofacSchedulerFactory = (cfgProvider != null)
                    ? new AutofacSchedulerFactory(cfgProvider(), c.Resolve<AutofacJobFactory>())
                    : new AutofacSchedulerFactory(c.Resolve<AutofacJobFactory>());
                return autofacSchedulerFactory;
            })
                .SingleInstance();

            builder.Register(c => c.Resolve<ISchedulerFactory>().GetScheduler())
                .SingleInstance();
        }
    }
}