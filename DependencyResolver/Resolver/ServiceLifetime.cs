using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolver.Resolver
{
    public enum ServiceLifetime
    {
        Singleton,
        Transient,
        //Scoped
    }
}
