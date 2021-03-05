using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CSVReader.Converters
{
    public class DoubleConverter : Converter<double>
    {
        public static readonly DoubleConverter Instance = new DoubleConverter();

        public override double GetConvertedValue(string value)
        {
            return double.Parse(value.Replace(",", "."), CultureInfo.InvariantCulture);
        }
    }
}
