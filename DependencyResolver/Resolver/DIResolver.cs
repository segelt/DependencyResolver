using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolver.Resolver
{
    public class DIResolver
    {
        internal DIContainer _container;

        public DIResolver()
        {
            _container = new DIContainer();
        }

        public object GetService(Type serviceType)
        {
            if (_container.registries.TryGetValue(serviceType, out ServiceEntity value))
            {
                if(value.Lifetime == ServiceLifetime.Singleton && value.Implementation != null) {
                    return value.Implementation;
                }

                Type targetType = serviceType;

                if (serviceType.IsAbstract || serviceType.IsInterface)
                {
                    targetType = value.Type;
                }

                var constructorInfo = targetType.GetConstructors().First();

                var parameters = constructorInfo.GetParameters()
                    .Select(x => GetService(x.ParameterType)).ToArray();

                var implementation = Activator.CreateInstance(value.Type, parameters);

                if(value.Lifetime == ServiceLifetime.Singleton)
                {
                    value.Implementation = implementation;
                }

                return implementation;
            }

            throw new Exception("Service does not exist");
        }

        public TService GetService<TService>()
        {
            return (TService)GetService(typeof(TService));
        }

        #region Singleton
        public void RegisterSingleton<TService>()
        {
            if (!_container.CheckIfImplementationExists<TService>())
            {
                _container.registries.Add(typeof(TService), new ServiceEntity(typeof(TService), ServiceLifetime.Singleton));
            }
        }

        public void RegisterSingleton<TService, TImplementation>() where TImplementation: TService
        {
            if (!_container.CheckIfImplementationExists<TService>())
            {
                _container.registries.Add(typeof(TService), new ServiceEntity(typeof(TImplementation), ServiceLifetime.Singleton));
            }
        }

        public void RegisterSingleton<TService>(TService implementation)
        {
            //throw new NotImplementedException();
            if (!_container.CheckIfImplementationExists<TService>())
            {
                _container.registries.Add(typeof(TService), new ServiceEntity(implementation, ServiceLifetime.Singleton));
            }
        }
        #endregion

        #region Transient
        public void RegisterTransient<TService>()
        {
            if (!_container.CheckIfImplementationExists<TService>())
            {
                _container.registries.Add(typeof(TService), new ServiceEntity(typeof(TService), ServiceLifetime.Transient));
            }
        }

        public void RegisterTransient<TService, TImplementation>() where TImplementation : TService
        {
            if (!_container.CheckIfImplementationExists<TService>())
            {
                _container.registries.Add(typeof(TService), new ServiceEntity(typeof(TImplementation), ServiceLifetime.Transient));
            }
        }

        public void RegisterTransient<TService>(TService implementation)
        {
            if (!_container.CheckIfImplementationExists<TService>())
            {
                _container.registries.Add(typeof(TService), new ServiceEntity(implementation, ServiceLifetime.Transient));
            }
        } 
        #endregion
    }
}
