namespace Calculator.Manager
{
    using System;
    using System.Collections.Generic;

    using Calculator.Interface;
    using Calculator.Model;

    /// <summary>
    /// Calculate and generate output data.
    /// </summary>
    public class CalculationManager : ICalculationManager
    {
        public OutputData GenerateOutputData(InputData inputData, List<Tax> taxList, ITaxManager manager)
        {
            var outputData = new OutputData();

            outputData.Name = GetName(inputData.FirstName, inputData.LastName);
            outputData.PayPeriod = inputData.PaymentStartDate;
            outputData.GrossIncome = GetGrossIncome(inputData.AnnualSalary);
            outputData.Super = GetSuper(outputData.GrossIncome, inputData.SuperRate);
            outputData.IncomeTax = manager.CalculateTax(taxList, inputData.AnnualSalary);
            outputData.NetIncome = outputData.GrossIncome - outputData.IncomeTax;

            return outputData;
        }

        private string GetName(string firstName, string lastName)
        {
            return firstName + " " + lastName;
        }

        private int GetGrossIncome(int annualSalary)
        {
            return Convert.ToInt32(annualSalary / 12);
        }

        private int GetSuper(int grossIncome, int super)
        {
            return Convert.ToInt32(grossIncome * super / 100);
        }
    }
}