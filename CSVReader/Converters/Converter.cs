﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSVReader.Converters
{
    /// <summary>
    /// Classe permettant de convertir des chaine de caractère en un type primitif
    /// </summary>
    /// <typeparam name="T">Type primitif dans lequel on veut convertir une chaine</typeparam>
    public abstract class Converter<T>: IConverter
    {
        /// <summary>
        /// Convertie une chaine dans le type <see cref="T"/>
        /// </summary>
        /// <param name="value">Valeur à convertir</param>
        /// <returns></returns>
        public abstract T GetConvertedValue(string value);

        /// <summary>
        /// Convertie une valeur de type <see cref="T"/> en string
        /// </summary>
        /// <param name="value">Valeur à convertir en chaine de caractère</param>
        /// <returns></returns>
        public virtual string GetStringValue(T value)
        {
            return value + "";
        }

        
        string IConverter.GetStringValue(object value)
        {
            
            try
            {
                return GetStringValue((T)Convert.ChangeType(value, typeof(T)));
            }
            catch (InvalidCastException)
            {
                throw new Exception("Impossible to convert" + typeof(object) + "in" + typeof(T));
            }
        }

        object IConverter.GetConvertedValue(string value)
        {
            return GetConvertedValue(value);
        }
    }
}
