

using System;

namespace HanoiIA
{
    public class StateConfiguration
    {
        public int[] State { get; set; }

        public int NumberOfTowers { get; set; }

        public int NumberOfPices { get; set; }

        public StateConfiguration(int[] stateVector)
        {
            State = stateVector;
            if (!(stateVector?.Length >= 0)) return;
            NumberOfTowers = stateVector[0];
            NumberOfPices = stateVector.Length - 1;
        }

        public StateConfiguration(int numerOfTowers, params int[] states)
        {
            var currentsState = new int[states.Length + 1];
            currentsState[0] = numerOfTowers;
            for (int i = 1; i < currentsState.Length; i++)
            {
                currentsState[i] = states[i];
            }
        }

        public StateConfiguration(int n, int m)
        {
            Initialize(n, m);
        }

        private void Initialize(int n, int m)
        {
            NumberOfTowers = n;
            NumberOfPices = m;
            State = new int[m + 1];
            State[0] = n;
            for (var i = 1; i < m + 1; i++)
            {
                State[i] = 1;
            }
        }


        public void Print()
        {
            var line = "";
            foreach (var el in State)
            {
                line = $"{line},{el}";
            }

            Console.WriteLine($"({line})");
        }

        public static void Print(int[] state)
        {
            var line = "";
            foreach (var el in state)
            {
                line = $"{line},{el}";
            }

            Console.WriteLine($"({line})");
        }

        public bool ExistsPiecesOnTower(int tower)
        {
            for (int i = 1; i < State.Length; i++)
            {
                if (State[i] == tower)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsFinalState()
        {
            foreach (var el in State)
            {
                if (el != NumberOfTowers)
                    return false;
            }
            return true;
        }
    }
}
