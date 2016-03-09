namespace MYOB
{
    using System;
    using System.IO;

    using Calculator.Interface;
    using Calculator.Register;

    using Castle.Windsor;

    internal class Program
    {
        private static readonly WindsorContainer Container = new WindsorContainer();

        private static void Main(string[] args)
        {
            var inputPath = Path.Combine(Environment.CurrentDirectory, @"Data\InputData.csv");
            var outputPath = Path.Combine(Environment.CurrentDirectory, @"Data\OutputData.csv");
            var taxTablePath = Path.Combine(Environment.CurrentDirectory, @"Tax\TaxTable.csv");

            RegisterContainers();

            var calculatorContainer = Container.GetChildContainer(Calculator.Register.Container.Name);
            var payslipManager = calculatorContainer.Resolve<IPayslipManager>();
            payslipManager.GeneratePayslip(calculatorContainer, inputPath, outputPath, taxTablePath);
        }

        private static void RegisterContainers()
        {
            ICalculatorContainer calculatorContainer = new Container();
            var calculatorChildContainer = calculatorContainer.RegisterComponents();

            Container.AddChildContainer(calculatorChildContainer);
        }
    }
}