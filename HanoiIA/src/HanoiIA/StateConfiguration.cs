﻿

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
            State = new int[stateVector.Length];
            for (int i = 0; i < stateVector.Length; i++)
            {
                State[i] = stateVector[i];
            }
           
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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(StateConfiguration))
                return false;
            StateConfiguration cfg = (StateConfiguration)obj;
            if (cfg.State.Length != State.Length)
                return false;
            for (int i = 0; i < State.Length; i++)
            {
                if (cfg.State[i] != State[i])
                    return false;
            }

            return true;
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
