using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.MicroKernel.Registration;
using demo.Code;
using Microsoft.AspNet.SignalR;
using demo.Hubs;

namespace demo
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication, IContainerAccessor
    {

        public IWindsorContainer Container { get { return container; } private set { container = value; } }

        private static IWindsorContainer container;

        private static void BootstrapContainer()
        {
            container = new WindsorContainer()
                .Install(FromAssembly.InDirectory(new AssemblyFilter("bin")));
            var controllerFactory = new WindsorControllerFactory(container.Kernel);

            var signalrDependency = new SignalResolver(container);
            GlobalHost.DependencyResolver = signalrDependency;
            RouteTable.Routes.MapHubs(
                new HubConfiguration
                {
                    Resolver = signalrDependency,
                    EnableCrossDomain = true,
                    EnableJavaScriptProxies = true
                });

            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_Start()
        {
            BootstrapContainer();
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            var a = this.Request;
        }
    }
}