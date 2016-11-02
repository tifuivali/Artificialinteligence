using System;

namespace HanoiIA.Strategies
{
    public class BacktrackingStrategy : IStrategy
    {
        public event EventHandler<TransitionEventArgs> OnTrantition;
        public event EventHandler<TransitionEventArgs> OnCompleted;
        public event EventHandler<EventArgs> OnAbort;
        public event EventHandler<EventArgs> OnStarted;
        public event EventHandler<EventArgs> OnReset;
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
            Bk(state,1);
        }



        private void Bk(StateConfiguration state, int k)
        {
            numberOfIterations++;
            if (numberOfIterations >= MaxIteration)
            {
                OnAbort?.Invoke(this,EventArgs.Empty);
                return;
            }

            for (int i = 1; i <= numberOfTowers; i++)
            {
                for (int j = 1; j <= numberOfTowers; j++)
                {
                    if (i != j)
                    {
                        var auxState = new StateConfiguration(state.State);
                        var copyState = new StateConfiguration(state.State);
                        var transition = new Transition(copyState, i, j);
                        copyState = transition.NextCurrentState();

                        if (!copyState.Equals(auxState))
                        {
                            OnTrantition?.Invoke(this, new TransitionEventArgs(k, i, state));
                            if (state.IsFinalState())
                            {
                                OnCompleted?.Invoke(this, new TransitionEventArgs(k, i, state));
                            }

                        }
                    }
                }
            }
        }
    }
}
