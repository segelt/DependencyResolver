using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolver.Resolver
{
    public class DIContainer
    {
        public Dictionary<Type, ServiceEntity> registries;

        public DIContainer()
        {
            registries = new Dictionary<Type, ServiceEntity>();
        }

        public bool CheckIfImplementationExists<TService>()
        {
            bool _isInterface = typeof(TService).IsInterface;

            if (_isInterface)
            {
                //todo - check this query
                return registries.Keys
                    .Any(p => typeof(TService).IsAssignableFrom(p));
            }
            else
            {
                return registries.Keys
                    .Any(p => typeof(TService) == p);
            }
        }
    }
}
