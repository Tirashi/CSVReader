﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CSVReader.Converters.PrimitiveConverters
{
    class FloatConverter : Converter<float>
    {
        public override float GetConvertedValue(string value)
        {
            return float.Parse(value.Replace(',', '.'), CultureInfo.InvariantCulture);
        }
    }
}
