using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSVReader.Converters
{
    /// <summary>
    /// Interface de base pour les convertions
    /// </summary>
    internal interface IConverter
    {
        /// <summary>
        /// Permet de convertir la valeur en string dans le bon type
        /// </summary>
        /// <param name="value">Valeur à convertir</param>
        /// <returns>Valeur convertie</returns>
        object GetConvertedValue(string value);

        /// <summary>
        /// Convertie une valeur de type <see cref="T"/> en string
        /// </summary>
        /// <param name="value">Valeur à convertir en chaine de caractère</param>
        /// <returns></returns>
        string GetStringValue(object value);
    }
}
