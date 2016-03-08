namespace MYOB
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Calculator.DataAccess;
    using Calculator.Interface;
    using Calculator.Manager;
    using Calculator.Model;

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
            Calculator.Register.Container.RegisterComponents(container);

            IFileReadAccess<InputData> fileReadInputData = container.Resolve<IFileReadAccess<InputData>>("CsvFileReadAccess");
            IFileWriteAccess<OutputData> fileWriteOutputData = container.Resolve<IFileWriteAccess<OutputData>>("CsvFileWriteAccess");
            IPayslipManager payslipManager = container.Resolve<IPayslipManager>();
            ITaxManager taxManager = container.Resolve<ITaxManager>();
            IFileReadAccess<Tax> fileReadTaxData = container.Resolve<IFileReadAccess<Tax>>("CsvFileReadAccess");

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