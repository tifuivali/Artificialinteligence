using System;

namespace HanoiIA.Strategies
{
    public class BacktrackingStrategy : IStrategy
    {
        public event EventHandler<TransitionEventArgs> OnTrantition;
        public event EventHandler<TransitionEventArgs> OnCompleted;
        public event EventHandler<EventArgs> OnAbort;
        public event EventHandler<EventArgs> OnStarted;
        private StateConfiguration state;
        private Transition transition;
        private int numberOfTowers;
        private int numberOfIterations;
        private const int MaxIteration = 100000;

        public void SolveHanoi(int numberOfTowers, int numberOfPices)
        {
            OnStarted?.Invoke(this,EventArgs.Empty);
            this.numberOfTowers = numberOfTowers;
            state = new StateConfiguration(numberOfTowers, numberOfPices);
            numberOfIterations = 0;
            Bk(1);
        }

        private void Bk(int k)
        {
            Console.WriteLine("K=" +k);
            numberOfIterations++;
            if (numberOfIterations >= MaxIteration)
            {
                OnAbort?.Invoke(this,EventArgs.Empty);
                return;
            }

            for (int i = 1; i <= numberOfTowers; i++)
            {
                for (int j = k+1; j <= numberOfTowers; j++)
                {
                    var auxState = new StateConfiguration(state.State);
                    var transition = new Transition(state, i, j);
                    state = transition.NextCurrentState();

                    if (!state.Equals(auxState))
                    {
                        OnTrantition?.Invoke(this, new TransitionEventArgs(k, i, state));
                        if (state.IsFinalState())
                        {
                            OnCompleted?.Invoke(this, new TransitionEventArgs(k, i, state));
                        }
                        else
                        {
                            Bk(k + 1);
                        }
                    }
                }
            }
        }
    }
}
