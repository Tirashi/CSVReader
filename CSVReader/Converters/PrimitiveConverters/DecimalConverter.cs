using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CSVReader.Converters
{
    class DecimalConverter : Converter<decimal>
    {
        public override decimal GetConvertedValue(string value)
        {
            return decimal.Parse(value.Replace(',', '.'), CultureInfo.InvariantCulture);
        }
    }
}
