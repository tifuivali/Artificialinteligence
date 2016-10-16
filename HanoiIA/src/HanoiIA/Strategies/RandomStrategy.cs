using System;
using System.Collections.Generic;
using System.Linq;

namespace HanoiIA.Strategies
{
    public class RandomStrategy : IStrategy
    {
        private CurrentState State { get; set; }

        private int MaxIteration { get; set; }

        private readonly Random random = new Random();

        public event EventHandler<TransitionEventArgs> OnTrantition;


        public RandomStrategy(int maxIteration)
        {
            MaxIteration = maxIteration;
        }

        private IList<Transition> UsedTransition { get; set; }

        private int[] GetRandomTowers()
        {
            var randomTower1 = 0;
            var randomTower2 = 0;
            while (ValidateRandomTowers(new[] { randomTower1, randomTower2 }))
            {
                randomTower1 = random.Next(1, State.NumberOfTowers);
                randomTower2 = random.Next(1, State.NumberOfTowers);
            }

            return new[] { randomTower1, randomTower2 };
        }

        private bool ValidateRandomTowers(int[] towers)
        {
            return towers[0] != towers[1] && UsedTransition.All(t => t.FromTower != towers[0] || t.ToTower != towers[1]);
        }


        public void SolveHanoi(int numberOfTowers, int numberOfPices)
        {
            State = new CurrentState(numberOfTowers, numberOfPices);
            var iterations = 0;
            while (!State.IsFinalState())
            {
                if (iterations > MaxIteration)
                {
                    State = new CurrentState(numberOfTowers, numberOfPices);
                }

                var randomTowers = GetRandomTowers();
                var transition = new Transition(State, randomTowers[0], randomTowers[1]);
                State = transition.NextCurrentState();
                OnTrantition?.Invoke(transition, new TransitionEventArgs(randomTowers[0], randomTowers[1]));
                iterations++;
            }
        }

    }
}
