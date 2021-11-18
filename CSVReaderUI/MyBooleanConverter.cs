using CSVReader.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSVReaderUI
{
    class MyBooleanConverter: Converter<bool>
    {
        public static readonly MyBooleanConverter Instance = new MyBooleanConverter();

        public override bool GetConvertedValue(string value)
        {
            if(value.ToLower() == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string GetStringValue(bool value)
        {
            if(value)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
    }
}
