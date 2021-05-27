using CSVReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSVReaderUI
{
    class Program
    {
        static void Main(string[] args)
        {

            TestLegume();

            // Keep the console window open in debug mode.
            Console.WriteLine("");
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        public static void TestLegume()
        {
            string path = @"..\..\Legumes.csv";

            List<Legumes> legumes = new List<Legumes>();
            CSVFileReader<Legumes> myFile = new CSVFileReader<Legumes>(path);


            legumes = myFile.GetData();

            foreach (Legumes f in legumes)
            {
                string lol = $"{f.Nom,-10}|{f.Comestible}";
                Console.WriteLine(lol);
            }

            Console.WriteLine("---------------------------");

            Legumes l = new Legumes() { Nom = "Bernard", Provenance = "Nord-ouest", Comestible = true  };
            Legumes m = new Legumes() { Nom = "Mangue", Provenance = "Sud-est", Comestible = false  };

            List<Legumes> fl = new List<Legumes>();
            fl.Add(l);
            fl.Add(m);
            myFile.AddData(fl);

            legumes = myFile.GetData();

            foreach (string h in myFile.Headers)
            {
                Console.Write($"{h,-10}|");
            }
            Console.WriteLine();
            Console.WriteLine("----------+----------+----------+----------+");
            foreach (Legumes f in legumes)
            {
                string lol = $"{f.Nom,-10}|{f.Provenance,-10}|{f.Comestible}";
                Console.WriteLine(lol);
            }
        }

        public static void TestFruit()
        {
            string path = @"..\..\Fruits.csv";

            List<Fruit> fruits = new List<Fruit>();
            CSVFileReader<Fruit> myFile = new CSVFileReader<Fruit>(path);


            fruits = myFile.GetData();

            foreach (Fruit f in fruits)
            {
                string lol = $"{f.Nom,-10}|{f.Prix,-10}";
                Console.WriteLine(lol);
            }

            Console.WriteLine("---------------------------");

            Fruit l = new Fruit() { Nom = "Bernard", Prix = 1000.32, Provenance = "Nord-ouest", Quantite = 150, Inutile = "Wesh" };
            Fruit m = new Fruit() { Nom = "Mangue", Prix = 145, Provenance = "Sud-est", Quantite = 90, Inutile = "Hello there" };

            List<Fruit> fl = new List<Fruit>();
            fl.Add(l);
            fl.Add(m);
            myFile.AddData(fl);

            fruits = myFile.GetData();

            foreach (string h in myFile.Headers)
            {
                Console.Write($"{h,-10}|");
            }
            Console.WriteLine();
            Console.WriteLine("----------+----------+----------+----------+");
            foreach (Fruit f in fruits)
            {
                string lol = $"{f.Nom,-10}|{f.Provenance,-10}|{f.Prix,-10}|{f.Quantite,-10}";
                Console.WriteLine(lol);
            }
        }

    }
}
