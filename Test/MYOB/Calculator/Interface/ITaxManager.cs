
namespace Calculator.Interface
{
    using System.Collections.Generic;

    using Calculator.Model;

    public interface ITaxManager
    {
        int CalculateTax(List<Tax> taxList, int annualSalary);
    }
}
