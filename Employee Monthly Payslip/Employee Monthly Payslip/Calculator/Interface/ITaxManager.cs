
namespace Calculator.Interface
{
    using System.Collections.Generic;

    using Calculator.Model;

    /// <summary>
    /// Calculate tax rate
    /// </summary>
    public interface ITaxManager
    {
        int CalculateTax(List<Tax> taxList, int annualSalary);
    }
}
