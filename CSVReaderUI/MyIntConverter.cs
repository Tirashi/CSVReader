using CSVReader.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSVReaderUI
{
    class MyIntConverter: Converter<int>
    {
        public static readonly MyIntConverter Instance = new MyIntConverter();

        public override int GetConvertedValue(string value)
        {
            return 0;
        }
    }
}
