using System.Drawing;

namespace ChessIA
{
    public class Move
    {
        public Point FromPoint { get; set; }
        public Point ToPoint { get; set; }
        public Piece Piece { get; set; }
        public int Score { get; set; }
        public Player CurrentPlayer { get; set; }

        public Move(int toX, int toY, int fromX, int fromY, Piece piece, Player player)
        {
            FromPoint = new Point(fromX,fromY);
            ToPoint = new Point(toX,toY);
            Piece = piece;
            CurrentPlayer = player;
        }

        public Move(Point fromPoint, Point toPoint, Piece piece, Player player)
        {
            FromPoint = fromPoint;
            ToPoint = toPoint;
            Piece = piece;
            CurrentPlayer = player;
        }

        public Move()
        {
            
        }
    }
}
