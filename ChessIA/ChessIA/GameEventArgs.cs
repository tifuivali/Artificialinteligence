using System;

namespace ChessIA
{
    public class GameEventArgs: EventArgs
    {
        public Player Player { get; set; }

        public GameEventArgs(Player player)
        {
            Player = player;
        }
    }
}
