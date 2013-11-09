using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace demo.Hubs
{
    public class SignalResolver : DefaultDependencyResolver
    {

        private readonly IWindsorContainer _container;

        public SignalResolver(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;

            foreach (var c in _lazyRegistrations)
                _container.Register(c);

            _lazyRegistrations.Clear();
        }

        public override object GetService(Type serviceType)
        {
            if (_container.Kernel.HasComponent(serviceType))
                return _container.Resolve(serviceType);
            return base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            IEnumerable<object> objects;
            if (_container.Kernel.HasComponent(serviceType))
                objects = _container.ResolveAll(serviceType).Cast<object>();
            else
                objects = new object[] { };

            var originalContainerServices = base.GetServices(serviceType);
            if (originalContainerServices != null)
                return objects.Concat(originalContainerServices);

            return objects;
        }

        public override void Register(Type serviceType, Func<object> activator)
        {
            if (_container != null)
                _container.Register(Component.For(serviceType).UsingFactoryMethod<object>(activator, true).OverridesExistingRegistration());
            else
                _lazyRegistrations.Add(Component.For(serviceType).UsingFactoryMethod<object>(activator));
        }

        private List<ComponentRegistration<object>> _lazyRegistrations = new List<ComponentRegistration<object>>();
    }

    public static class WindsorTrickyExtensions
    {
        public static ComponentRegistration<T> OverridesExistingRegistration<T>(this ComponentRegistration<T> componentRegistration) where T : class
        {
            return componentRegistration
            .Named(Guid.NewGuid().ToString())
            .IsDefault();
        }


    }
}