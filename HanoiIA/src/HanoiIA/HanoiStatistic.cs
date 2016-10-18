using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using HanoiIA.Strategies;

namespace HanoiIA
{
    public class HanoiStatistic
    {
        private IStrategy strategy;

        private const int NumberOfCalls = 100;

        public int currentIteration = 0; 

        private int numberOfStepsForCurrentCall;
        private List<int> StepsList { get; set; }
        private StreamWriter streamWriter;
        private int numberOfSolvedCalls;

        public HanoiStatistic(string fileName)
        {
            streamWriter = File.CreateText(fileName);
            numberOfStepsForCurrentCall = 0;
            strategy = new RandomStrategy(100);
            strategy.OnTrantition += Strategy_OnTrantition;
            strategy.OnCompleted += Strategy_OnCompleted;
            strategy.OnAbort += Strategy_OnAbort;
            StepsList = new List<int>();
            numberOfSolvedCalls = 0;
        }

        private void Strategy_OnAbort(object sender, EventArgs e)
        {
            Console.WriteLine($"Not found! Current Iteration: {currentIteration}");
        }

        private void Strategy_OnCompleted(object sender, TransitionEventArgs e)
        {
            StepsList.Add(numberOfStepsForCurrentCall);
            numberOfStepsForCurrentCall = 0;
            numberOfSolvedCalls++;
            Console.WriteLine($"Gasit! Iteration: {currentIteration}");
            e.State.Print();
        }

        private void Strategy_OnTrantition(object sender, TransitionEventArgs e)
        {
            //Console.WriteLine($"Tranitie de la turnul {e.FromTower} la turnul {e.ToTower}");
            //e.State.Print();
            numberOfStepsForCurrentCall++;
        }

        public void Run()
        {
            currentIteration = 0;
            while (currentIteration < NumberOfCalls)
            {
                int[] input = GenerateTowerAndPiecesInput();
                strategy.SolveHanoi(input[0], input[1]);
                currentIteration++;
            }

        }

        public void PrintStatistic()
        {
            Console.WriteLine("Printez statistici");
            var numberNotFoundCase = NumberOfCalls - numberOfSolvedCalls;
            var mean = GetMeanOfSuccesCallsSteps();
            streamWriter.WriteLine($"Numar de cazuri in care nu s-a gasit solutia: {numberNotFoundCase}");
            streamWriter.WriteLine($"Numarul mediu de pasi pentru solutiile gasite: {mean}");
            streamWriter.Dispose();
        }

        private double GetMeanOfSuccesCallsSteps()
        {
            double sum = 0;
            foreach (var el in StepsList)
            {
                sum += el;
            }
            return sum / StepsList.Count;
        }

        private int[] GenerateTowerAndPiecesInput()
        {
            var random = new Random();
            int tower = random.Next(3, 6);
            int pieces = random.Next(2, 10);
            return new int[] { tower, pieces };
        }

    }
}
