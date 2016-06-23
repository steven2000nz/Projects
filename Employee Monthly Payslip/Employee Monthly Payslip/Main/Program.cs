namespace EmployeeMonthlyPayslip
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

                Console.WriteLine("Payslip is written to '{0}'.", outputDataPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to generate payslip: {0}", e.Message);
            }
        }

        private static string ReadInput(string requestInput, string defaultPath)
        {
            Console.WriteLine(requestInput + ":");
            var path = Console.ReadLine();

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