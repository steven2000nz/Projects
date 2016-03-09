namespace Calculator.Interface
{
    using Castle.Windsor;

    public interface IPayslipManager
    {
        void GeneratePayslip(IWindsorContainer container, string inputPath, string outputPath, string taxTablePath);
    }
}