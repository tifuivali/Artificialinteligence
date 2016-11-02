using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ChessIA.Strategies;

namespace ChessIA
{
    public class Game
    {
        private Player CurrentPlayer { get; set; }

        public List<Player> Players { get; set; }

        public Board Board { get; set; }

        public event EventHandler<GameEventArgs> OnGameComplete;

        public event EventHandler<GameEventArgs> OnPlayerChanged;

        public Game()
        {
            Board = new Board();
            Players = new List<Player>();
            Players.Add(new Player { Name = "Georgel", PieceColor = PieceColor.White });
            Players.Add(new Player { Name = "Computer", PieceColor = PieceColor.Black });
        }

        public void Start()
        {
            CurrentPlayer = Players[0];
            OnPlayerChanged?.Invoke(this, new GameEventArgs(CurrentPlayer));
        }

        private void DoComputerMove()
        {
            IStrategy strategy = new MinimaxStrategy(CurrentPlayer, Board);
            var move = strategy.GetMove();
            Move(move);
        }

        private void ChangePlayer()
        {
            CurrentPlayer = CurrentPlayer == Players[0] ? Players[1] : Players[0];
            OnPlayerChanged?.Invoke(this, new GameEventArgs(CurrentPlayer));
            if (CurrentPlayer.Name == "Computer")
            {
                DoComputerMove();
            }
            
        }

        private bool IsFinalState()
        {
            for (int x = 0; x < 7; x++)
            {
                if (Board.GetPiece(x, 0)?.Color == PieceColor.White)
                {
                    return true;
                }
                if (Board.GetPiece(x, 7)?.Color == PieceColor.Black)
                {
                    return true;
                }
            }

            var pieces = Board.GetPieces();
            if (pieces.All(x => x?.Color != PieceColor.White) || pieces.All(x => x?.Color != PieceColor.Black))
            {
                return true;
            }

            return false;
        }

        public void Move(Move move)
        {
            move.CurrentPlayer = CurrentPlayer;
            var moveValidator = new MoveValidator(Board, move);
            if (moveValidator.Validate())
            {
                Board.SetPiece(move.FromPoint.X, move.FromPoint.Y, null);
                Board.SetPiece(move.ToPoint.X, move.ToPoint.Y, move.Piece);
                if (IsFinalState())
                {
                    OnGameComplete?.Invoke(this, new GameEventArgs(CurrentPlayer));
                }
                ChangePlayer();
            }
        }
    }
}
