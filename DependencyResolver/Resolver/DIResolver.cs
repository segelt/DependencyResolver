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

        public TService GetService<TService>()
        {
            if (_container.registries.TryGetValue(typeof(TService), out ServiceEntity value))
            {
                if(value.Lifetime == ServiceLifetime.Transient)
                {
                    TService implementation = (TService)Activator.CreateInstance(value.Type);
                    return implementation;
                }
                else //This is a singleton instance
                {
                    if (value.Implementation == null)
                    {
                        TService implementation = (TService)Activator.CreateInstance(value.Type);
                        value.Implementation = implementation;

                        return implementation;
                    }
                    else
                    {
                        return (TService)value.Implementation;
                    }
                }
            }

            throw new Exception("Service does not exist");
        }

        #region Singleton
        public void RegisterSingleton<TService>()
        {
            if (!_container.CheckIfImplementationExists<TService>())
            {
                _container.registries.Add(typeof(TService), new ServiceEntity(typeof(TService), ServiceLifetime.Singleton));
            }
        }

        public void RegisterSingleton<TService, TImplementation>()
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

        public void RegisterTransient<TService, TImplementation>()
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
