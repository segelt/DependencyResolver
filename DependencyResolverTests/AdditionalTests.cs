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
    }
}
