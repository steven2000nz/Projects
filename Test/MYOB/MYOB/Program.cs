namespace MYOB
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Calculator.Interface;
    using Calculator.Model;
    using Calculator.Register;

    using Castle.Windsor;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var inputPath = Path.Combine(Environment.CurrentDirectory, @"Data\InputData.csv");
            var outputPath = Path.Combine(Environment.CurrentDirectory, @"Data\OutputData.csv");
            var taxTablePath = Path.Combine(Environment.CurrentDirectory, @"Tax\TaxTable.csv");

            ICalculatorContainer calculatorContainer = new Container();
            var calculatorChildContainer = calculatorContainer.RegisterComponents();

            var container = new WindsorContainer();
            container.AddChildContainer(calculatorChildContainer);

            GeneratePayslip(container.GetChildContainer(Calculator.Register.Container.Name), inputPath, outputPath, taxTablePath);
        }

        private static void GeneratePayslip(IWindsorContainer container, string inputPath, string outputPath, string taxTablePath)
        {
            var fileReadInputData = container.Resolve<IFileReadAccess<InputData>>(Container.CsvFileReadAccess);
            var fileWriteOutputData = container.Resolve<IFileWriteAccess<OutputData>>(Container.CsvFileWriteAccess);
            var payslipManager = container.Resolve<IPayslipManager>();
            var taxManager = container.Resolve<ITaxManager>();
            var fileReadTaxData = container.Resolve<IFileReadAccess<Tax>>(Container.CsvFileReadAccess);

            var taxList = fileReadTaxData.ReadFile(taxTablePath);
            var records = fileReadInputData.ReadFile(inputPath);
            var outputData = new List<OutputData>();

            foreach (var record in records)
            {
                outputData.Add(payslipManager.GenerateOutputData(record, taxList, taxManager));
            }

            fileWriteOutputData.WriteFile(outputPath, outputData);
        }
    }
}