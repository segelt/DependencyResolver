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
            //Interfaces should be included even if some key already implements them.
            return registries.Keys
                .Any(p => typeof(TService) == p);
        }
    }
}
