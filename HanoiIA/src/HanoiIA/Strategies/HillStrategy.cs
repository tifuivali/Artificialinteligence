namespace HanoiIA.Strategies
{
    public class HillStrategy:RandomStrategy
    {
        private int currentSum;

        public HillStrategy(int maxIterationToReset) : base(maxIterationToReset)
        {
            currentSum = 0;
        }

        protected override bool ValidateRandomTowers(int[] towers)
        {
            var baseValidation = base.ValidateRandomTowers(towers);
            if (HillSum() < currentSum)
            {
                return false;
            }
            return baseValidation;
        }

        private int HillSum()
        {
            int sum = 0;
            for (int i = 1; i < StateConfiguration.State.Length; i++)
            {
                sum += StateConfiguration.State[i];
            }
            return sum;
        }
    }
}
