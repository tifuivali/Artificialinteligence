using System;
using HanoiIA.Strategies;

namespace HanoiIA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HanoiStatistic hanoiStatistic = new HanoiStatistic(@"C:\Users\tifui\Downloads\statistic.txt");
            hanoiStatistic.Run();
           // hanoiStatistic.PrintStatistic();
           Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
