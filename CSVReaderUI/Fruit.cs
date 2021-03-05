using CSVReader.Attributes;
using CSVReader.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSVReaderUI
{
    /// <summary>
    /// Model d'un fruit
    /// </summary>
    public class Fruit
    {        
        public string Nom { get; set; }
        public string Provenance { get; set; }
        public double Prix { get; set; }

        [OverrideConverter(typeof(IntConverter))]
        public int Quantite { get; set; }
        public string Inutile { get; set; }
    }
}
