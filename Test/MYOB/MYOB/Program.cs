namespace MYOB
{
    using System;
    using System.IO;

    using Calculator.Interface;
    using Calculator.Register;

    using Castle.Windsor;

    using static System.Console;

    internal class Program
    {
        private static readonly WindsorContainer Container = new WindsorContainer();

        private static void Main(string[] args)
        {
            try
            {
                //Get user inputs
                var inputDataPath = ReadInput("Please enter input data path", @"Data\InputData.csv");
                var outputDataPath = ReadInput("Please enter output data path", @"Data\OutputData.csv");
                var taxTablePath = ReadInput("Please enter tax table path", @"Tax\TaxTable.csv");

                //Register components in Calculator assembly
                RegisterContainers();

                //Generate payslip
                var calculatorContainer = Container.GetChildContainer(Calculator.Register.Container.Name);
                var payslipManager = calculatorContainer.Resolve<IPayslipManager>();
                payslipManager.GeneratePayslip(calculatorContainer, inputDataPath, outputDataPath, taxTablePath);

                WriteLine($"Payslip is written to '{outputDataPath}'.");
            }
            catch (Exception e)
            {
                WriteLine($"Failed to generate payslip: {e.Message}");
            }
        }

        private static string ReadInput(string requestInput, string defaultPath)
        {
            WriteLine(requestInput + ":");
            var path = ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(path))
            {
                path = Path.Combine(Environment.CurrentDirectory, defaultPath);
            }

            return path;
        }

        private static void RegisterContainers()
        {
            ICalculatorContainer calculatorContainer = new Container();
            var calculatorChildContainer = calculatorContainer.RegisterComponents();

            Container.AddChildContainer(calculatorChildContainer);
        }
    }
}