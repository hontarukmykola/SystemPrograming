using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;

namespace _06_practice_DZ
{
    class Statistic
    {
        public static int Words { get; set; } =0;
        public static int Lines { get; set; } =0;
        public static int Letters { get; set; } =0;
        public static int Sumbol { get; set; } =0;




        public static int Punctuation { get; set; } = 0;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Statistic totalStatistic = new Statistic();

            string[] files = Directory.GetFiles("C://Users//Mykola//Desktop//test");
            foreach (var file in files)
            {
                    string text = File.ReadAllText(file);

                    Thread thread = new Thread(TextAnalyse);
                    thread.Start(text);
            }
            Console.ReadKey();
            PrintResult();
        }
        static void TextAnalyse(object text)
        {
            
            string pattern = @"\w+";
            string textAnalyze = (string)text;
            lock (typeof(Statistic))
            {
                MatchCollection coll = Regex.Matches(textAnalyze, pattern);
                Statistic.Words += coll.Count();

                pattern = @"\w";
                coll = Regex.Matches(textAnalyze, pattern);
                Statistic.Letters += coll.Count();


                pattern = @"[.!@#$%^&*(),?/<>{}';]";
                coll = Regex.Matches(textAnalyze, pattern);
                Statistic.Sumbol += coll.Count();


                pattern = @"\n";
                coll = Regex.Matches(textAnalyze, pattern);
                Statistic.Lines += coll.Count()+1;



            }
        }
        static void PrintResult()
        {
            Console.WriteLine($"Count words of all files : {Statistic.Words}");
            Console.WriteLine($"Count Letters of all files : {Statistic.Letters}");
            Console.WriteLine($"Count Lines of all files : {Statistic.Lines}");
            Console.WriteLine($"Count Sumbol of all files : {Statistic.Sumbol}");


        }
    }
}