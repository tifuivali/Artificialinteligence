using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using HanoiIA.Strategies;

namespace HanoiIA
{
    public class HanoiStatistic
    {
        private IStrategy strategy;

        private const int NumberOfCalls = 100;
        private DateTime StartTimeCurentCall;
        public int currentIteration = 0;
        private int numberOfStepsForCurrentCall;
        private List<int> StepsList { get; }
        private List<TimeSpan> ExecutionTimes { get; }
        private StreamWriter streamWriter;
        private int numberOfSolvedCalls;

        public HanoiStatistic(string fileName)
        {
            streamWriter = File.CreateText(fileName);
            numberOfStepsForCurrentCall = 0;
            //strategy = new RandomStrategy(100);
            //strategy = new BacktrackingStrategy();
            strategy = new HillStrategy(100);
            strategy.OnTrantition += Strategy_OnTrantition;
            strategy.OnCompleted += Strategy_OnCompleted;
            strategy.OnAbort += Strategy_OnAbort;
            strategy.OnStarted += Strategy_OnStarted;
            strategy.OnReset += Strategy_OnReset;
            StepsList = new List<int>();
            ExecutionTimes = new List<TimeSpan>();
            numberOfSolvedCalls = 0;
        }

        private void Strategy_OnReset(object sender, EventArgs e)
        {
            numberOfStepsForCurrentCall = 0;
        }

        private void Strategy_OnStarted(object sender, EventArgs e)
        {
            StartTimeCurentCall = DateTime.Now;
        }

        private void Strategy_OnAbort(object sender, EventArgs e)
        {
            var executionTime = DateTime.Now - StartTimeCurentCall;
            Console.WriteLine($"Not found! Current Iteration: {currentIteration}");
            Console.WriteLine($"Timp de executie:{executionTime}");
        }

        private void Strategy_OnCompleted(object sender, TransitionEventArgs e)
        {
            var executionTime = DateTime.Now - StartTimeCurentCall;
            ExecutionTimes.Add(executionTime);
            StepsList.Add(numberOfStepsForCurrentCall);
            numberOfStepsForCurrentCall = 0;
            numberOfSolvedCalls++;
            Console.WriteLine($"Gasit! Iteration: {currentIteration}");
            e.State.Print();
            Console.WriteLine($"Timp de executie:{executionTime}");
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
            var executionTimeMean = GetMeanExecutionTime();
            streamWriter.WriteLine($"Numar de cazuri in care nu s-a gasit solutia: {numberNotFoundCase}");
            streamWriter.WriteLine($"Numarul mediu de pasi pentru solutiile gasite: {mean}");
            streamWriter.WriteLine($"Timp mediu de executie pentru solutiile gasite: {executionTimeMean.ToString("c")}");
            streamWriter.Dispose();
            Console.WriteLine("Gata!");
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

        private TimeSpan GetMeanExecutionTime()
        {
            TimeSpan sum;
            sum = ExecutionTimes.Aggregate(sum, (current, executionTime) => current + executionTime);
            var ticks = sum.Ticks / ExecutionTimes.Count;
            var result = new TimeSpan(ticks);
            return result;
        }

        private int[] GenerateTowerAndPiecesInput()
        {
            var random = new Random();
            var tower = random.Next(3, 6);
            var pieces = random.Next(2, 10);
            return new int[] { tower, pieces };
        }

    }
}
