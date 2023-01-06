using NUnit.Framework;
using SFramework.Core.Runtime;

namespace SFramework.Core.Tests.Editor.SFramework.Modules.SF_Core_Tests.Editor
{
    public class ContainerTests
    {
        private interface ITestService : ISFService
        {
            string Run();
        }
    
        private class TestServiceA : ITestService
        {
            public string Run()
            {
                return "A";
            }
        }
    
        private class TestServiceB : ITestService
        {
            public string Run()
            {
                return "B";
            }
        }
    
        private class TestObject_1 : ISFInjectable
        {
            public ITestService TestService => _testService;

            [SFInject]
            private ITestService _testService;
        }
    
    
        private class TestObject_2 : ISFInjectable
        {
            [SFInject]
            private void Init_1()
            {
                Result++;
            }
        
            [SFInject]
            private void Init_2()
            {
                Result++;
            }

            public int Result { get; protected set; }
        }
    
    
        private class NestedTestObject_2 : TestObject_2
    
        {
            [SFInject]
            private void Init_Parent()
            {
                Result++;
            }
        }

        [Test]
        public void Create_Container()
        {
            var container = new SFContainer();
            Assert.NotNull(container);
        }
    
        [Test]
        public void Bind_Resolve_Interface()
        {
            var container = new SFContainer();
            var serviceInstance = new TestServiceA();
            container.Bind<ITestService>(serviceInstance);
            var resolvedInstance = container.Resolve<ITestService>();
            Assert.AreEqual(resolvedInstance, serviceInstance);
        }
    
        [Test]
        public void Bind_Resolve_Object()
        {
            var container = new SFContainer();
            var serviceInstance = new TestServiceA();
            container.Bind(serviceInstance);
            var resolvedInstance = container.Resolve<TestServiceA>();
            Assert.AreEqual(resolvedInstance, serviceInstance);
        }

        [Test]
        public void Container_Injection()
        {
            var container = new SFContainer();
            var serviceInstance = new TestServiceA();
            var testObjectInstance = new TestObject_1();
            container.Bind<ITestService>(serviceInstance);
            container.Bind(testObjectInstance);
            container.Inject();
            Assert.AreEqual(testObjectInstance.TestService, serviceInstance);
        }
    
        [Test]
        public void Method_Injection()
        {
            var container = new SFContainer();
            var testObjectInstance = new TestObject_2();
            container.Bind(testObjectInstance);
            container.Inject();
            Assert.True(testObjectInstance.Result == 2);
        }
    
        
        [Test]
        public void Parent_Method_Injection()
        {
            var container = new SFContainer();
            var testObjectInstance = new NestedTestObject_2();
            container.Bind(testObjectInstance);
            container.Inject();
            Assert.True(testObjectInstance.Result == 3);
        }
    
        [Test]
        public void Double_Injection()
        {
            var container = new SFContainer();
            var testObjectInstance = new TestObject_2();
            container.Bind(testObjectInstance);
            container.Inject();
            container.Inject();
            Assert.True(testObjectInstance.Result == 2);
        }
    }
}
