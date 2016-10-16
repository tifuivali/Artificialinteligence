using System;

namespace HanoiIA
{
    public class TransitionEventArgs:EventArgs
    {
        public int FromTower { get; set; }
        public int ToTower { get; set; }

        public TransitionEventArgs(int fromTower, int toTower)
        {
            FromTower = fromTower;
            ToTower = toTower;
        }
    }
}
