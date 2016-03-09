namespace Calculator.Model
{
    using Calculator.Converter;

    using FileHelpers;

    /// <summary>
    /// Employee record
    /// </summary>
    [IgnoreFirst(1)]
    [DelimitedRecord(",")]
    public class InputData
    {
        [FieldOrder(3)]
        public int AnnualSalary;

        [FieldOrder(1)]
        public string FirstName;

        [FieldOrder(2)]
        public string LastName;

        [FieldOrder(5)]
        public string PaymentStartDate;

        [FieldOrder(4)]
        [FieldConverter(typeof(PercentageConverter))]
        public int SuperRate;
    }
}