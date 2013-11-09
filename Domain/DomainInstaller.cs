using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Model.Model.Interfaces.Domain;

namespace Domain
{
    public class DomainInstaller: IWindsorInstaller
    {
        #region IWindsorInstaller Members

        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<IGroupModel>().ImplementedBy<GroupModel>().LifestyleTransient());
            container.Register(Component.For<IUserModel>().ImplementedBy<UserModel>().LifestyleTransient());
        }

        #endregion
    }
}
