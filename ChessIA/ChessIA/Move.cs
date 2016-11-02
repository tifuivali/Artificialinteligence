using System.Drawing;

namespace ChessIA
{
    public class Move
    {
        public Point FromPoint { get; set; }
        public Point ToPoint { get; set; }
        public Piece Piece { get; set; }
        public Player CurrentPlayer { get; set; }
    }
}
