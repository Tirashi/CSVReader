using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSVReader.Converters
{
    public class StringConverter : Converter<string>
    {
        public static readonly StringConverter Instance = new StringConverter();

        public override string GetConvertedValue(string value)
        {
            return value;
        }
    }
}
