using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSVReader.Converters
{
    /// <summary>
    /// Permet de convertir des chaine de caractère en entier
    /// </summary>
    public class IntConverter : Converter<int>
    {
        public static readonly IntConverter Instance = new IntConverter();

        public override int GetConvertedValue(string value)
        {
            return int.Parse(value);
        }
    }
}
