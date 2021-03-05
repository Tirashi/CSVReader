using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSVReader.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class OverrideConverterAttribute: Attribute
    {
        public Type Type { get; }

        public OverrideConverterAttribute(Type type)
        {
            Type = type;
        }
    }
}
