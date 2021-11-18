using CSVReader.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSVReaderUI
{
    /// <summary>
    /// Modele d'un légume
    /// </summary>
    class Legumes
    {
        public string Nom { get; set; }
        public string Provenance { get; set; }

        [OverrideConverter(typeof(MyBooleanConverter))]
        public bool Comestible { get; set; }
    }
}
