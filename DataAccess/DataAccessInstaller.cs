using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Model.Repository;
using System.Data.Objects;
using System.Collections;
using System.Web.Configuration;

namespace DataAccess
{
    public class DataAccessInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller Members

        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().BasedOn(typeof(IRepositoryBase<>)).WithService.AllInterfaces().LifestyleTransient());
            
            container.Register(Component.For<DemoContainer>().DependsOn(new Hashtable
            { 
                { "connectionString", WebConfigurationManager.ConnectionStrings["DemoContainer"].ConnectionString }
            }).LifestylePerWebRequest());
        }

        #endregion
    }
}
