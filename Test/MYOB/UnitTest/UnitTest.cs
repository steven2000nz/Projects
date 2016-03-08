namespace UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Calculator.DataAccess;
    using Calculator.Interface;
    using Calculator.Manager;
    using Calculator.Model;
    using Calculator.Register;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rhino.Mocks;

    [TestClass]
    public class UnitTest
    {
        private static WindsorContainer container;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            var calculatorContainer = MockRepository.GenerateStub<ICalculatorContainer>();
            calculatorContainer.Stub(stub => stub.RegisterComponents()).Return(GetTestContainer());

            container = calculatorContainer.RegisterComponents();
        }

        private static WindsorContainer GetTestContainer()
        {
            var testContainer = new WindsorContainer();

            testContainer.Register(Component.For(typeof(IPayslipManager)).ImplementedBy(typeof(PayslipManager)).LifestyleTransient());
            testContainer.Register(Component.For(typeof(ITaxManager)).ImplementedBy(typeof(TaxManager)).LifestyleTransient());
            testContainer.Register(Component.For(typeof(IFileReadAccess<>)).ImplementedBy(typeof(CsvFileReadAccess<>)).Named(Container.CsvFileReadAccess).LifestyleTransient());
            testContainer.Register(Component.For(typeof(IFileWriteAccess<>)).ImplementedBy(typeof(CsvFileWriteAccess<>)).Named(Container.CsvFileWriteAccess).LifestyleTransient());

            return testContainer;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod]
        public void ReadInputData()
        {
            var fileReadInputData = container.Resolve<IFileReadAccess<InputData>>(Container.CsvFileReadAccess);
            var inputPath = Path.Combine(Environment.CurrentDirectory, @"TestData\InputData.csv");
            var firstRecord = fileReadInputData.ReadFile(inputPath).First();

            Assert.AreEqual(firstRecord.FirstName, "David");
            Assert.AreEqual(firstRecord.AnnualSalary, 60050);
        }

        [TestMethod]
        public void ReadTaxTable()
        {
            var fileReadTaxData = container.Resolve<IFileReadAccess<Tax>>(Container.CsvFileReadAccess);
            var taxTablePath = Path.Combine(Environment.CurrentDirectory, @"TestData\TaxTable.csv");
            var lastRecord = fileReadTaxData.ReadFile(taxTablePath).Last();

            Assert.AreEqual(lastRecord.Start, 180001);
            Assert.AreEqual(lastRecord.Rate, 45);
        }

        [TestMethod]
        public void CalculateTax()
        {
            var taxManager = container.Resolve<ITaxManager>();
            var taxList = CreateTaxList();

            Assert.AreEqual(922, taxManager.CalculateTax(taxList, 60050));
        }

        [TestMethod]
        public void GenerateOutputData()
        {
            var payslipManager = container.Resolve<IPayslipManager>();
            var taxManager = MockRepository.GenerateStub<ITaxManager>();
            taxManager.Stub(stub => stub.CalculateTax(null, 0)).IgnoreArguments().Return(922);
            var inputData = CreateInputData();
            var outputData = payslipManager.GenerateOutputData(inputData, CreateTaxList(), taxManager);

            Assert.AreEqual(outputData.Name, inputData.FirstName + " " + inputData.LastName);
            Assert.AreEqual(outputData.GrossIncome, 5004);
            Assert.AreEqual(outputData.Super, 450);
            Assert.AreEqual(outputData.NetIncome, 4082);
        }

        private List<Tax> CreateTaxList()
        {
            return new List<Tax>
                       {
                           new Tax { Start = 37001, Finish = 80000, Base = 3572, Rate = (decimal)32.5 },
                           new Tax { Start = 80001, Finish = 180000, Base = 17547, Rate = 37 }
                       };
        }

        private InputData CreateInputData()
        {
            return new InputData
            {
                FirstName = "David",
                LastName = "Rudd",
                AnnualSalary = 60050,
                SuperRate = 9,
                PaymentStartDate = "01 March - 31 March"
            };
        }
    }
}