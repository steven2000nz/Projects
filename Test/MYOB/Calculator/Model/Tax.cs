namespace Calculator.Model
{
    using FileHelpers;

    /// <summary>
    /// Tax table used for calculation
    /// </summary>
    [IgnoreFirst(1)]
    [DelimitedRecord(",")]
    public class Tax
    {
        [FieldOrder(3)]
        public int Base;

        [FieldOrder(2)]
        public int Finish;

        [FieldOrder(4)]
        public decimal Rate;

        [FieldOrder(1)]
        public int Start;
    }
}