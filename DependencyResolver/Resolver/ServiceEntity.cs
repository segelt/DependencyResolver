using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolver.Resolver
{
    public class ServiceEntity
    {
        public Type Type { get; set; } //Can hold both interface and class

        public object Implementation { get; internal set; }

        public ServiceLifetime Lifetime { get; internal set; }

        public ServiceEntity(object implementation, ServiceLifetime Lifetime)
        {
            Type = implementation.GetType();
            Implementation = implementation;
            this.Lifetime = Lifetime;
        }

        public ServiceEntity(Type type, ServiceLifetime Lifetime)
        {
            Type = type;
            this.Lifetime = Lifetime;
        }
    }
}
