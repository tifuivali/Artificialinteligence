using System;
using HanoiIA.Strategies;

namespace HanoiIA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int numberOfTowers = 0;
            int numberOfPieces = 0;

            Console.WriteLine("Introduceti numarul de turnuri:");
            numberOfTowers = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti numarul de piese:");
            numberOfPieces = int.Parse(Console.ReadLine());

            RandomStrategy strategy = new RandomStrategy(100);
            strategy.OnTrantition += Strategy_OnTrantition;
            strategy.SolveHanoi(numberOfTowers, numberOfPieces);
        }

        private static void Strategy_OnTrantition(object sender, TransitionEventArgs e)
        {
            Console.WriteLine($"Tranitie de la turnul {e.FromTower} la turnul {e.ToTower}");
        }
    }
}
