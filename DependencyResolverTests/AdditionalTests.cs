using DependencyResolver.Resolver;
using DependencyResolverTests.TestClasses.Contract;
using DependencyResolverTests.TestClasses.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DependencyResolverTests
{
    [TestClass]
    public class AdditionalTests
    {
        [TestMethod]
        public void TestMultipleImplementations()
        {
            DIResolver _sut1 = new DIResolver();

            DatetimeGenerator ImplementationOne = new DatetimeGenerator();
            GuidGenerator ImplementationTwo = new GuidGenerator();

            _sut1.RegisterSingleton<DatetimeGenerator>(ImplementationOne);
            _sut1.RegisterSingleton<IGenerator>(ImplementationTwo);

            IGenerator _ResolvedService = _sut1.GetService<IGenerator>();

            Assert.AreNotEqual(_ResolvedService, null);
            Assert.AreEqual(_ResolvedService.GetType(), typeof(GuidGenerator));
            Assert.AreEqual(_ResolvedService.GetText, ImplementationTwo.GetText);
        }

        [TestMethod]
        public void TestNestedDependencies_Initialization()
        {
            DIResolver _sut = new DIResolver();

            _sut.RegisterSingleton<IGenerator, MathGenerator>(); //Has dependency on IDependency interface
            _sut.RegisterSingleton<IDependency, DependencyImplementation>();

            IGenerator _ResolvedService = _sut.GetService<IGenerator>();
            Assert.AreNotEqual(_ResolvedService, null);
            Assert.AreEqual(_ResolvedService.GetType(), typeof(MathGenerator));
        }

        [TestMethod]
        public void TestNestedDependencies_Values()
        {
            DIResolver _sut = new DIResolver();

            IDependency _internalImplementation = new DependencyImplementation();

            _sut.RegisterSingleton<IGenerator, MathGenerator>(); //Has dependency on IDependency interface
            _sut.RegisterSingleton<IDependency>(_internalImplementation);

            IGenerator _ResolvedService = _sut.GetService<IGenerator>();
            Assert.AreNotEqual(_ResolvedService, null);
            Assert.AreEqual(_ResolvedService.GetType(), typeof(MathGenerator));
            Assert.AreEqual(
                ((MathGenerator)_ResolvedService).GetInternalText, _internalImplementation.Foo);
            Console.WriteLine(_internalImplementation.Foo);
        }
    }
}
