using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using demo.Hubs;
using Microsoft.AspNet.SignalR.Hubs;

namespace demo
{
    public class ControllersInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller Members

        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());
            container.Register(Classes.FromThisAssembly().BasedOn<IHub>().LifestyleTransient());
        }

        #endregion
    }
}