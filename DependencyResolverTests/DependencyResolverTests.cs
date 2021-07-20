using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DependencyResolver.Resolver;
using DependencyResolverTests.TestClasses.Implementation;
using DependencyResolverTests.TestClasses.Contract;

namespace DependencyResolverTests
{
    [TestClass]
    public class DependencyResolverTests
    {
        [TestMethod]
        public void InitialTest()
        {
            DIResolver services = new DIResolver();

            //services.RegisterSingleton<DatetimeGenerator>();
            //services.RegisterSingleton<GuidGenerator>();
            //services.RegisterSingleton<IGenerator, GuidGenerator>();
            //services.RegisterSingleton<IGenerator>(new GuidGenerator());

            DatetimeGenerator _sut1 = new DatetimeGenerator();
            GuidGenerator _sut2 = new GuidGenerator();
            services.RegisterSingleton<DatetimeGenerator>(_sut1);
            services.RegisterSingleton<GuidGenerator>(_sut2);

            GuidGenerator _RegisteredService = services.GetService<GuidGenerator>();

            Assert.AreNotEqual(_RegisteredService, null);
            Assert.AreEqual(_sut2.GetText, _RegisteredService.GetText);

        }
    }
}
