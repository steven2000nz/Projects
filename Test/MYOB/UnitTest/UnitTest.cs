using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    using Calculator.DataAccess;
    using Calculator.Interface;
    using Calculator.Manager;
    using Calculator.Model;
    using Calculator.Register;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using Rhino.Mocks;

    [TestClass]
    public class UnitTest
    {
        static WindsorContainer container;

        IFileReadAccess<InputData> fileReadInputData;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            /*
            var configuration = new Configuration(new TypeMapFactory(), MapperRegistry.AllMappers());
            var mappingEngine = new MappingEngine(configuration);
            new ScriptAutomapperConfiguration(configuration);
            var scriptManager = new ScriptManager();

            fieldStorage = MockRepository.GenerateStub<IFieldStorage>();
            fieldStorage.Stub(stub => stub.ExtractFieldsFromInstance(null)).IgnoreArguments().Return(new AbstractFieldData[] {});

            comparisonBuilder = MockRepository.GenerateStub<ICustomFieldsPropertyComparisonBuilder>();
            */

            
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
        }

        [TestInitialize()]
        public void Initialize()
        {
        }

        [TestCleanup()]
        public void Cleanup()
        {
        }

        [TestMethod]
        public void ReadInputData()
        {

        }
    }
}
