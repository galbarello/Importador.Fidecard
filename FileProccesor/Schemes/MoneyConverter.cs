using System;
using FileHelpers;

namespace FileProccesor.Schemes
{
    public class MoneyConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            return Double.Parse(from);
        }

        public override string FieldToString(object fieldValue)
        {
            // a more elegant option that also works
            return Convert.ToDouble(fieldValue).ToString();
        }
    }
}