using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolverTests.TestClasses.Contract
{
    public interface IGenerator
    {
        string GetText { get; set; }
    }
}
