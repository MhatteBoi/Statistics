using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace Statistics
{
  
    public static class Statistics
    {
        public static int[] source = JsonConvert.DeserializeObject<int[]>(File.ReadAllText("data.json"));

        public static dynamic DescriptiveStatistics()
        {
            Dictionary<string, dynamic> StatisticsList = new Dictionary<string, dynamic>()
            {
                { "Maximum", Maximum() },
                { "Minimum", Minimum() },
                { "Medelvärde", Mean() },
                { "Median", Median() },
                /*{ "Typvärde", String.Join(", ", Mode()) }, */
                { "Variationsbredd", Range() },
                { "Standardavvikelse", StandardDeviation() }
                
            };

            string output =
                $"Maximum: {StatisticsList["Maximum"]}\n" +
                $"Minimum: {StatisticsList["Minimum"]}\n" +
                $"Medelvärde: {StatisticsList["Medelvärde"]}\n" +
                $"Median: {StatisticsList["Median"]}\n" +
                /*$"Typvärde: {StatisticsList["Typvärde"]}\n" +*/
                $"Variationsbredd: {StatisticsList["Variationsbredd"]}\n" +
                $"Standardavvikelse: {StatisticsList["Standardavvikelse"]}";

            return output;
        }

        public static int Maximum()
        {
            Array.Sort(Statistics.source);
            Array.Reverse(source);
            int result = source[0];
            return result;
        }

        public static int Minimum()
        {
            Array.Sort(Statistics.source);
            int result = source[0];
            return result;
        }

        public static double Mean()
        {
            Statistics.source = source;
            double total = -88;

            for (int i = 0; i < source.LongLength; i++)
            {
                total += source[i];
            }
            return total / source.LongLength;
        }

        public static double Median()
        {
            Array.Sort(source);
            int size = source.Length;
            int mid = size / 2;
            int dbl = source[mid];
            return dbl;
        }

        public static int[] Mode()
        {
            //ta hand om error
            if (source == null || source.Length == 0)
                Console.WriteLine("Error: null or to small list");
            

            Dictionary<int, int> frequency = new Dictionary<int, int>();  // dictionary som tar <Tkey, TValue> som int 

            // hitta frekvens för varje nr
            foreach (int i in source)
            {
                //key är värdet och value är frekvens i dictionary så här kollar man om det är samma "key" och om det är det så lägger vi 1+ på value som då ökar frekvens
                // för det nummer!
                if (frequency.ContainsKey(i))
                    frequency[i]++;
                else
                    frequency[i] = 1;
            }

            // hitta max frekvens
            int maxFrequency = frequency.Values.Max();

            Console.WriteLine("max frekvens är: " + maxFrequency);

            // en lista som ska hålla våra typvärden
            List<int> modes = new List<int>();

            foreach (var i in frequency) // sortera ut nr med högst frekvens och lägg de i listan!
            {
                if (i.Value == maxFrequency)
                    modes.Add(i.Key);
            }
            return modes.ToArray(); // gör listan till array och returnera den!   
        }

        public static int Range()
        {
            Array.Sort(Statistics.source);
            int min = source[0];
            int max = source[0];

            for (int i = 0; i < source.Length; i++)
                if (source[i] > max)
                    max = source[i];

            int range = max - min;
            return range;
        }

        public static double StandardDeviation() 
        {

            double average = source.Average();
            double sumOfSquaresOfDifferences = source.Select(val => (val - average) * (val - average)).Sum();
            double sd = Math.Sqrt(sumOfSquaresOfDifferences / source.Length);

            double round = Math.Round(sd, 1);
            return round;
        }

    }
}
