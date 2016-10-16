using System;

namespace HanoiIA
{
    public class Transition
    {
        public CurrentState CurrentState { get; set; }

        public int FromTower { get; set; }

        public int ToTower { get; set; }

        public Transition(CurrentState currentState, int fromTower, int toTower)
        {
            CurrentState = currentState;
            FromTower = fromTower;
            ToTower = toTower;
        }


        public CurrentState NextCurrentState()
        {
            if (Validation())
            {
                var k = GetFirstPiecesOfTower(FromTower);
                CurrentState.State[k] = ToTower;
            }
            return CurrentState;
        }


        private bool Validation()
        {
            var k = GetFirstPiecesOfTower(FromTower);
            if(k==-1 || k != 0)
                return false;
            var t = GetFirstPiecesOfTower(ToTower);
            if (t > 0)
            {
                if (t < k)
                    return false;
            }
            return true;
        }

        private int GetFirstPiecesOfTower(int tower)
        {
            for (int i = 1; i < CurrentState.State.Length;i++)
            {
                if (CurrentState.State[i] == tower)
                    return i;
            }

            return -1;
        }
    }
}
