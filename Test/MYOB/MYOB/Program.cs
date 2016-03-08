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
            GeneratePayslip();
        }

        private static void GeneratePayslip()
        {
            var container = new WindsorContainer();
            var calculatorContainer = new Container();
            calculatorContainer.RegisterComponents(container);

            var fileReadInputData = container.Resolve<IFileReadAccess<InputData>>("CsvFileReadAccess");
            var fileWriteOutputData = container.Resolve<IFileWriteAccess<OutputData>>("CsvFileWriteAccess");
            var payslipManager = container.Resolve<IPayslipManager>();
            var taxManager = container.Resolve<ITaxManager>();
            var fileReadTaxData = container.Resolve<IFileReadAccess<Tax>>("CsvFileReadAccess");

            var taxList = fileReadTaxData.ReadFile(Path.Combine(Environment.CurrentDirectory, @"Tax\TaxTable.csv"));
            var records = fileReadInputData.ReadFile(Path.Combine(Environment.CurrentDirectory, @"Data\InputData.csv"));
            var outputData = new List<OutputData>();

            foreach (var record in records)
            {
                outputData.Add(payslipManager.GenerateOutputData(record, taxList, taxManager));
            }

            fileWriteOutputData.WriteFile(
                Path.Combine(Environment.CurrentDirectory, @"Data\OutputData.csv"),
                outputData);
        }
    }
}