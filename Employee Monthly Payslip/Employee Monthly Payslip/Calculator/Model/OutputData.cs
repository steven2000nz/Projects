namespace Calculator.Model
{
    using FileHelpers;

    /// <summary>
    /// Employee payslip
    /// </summary>
    [IgnoreFirst(1)]
    [DelimitedRecord(",")]
    public class OutputData
    {
        [FieldOrder(3), FieldTitle("gross income")]
        public int GrossIncome;

        [FieldOrder(4), FieldTitle("income tax")]
        public int IncomeTax;

        [FieldOrder(1), FieldTitle("name")]
        public string Name;

        [FieldOrder(5), FieldTitle("net income")]
        public int NetIncome;

        [FieldOrder(2), FieldTitle("pay period")]
        public string PayPeriod;

        [FieldOrder(6), FieldTitle("super")]
        public int Super;
    }
}