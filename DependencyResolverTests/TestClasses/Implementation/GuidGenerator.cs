using DependencyResolverTests.TestClasses.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolverTests.TestClasses.Implementation
{
    public class GuidGenerator : IGenerator
    {
        public string GetText { get; set; } = Guid.NewGuid().ToString();
    }
}
