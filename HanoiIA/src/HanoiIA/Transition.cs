using System;

namespace HanoiIA
{
    public class Transition
    {
        public StateConfiguration StateConfiguration { get; set; }

        public int FromTower { get; set; }

        public int ToTower { get; set; }

        public Transition(StateConfiguration stateConfiguration, int fromTower, int toTower)
        {
            StateConfiguration = stateConfiguration;
            FromTower = fromTower;
            ToTower = toTower;
        }


        public StateConfiguration NextCurrentState()
        {

            if (Validation())
            {
                var k = GetFirstPiecesOfTower(FromTower);
                StateConfiguration.State[k] = ToTower;
            }
            return StateConfiguration;
        }


        private bool Validation()
        {
            var k = GetFirstPiecesOfTower(FromTower);
            if(k==-1 || k == 0)
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
            for (int i = 1; i < StateConfiguration.State.Length;i++)
            {
                if (StateConfiguration.State[i] == tower)
                    return i;
            }

            return -1;
        }
    }
}
