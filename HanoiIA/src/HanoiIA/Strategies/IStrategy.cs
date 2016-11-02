using System;

namespace HanoiIA.Strategies
{
    public interface IStrategy
    {
        void SolveHanoi(int numberOfTowers, int numberOfPices);
        event EventHandler<TransitionEventArgs> OnTrantition;
        event EventHandler<TransitionEventArgs> OnCompleted;
        event EventHandler<EventArgs> OnAbort;
        event EventHandler<EventArgs> OnStarted;
        event EventHandler<EventArgs> OnReset;
    }
}
