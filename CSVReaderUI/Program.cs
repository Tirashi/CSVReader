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
            string path = @"C:\Users\Rudy\Documents\Perso\Fruits.csv";

            List<Fruit> fruits = new List<Fruit>();
            CSVFileReader<Fruit> myFile = new CSVFileReader<Fruit>(path);


            fruits = myFile.GetData();

            foreach (Fruit f in fruits)
            {
                string lol = $"{f.Nom,-10}|{f.Prix,-10}";
                Console.WriteLine(lol);
            }

            Console.WriteLine("---------------------------");

            Fruit l = new Fruit() { Nom="Bernard", Prix = 1000.25, Provenance="Nord-ouest", Quantite=150, Inutile="Wesh"};
            Fruit m = new Fruit() { Nom = "Mangue", Prix = 145.98, Provenance = "Sud-est", Quantite = 90,Inutile="Hello there" };

            List<Fruit> fl = new List<Fruit>();
            fl.Add(l);
            fl.Add(m);
            myFile.AddData(fl);

            fruits = myFile.GetData();

            foreach (Fruit f in fruits)
            {
                string lol = $"{f.Nom,-10}|{f.Prix,-10}";
                Console.WriteLine(lol);
            }


            // Keep the console window open in debug mode.
            Console.WriteLine("");
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

    }
}
