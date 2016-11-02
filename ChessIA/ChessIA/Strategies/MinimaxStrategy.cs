namespace ChessIA.Strategies
{
    public class MinimaxStrategy : IStrategy
    {
        public Player Player { get; set; }

        public MinimaxStrategy(Player player, Board board)
        {
            Player = player;
        }
        public Move GetMove()
        {
            
        }
    }
}
