using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HanoiIA.Strategies
{
    public class RandomStrategy : IStrategy
    {
        protected StateConfiguration StateConfiguration { get; set; }

        private int MaxIterationToReset { get; set; }

        private const int MaxIterationToAbort = 100000;
        private readonly Random random = new Random();

        public event EventHandler<TransitionEventArgs> OnTrantition;

        public event EventHandler<TransitionEventArgs> OnCompleted;

        public event EventHandler<EventArgs> OnAbort;

        public event EventHandler<EventArgs> OnStarted;

        public event EventHandler<EventArgs> OnReset; 

        public RandomStrategy(int maxIterationToReset)
        {
            MaxIterationToReset = maxIterationToReset;
            UsedConfigurations = new List<StateConfiguration>();
        }

        private IList<StateConfiguration> UsedConfigurations { get; set; }

        private int[] GetRandomTowers()
        {
            var randomTower1 = 0;
            var randomTower2 = 0;
            while (!ValidateRandomTowers(new[] { randomTower1, randomTower2 }))
            {

                randomTower1 = random.Next(1, StateConfiguration.NumberOfTowers + 1);
                randomTower2 = random.Next(1, StateConfiguration.NumberOfTowers + 1);
            }

            return new[] { randomTower1, randomTower2 };
        }

        protected virtual bool ValidateRandomTowers(int[] towers)
        {
            if (!StateConfiguration.ExistsPiecesOnTower(towers[0]))
            {

                return false;
            }


            if (towers[0] == 0 || towers[1] == 0)
                return false;
            return towers[0] != towers[1];
        }


        public void SolveHanoi(int numberOfTowers, int numberOfPices)
        {
            OnStarted?.Invoke(this,EventArgs.Empty);
            StateConfiguration = new StateConfiguration(numberOfTowers, numberOfPices);
            var iterations = 0;
            var finalFromTower = 0;
            var finalToTower = 0;
            var curentCallIteration = 0;
            while (!StateConfiguration.IsFinalState() && curentCallIteration < MaxIterationToAbort)
            {
                if (iterations > MaxIterationToReset)
                {
                    OnReset?.Invoke(this,EventArgs.Empty);
                    StateConfiguration = new StateConfiguration(numberOfTowers, numberOfPices);
                    UsedConfigurations.Clear();
                    iterations = 0;
                }

                var randomTowers = GetRandomTowers();
                var transition = new Transition(StateConfiguration, randomTowers[0], randomTowers[1]);
                var auxState = new StateConfiguration(StateConfiguration.State);
                StateConfiguration = transition.NextCurrentState();
                if(!StateConfiguration.Equals(auxState))
                 OnTrantition?.Invoke(transition, new TransitionEventArgs(randomTowers[0], randomTowers[1], StateConfiguration));
                UsedConfigurations.Add(new StateConfiguration(StateConfiguration.State));
                iterations++;
                finalFromTower = randomTowers[0];
                finalToTower = randomTowers[1];
                curentCallIteration++;
            }

            if (curentCallIteration >= MaxIterationToAbort)
            {
                OnAbort?.Invoke(this,EventArgs.Empty);
                return;
            }
            OnCompleted?.Invoke(this, new TransitionEventArgs(finalFromTower, finalToTower, StateConfiguration));
        }

    }
}
