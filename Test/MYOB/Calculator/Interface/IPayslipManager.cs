namespace Calculator.Interface
{
    using Castle.Windsor;

    /// <summary>
    /// Generate payslip
    /// </summary>
    public interface IPayslipManager
    {
        void GeneratePayslip(IWindsorContainer container, string inputPath, string outputPath, string taxTablePath);
    }
}