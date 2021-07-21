using DependencyResolverTests.TestClasses.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolverTests.TestClasses.Implementation
{
    public class DependencyImplementation : IDependency
    {
        public string Foo { get; set; } = DateTime.Now.Millisecond.ToString();


    }
}
