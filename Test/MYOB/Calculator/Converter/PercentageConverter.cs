namespace Calculator.Converter
{
    using System;
    using System.Globalization;

    using FileHelpers;

    internal class PercentageConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            return Convert.ToInt32(from.Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, ""));
        }
    }
}