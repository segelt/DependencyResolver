using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DependencyResolver.Resolver;
using DependencyResolverTests.TestClasses.Implementation;
using DependencyResolverTests.TestClasses.Contract;
using System.Threading;

namespace DependencyResolverTests
{
    [TestClass]
    public class DependencyResolverTests
    {
        [TestMethod]
        public void InitialTest()
        {
            DIResolver _sut = new DIResolver();

            //services.RegisterSingleton<DatetimeGenerator>();
            //services.RegisterSingleton<GuidGenerator>();
            //services.RegisterSingleton<IGenerator, GuidGenerator>();
            //services.RegisterSingleton<IGenerator>(new GuidGenerator());

            DatetimeGenerator ImplementationOne = new DatetimeGenerator();
            GuidGenerator ImplementationTwo = new GuidGenerator();
            _sut.RegisterSingleton<DatetimeGenerator>(ImplementationOne);
            _sut.RegisterSingleton<GuidGenerator>(ImplementationTwo);

            GuidGenerator _RegisteredService = _sut.GetService<GuidGenerator>();

            Assert.AreNotEqual(_RegisteredService, null);
            Assert.AreEqual(ImplementationTwo.GetText, _RegisteredService.GetText);

        }

        [TestMethod]
        public void Test_Singleton_InterfaceRegisteringCTOR()
        {
            DIResolver _sut1 = new DIResolver();

            DatetimeGenerator foo = new DatetimeGenerator();
            _sut1.RegisterSingleton<IGenerator>(foo);

            Thread.Sleep(2000);

            IGenerator resolvedService = _sut1.GetService<IGenerator>();

            Assert.AreNotEqual(resolvedService, null);
            Assert.AreEqual(resolvedService.GetText, foo.GetText);
        }

        [TestMethod]
        public void Test_Singleton_InterfaceRegisteringMultipleGenericParams()
        {
            DIResolver _sut1 = new DIResolver();

            _sut1.RegisterSingleton<IGenerator, GuidGenerator>();

            IGenerator resolvedService = _sut1.GetService<IGenerator>();
            IGenerator resolvedServiceTwo = _sut1.GetService<IGenerator>();

            Assert.AreNotEqual(resolvedService, null);
            Assert.AreEqual(resolvedService.GetType(), typeof(GuidGenerator));
            Assert.AreEqual(resolvedService.GetText, resolvedServiceTwo.GetText);
        }

        [TestMethod]
        public void Test_Singleton_InterfaceRegisteringSingleGenericParams()
        {
            DIResolver _sut1 = new DIResolver();

            _sut1.RegisterSingleton<GuidGenerator>();

            IGenerator resolvedService = _sut1.GetService<GuidGenerator>();
            IGenerator resolvedServiceTwo = _sut1.GetService<GuidGenerator>();

            Assert.AreNotEqual(resolvedService, null);
            Assert.AreEqual(resolvedService.GetType(), typeof(GuidGenerator));
            Assert.AreEqual(resolvedService.GetText, resolvedServiceTwo.GetText);
        }

        #region Transient

        [TestMethod]
        public void Test_Transient_InterfaceRegisteringCTOR()
        {
            DIResolver _sut1 = new DIResolver();

            GuidGenerator foo = new GuidGenerator();
            _sut1.RegisterTransient<IGenerator>(foo);

            IGenerator resolvedServiceOne = _sut1.GetService<IGenerator>();
            IGenerator resolvedServiceTwo = _sut1.GetService<IGenerator>();

            Assert.AreNotEqual(resolvedServiceOne, null);
            Assert.AreEqual(resolvedServiceOne.GetType(), typeof(GuidGenerator));
            Assert.AreNotEqual(resolvedServiceOne.GetText, foo.GetText); //Using transient, new instances of GuidGenerator
                                                                         //should be instantiated each time the service is resolved
            Assert.AreNotEqual(resolvedServiceOne.GetText, resolvedServiceTwo.GetText);
        }

        [TestMethod]
        public void Test_Transient_InterfaceRegisteringMultipleGenericParams()
        {
            DIResolver _sut1 = new DIResolver();

            _sut1.RegisterTransient<IGenerator, GuidGenerator>();

            IGenerator resolvedServiceOne = _sut1.GetService<IGenerator>();
            IGenerator resolvedServiceTwo = _sut1.GetService<IGenerator>();

            Assert.AreNotEqual(resolvedServiceOne, null);
            Assert.AreEqual(resolvedServiceOne.GetType(), typeof(GuidGenerator));
            Assert.AreNotEqual(resolvedServiceOne.GetText, resolvedServiceTwo.GetText);
        }

        [TestMethod]
        public void Test_Transient_InterfaceRegisteringSingleGenericParams()
        {
            DIResolver _sut1 = new DIResolver();

            _sut1.RegisterTransient<GuidGenerator>();

            IGenerator resolvedService = _sut1.GetService<GuidGenerator>();
            IGenerator resolvedServiceTwo = _sut1.GetService<GuidGenerator>();

            Assert.AreNotEqual(resolvedService, null);
            Assert.AreEqual(resolvedService.GetType(), typeof(GuidGenerator));
            Assert.AreNotEqual(resolvedService.GetText, resolvedServiceTwo.GetText);
        }

        #endregion

    }
}
