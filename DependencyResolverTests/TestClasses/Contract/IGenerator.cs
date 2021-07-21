using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolverTests.TestClasses.Contract
{
    /// <summary>
    /// Classes used in tests will implement this interface
    /// </summary>
    public interface IGenerator
    {
        string GetText { get; set; }
    }
}
