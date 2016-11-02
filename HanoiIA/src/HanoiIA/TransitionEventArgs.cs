using System;

namespace HanoiIA
{
    public class TransitionEventArgs:EventArgs
    {
        public int FromTower { get; set; }
        public int ToTower { get; set; }
        public StateConfiguration State { get; set; }

        public TransitionEventArgs(int fromTower, int toTower, StateConfiguration state)
        {
            FromTower = fromTower;
            ToTower = toTower;
            State = state;
        }
    }
}
