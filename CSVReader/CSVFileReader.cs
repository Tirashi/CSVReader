using CSVReader.Attributes;
using CSVReader.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CSVReader
{
    /// <summary>
    /// Classe de lecture des fichier CSV
    /// </summary>
    public class CSVFileReader<T>
    {
        private char _separator;
        private string _filePath;

        public List<string> Headers { get; private set; }

        /// <summary>
        /// Mapping des types primitif et de leur class de conversion par défaut
        /// </summary>
        private static readonly Dictionary<Type, Type> _typeMap;

        /// <summary>
        /// Pool d'objet pour effectuer les convertion
        /// </summary>
        private static readonly List<IConverter> _converters;

        /// <summary>
        /// Constructeur static pour instancier les pool et map de type
        /// </summary>
        static CSVFileReader()
        {
            _typeMap = new Dictionary<Type, Type>
            {
                [typeof(int)] = typeof(IntConverter),
                [typeof(double)] = typeof(DoubleConverter),
                [typeof(string)] = typeof(StringConverter),
                [typeof(decimal)] = typeof(DecimalConverter),
                [typeof(float)] = typeof(FloatConverter)
            };

            _converters = new List<IConverter>
            {
                IntConverter.Instance,
                DoubleConverter.Instance,
                StringConverter.Instance,
                DecimalConverter.Instance,
                FloatConverter.Instance
            };
        }

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="path">Chemin vers le fichier CSV</param>
        /// <param name="separator">Séparateur à utiliser</param>
        public CSVFileReader(string path, char separator = ';')
        {
            _filePath = path;
            _separator = separator;

            if (File.Exists(path))
            {
                GetHeaderFromFile();
            }
            else
            {
                GetHeaderFromModel();
                CreateCSVFile();
            }

        }

        /// <summary>
        /// Met à jour la liste des headers en fonction du model de deonnée
        /// </summary>
        public void GetHeaderFromModel()
        {
            Headers = new List<string>();
            foreach (var prop in typeof(T).GetProperties())
            {
                //Console.WriteLine("{0}", prop.Name);
                Headers.Add(prop.Name);
            }
        }

        /// <summary>
        /// Créé le fichier CSV contenant seulement les headers, si le fichier existe déjà, il est écrasé;
        /// </summary>
        private void CreateCSVFile()
        {
            string header = ConvertHeaderToCSV();
            File.WriteAllText(_filePath, header);
        }

        /// <summary>
        /// Transforme la liste des headers en une ligne du fichier CSV
        /// </summary>
        /// <returns>Une chaine rerésentant la première ligne du fichier CSV</returns>
        public string ConvertHeaderToCSV()
        {
            string line = ConvertListToCSVLine(Headers);
            return line;
        }

        /// <summary>
        /// Renvoi les donées sous la forme d'une liste du model 
        /// </summary>
        /// <returns></returns>
        public List<T> GetData()
        {
            List<T> modelList = new List<T>();
            List<string> lines = GetAllLines();

            foreach (string line in lines)
            {
                if (line != lines.First())
                {
                    modelList.Add(GetModelFromLine(line));
                }
            }

            return modelList;
        }

        /// <summary>
        /// Renvoi un model en fonction de la line du fichier CSV
        /// </summary>
        /// <param name="line">Une ligne du fichier CSV</param>
        /// <returns></returns>
        private T GetModelFromLine(string line)
        {
            T model = (T)Activator.CreateInstance(typeof(T));

            List<string> values = line.Split(_separator).ToList();

            foreach (string v in values)
            {
                int headerIndex = values.IndexOf(v);

                if (headerIndex >= Headers.Count)
                {
                    continue;
                }

                PropertyInfo prop = typeof(T).GetProperty(Headers[headerIndex]);

                var obj = prop.GetCustomAttributes(typeof(OverrideConverterAttribute), true);

                Type classConverter;

                if (obj.Length > 0)
                {
                    classConverter = (obj[0] as OverrideConverterAttribute).Type;
                }
                else if (!_typeMap.TryGetValue(prop.PropertyType, out classConverter))
                {
                    //classConverter = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => typeof(Converter<>).MakeGenericType(prop.PropertyType).IsAssignableFrom(t));

                    throw new Exception("No converter found for this type");

                }


                var instanceConverter = GetOrCreateConverter(classConverter);

                prop.SetValue(model, instanceConverter.GetConvertedValue(v), null);

            }

            return model;
        }

        /// <summary>
        /// Affiche tout le contenu du fichier CSV dans la concole
        /// </summary>
        public void OpenCSV()
        {

            List<string> lines = GetAllLines();

            // Display the file contents by using a foreach loop.
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// Insère les headers du fichier CSV dans l'attribut Headers
        /// </summary>
        public void GetHeaderFromFile()
        {
            string line = File.ReadLines(_filePath).First();
            Headers = line.Split(_separator).ToList();
        }

        /// <summary>
        /// Renvoi la liste des valeurs dans la colone à l'indexe donné
        /// </summary>
        /// <param name="index">Index de la colonne dont on veut les valeurs</param>
        /// <returns></returns>
        public List<string> GetColValuesWithHeader(int index)
        {
            List<string> columns = new List<string>();

            List<string> lines = GetAllLines();

            foreach (string line in lines)
            {
                List<string> col = line.Split(_separator).ToList();
                if (index < col.Count)
                {
                    columns.Add(col[index]);
                }
            }

            return columns;
        }

        /// <summary>
        /// Renvoi la liste des valeurs dans la colone à l'indexe donné
        /// </summary>
        /// <param name="index">Index de la colonne dont on veut les valeurs</param>
        /// <returns></returns>
        public List<string> GetColValues(int index)
        {
            List<string> columns = GetColValuesWithHeader(index);
            List<string> res = new List<string>();

            foreach (string s in columns)
            {
                if (s != columns.First())
                {
                    res.Add(s);
                }
            }

            return res;
        }

        /// <summary>
        /// Renvoi une liste contenant chaque ligne du fichier CSV
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllLines()
        {
            List<string> lines = new List<string>();

            try
            {
                lines = File.ReadAllLines(_filePath).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lines;
        }

        /// <summary>
        /// Converti un model en une ligne du fichier CSV
        /// </summary>
        /// <param name="model">Model à convertir</param>
        /// <returns></returns>
        private string ConvertModelToCSVString(T model)
        {
            string[] arrStr = OrderModelValues(model);
            string res = ConvertListToCSVLine(arrStr.ToList());

            return res;
        }

        /// <summary>
        /// Renvoi un array qui contient les valeur du model mis dans l'ordre à partir de l'ordre des headers
        /// </summary>
        /// <param name="model">Model dont on veu les valeur dans le bon ordre</param>
        /// <returns>un array de string contenant les valeur du model dans le bon ordre</returns>
        private string[] OrderModelValues(T model)
        {
            string[] arr = GetNewArray(Headers.Count);
            arr = GetOrdonedValues(model, arr);

            return arr;
        }

        /// <summary>
        /// Renvoi un array de string de la taille souhaité, 
        /// met toutes les string de l'array égal à ""
        /// </summary>
        /// <param name="size">Taille de l'array voulu</param>
        /// <returns></returns>
        private string[] GetNewArray(int size)
        {
            string[] arr = new string[size];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = "";
            }

            return arr;
        }

        /// <summary>
        /// Met les valeur du model dans le tableau de string en respectant l'ordre donné par les headers
        /// </summary>
        /// <param name="model">Model dont on veut les valeurs dans le bon ordre</param>
        /// <param name="arr">Array où mettre les valeurs</param>
        /// <returns></returns>
        private string[] GetOrdonedValues(T model, string[] arr)
        {
            foreach (var prop in model.GetType().GetProperties())
            {
                // Console.WriteLine("{0}={1}", prop.Name, prop.GetValue(model, null));

                int index = Headers.FindIndex(a => a == prop.Name);

                if (index > -1 && index < arr.Length)
                {
                    arr[index] = prop.GetValue(model, null) + "";
                }

            }

            return arr;
        }

        /// <summary>
        /// Converti une liste de string en une ligne de fichier CSV
        /// </summary>
        /// <param name="listToConvert">Liste à convertir</param>
        /// <returns>Ligne à ajouter dans un fichier CVS</returns>
        private string ConvertListToCSVLine(List<string> listToConvert)
        {
            string res = "";

            foreach (string s in listToConvert)
            {
                res += $"{s}";

                if (s != listToConvert.Last())
                {
                    res += $"{_separator}";
                }
            }

            return res;
        }

        /// <summary>
        /// Ajoute le model dans le fichier CSV
        /// </summary>
        /// <param name="model">Donné à ajouter</param>
        public void AddData(T model)
        {
            string line = ConvertModelToCSVString(model);

            try
            {
                File.AppendAllText(_filePath, $"{Environment.NewLine}{line}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Ajoute une liste de model dans le fichier CSV
        /// </summary>
        /// <param name="models">List de model à ajouter</param>
        public void AddData(List<T> models)
        {
            foreach (T model in models)
            {
                AddData(model);
            }
        }

        /// <summary>
        /// Ecrase le fichier avec un nouveau contenant le model
        /// </summary>
        /// <param name="model">Model à écrire</param>
        public void WriteData(T model)
        {
            CreateCSVFile();
            AddData(model);
        }

        /// <summary>
        /// Ecrase le fichier avec un nouveau contenant la liste de model
        /// </summary>
        /// <param name="model">Liste de model à écrire</param>
        public void WriteData(List<T> model)
        {
            CreateCSVFile();
            AddData(model);
        }

        /// <summary>
        /// Permet de récupérer un converter depuis le pool, ajoute le converter au pool si il n'existe pas
        /// </summary>
        /// <param name="converterType">Type de converter que l'on veut</param>
        /// <returns></returns>
        private static IConverter GetOrCreateConverter(Type converterType)
        {
            IConverter res = _converters.FirstOrDefault(c => c.GetType() == converterType);

            if (res == null)
            {
                res = (IConverter)Activator.CreateInstance(converterType);
                _converters.Add(res);
            }
            return res;
        }
    }
}
