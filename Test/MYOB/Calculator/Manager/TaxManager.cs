namespace Calculator.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Calculator.Interface;
    using Calculator.Model;

    /// <summary>
    /// Calculate tax rate
    /// </summary>
    public class TaxManager : ITaxManager
    {
        public int CalculateTax(List<Tax> taxList, int annualSalary)
        {
            var tax = taxList.SingleOrDefault(t => annualSalary >= t.Start && annualSalary <= t.Finish);

            return Fomular(annualSalary, tax);
        }

        private int Fomular(int annualSalary, Tax tax)
        {
            return Convert.ToInt32((tax.Base + (annualSalary - tax.Start - 1) * (tax.Rate / 100)) / 12);
        }
    }
}