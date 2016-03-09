namespace Calculator.Manager
{
    using System.Collections.Generic;

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
            var fileReadInputData = container.Resolve<IFileReadAccess<InputData>>(Container.CsvFileReadAccess);
            var fileWriteOutputData = container.Resolve<IFileWriteAccess<OutputData>>(Container.CsvFileWriteAccess);
            var payslipManager = container.Resolve<ICalculationManager>();
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