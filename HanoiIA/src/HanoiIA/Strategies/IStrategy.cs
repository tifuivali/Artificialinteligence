using System;

namespace HanoiIA.Strategies
{
    public interface IStrategy
    {
        void SolveHanoi(int numberOfTowers, int numberOfPices);
        event EventHandler<TransitionEventArgs> OnTrantition;
    }
}
