namespace MYOB
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Calculator.DataAccess;
    using Calculator.Interface;
    using Calculator.Manager;
    using Calculator.Model;

    internal class Program
    {
        private static void Main(string[] args)
        {
            GeneratePayslip();
        }

        private static void GeneratePayslip()
        {
            IFileReadAccess<InputData> fileReadInputData = new CsvFileReadAccess<InputData>();
            IFileWriteAccess<OutputData> fileWriteOutputData = new CsvFileWriteAccess<OutputData>();
            IPayslipManager payslipManager = new PayslipManager();
            ITaxManager taxManager = new TaxManager();
            IFileReadAccess<Tax> fileReadTaxData = new CsvFileReadAccess<Tax>();

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