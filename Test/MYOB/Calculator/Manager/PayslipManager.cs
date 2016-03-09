namespace Calculator.Manager
{
    using System.Collections.Generic;
    using System.IO;

    using Calculator.Interface;
    using Calculator.Model;
    using Calculator.Register;

    using Castle.Windsor;

    public class PayslipManager : IPayslipManager
    {
        public void GeneratePayslip(
            IWindsorContainer container,
            string inputPath,
            string outputPath,
            string taxTablePath)
        {
            var fileReadAsyncInputData = container.Resolve<IFileReadAsyncAccess<InputData>>();
            var fileWriteOutputData = container.Resolve<IFileWriteAccess<OutputData>>(Container.CsvFileWriteAccess);
            var payslipManager = container.Resolve<ICalculationManager>();
            var taxManager = container.Resolve<ITaxManager>();
            var fileReadTaxData = container.Resolve<IFileReadAccess<Tax>>(Container.CsvFileReadAccess);

            fileWriteOutputData.WriteFile(outputPath, new List<OutputData>());
            var taxList = fileReadTaxData.ReadFile(taxTablePath);

            using (StreamWriter sw = File.AppendText(outputPath))
            {
                fileReadAsyncInputData.ReadFileAsync(inputPath, record =>
                {
                    var outputData = payslipManager.GenerateOutputData(record, taxList, taxManager);
                    string formatedRecord = 
                        string.Format($"{outputData.Name},{outputData.PayPeriod},{outputData.GrossIncome},{outputData.IncomeTax},{outputData.NetIncome},{outputData.Super}");
                    sw.WriteLine(formatedRecord);
                });
            }
        }
    }
}