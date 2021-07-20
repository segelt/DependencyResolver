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
            throw new NotImplementedException();
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

        #region Scoped
        public void RegisterScoped<T>()
        {
            throw new System.NotImplementedException();
        }

        public void RegisterScoped<TService, TImplementation>()
        {
            throw new NotImplementedException();
        }

        public void RegisterScoped<TService>(TService implementation)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Transient
        public void RegisterTransient<T>()
        {
            throw new System.NotImplementedException();
        }

        public void RegisterTransient<TService, TImplementation>()
        {
            throw new NotImplementedException();
        }

        public void RegisterTransient<TService>(TService implementation)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
