using DependencyResolverTests.TestClasses.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolverTests.TestClasses.Implementation
{
    public class MathGenerator : IGenerator
    {
        public string GetText { get; set; } = (new Random().NextDouble() * 50 + 10).ToString();

        internal IDependency _acyclicDependency;

        public string GetInternalText { 
            get
            {
                return _acyclicDependency.Foo;
            }
        }

        public MathGenerator(IDependency dependency)
        {
            _acyclicDependency = dependency;
        }
    }
}
