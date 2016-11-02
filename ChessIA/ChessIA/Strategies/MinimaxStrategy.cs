using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ChessIA.Strategies
{
    public class MinimaxStrategy : IStrategy
    {
        public Player Player { get; set; }

        private readonly Random random;
        private readonly Board board;
        public MinimaxStrategy(Player player, Board board)
        {
            Player = player;
            this.board = board;
            random = new Random();
        }

        public Move GetMove()
        {
            var playerPices = GetAllCurrentPlayerPieces();
            var movesWithScores = playerPices.Select(GetMinimalMove).ToList();
            var minimalMoves = movesWithScores.Where(m => m != null && m?.Score == movesWithScores.Min(x => x?.Score)).ToList();
            var index = random.Next(0, minimalMoves.Count());
            return minimalMoves[index];
        }

        private Move GetMinimalMove(Piece piece)
        {
            var nextMoves = GetNextMovesFromCurrentPiece(piece);
            nextMoves.ForEach(CalculateMoveScore);
            return nextMoves.FirstOrDefault(m => m.Score == nextMoves.Min(x => x.Score));
        }

        private List<Piece> GetAllCurrentPlayerPieces()
        {
            var pieces = board.GetPieces();
            return pieces.Where(p => p != null && p.Color == Player.PieceColor).ToList();
        }

        private bool IsOponnentPiece(Piece piece, PieceColor oponnentPieceColor)
        {
            if (piece == null)
                return false;
            return piece.Color == oponnentPieceColor;
        }

        private void CalculateMoveScore(Move move)
        {
            int score1 = 10;
            int score2 = 0;
            int score3 = 10;
            if (Player.PieceColor == PieceColor.White)
            {
                int x = move.ToPoint.X;
                int y = move.ToPoint.Y;
                if(x < 8 && y >= 0 && IsOponnentPiece(board.GetPiece(x+1, y-1), PieceColor.Black))
                {
                    score1 = 0;
                }
                x = move.ToPoint.X;
                y = move.ToPoint.Y;
                while (y >= 0 && !IsOponnentPiece(board.GetPiece(x, y), PieceColor.Black))
                {
                    score2++;
                    y--;
                }
                x = move.ToPoint.X;
                y = move.ToPoint.Y;
                if (x >= 0 && y >= 0 && IsOponnentPiece(board.GetPiece(x-1, y-1),PieceColor.Black))
                {
                    score3 = 0;
                }
            }
            else
            {
                int x = move.ToPoint.X;
                int y = move.ToPoint.Y;
                if (x < 8 && y < 8 && IsOponnentPiece(board.GetPiece(x+1, y+1),PieceColor.White))
                {
                    score1 = 0;
                }
                x = move.ToPoint.X;
                y = move.ToPoint.Y;
                while (y < 8 && !IsOponnentPiece(board.GetPiece(x, y), PieceColor.White))
                {
                    score2++;
                    y++;
                }
                x = move.ToPoint.X;
                y = move.ToPoint.Y;
                if (x >= 0 && y < 8 && IsOponnentPiece(board.GetPiece(x-1, y+1), PieceColor.White))
                {
                    score3 = 0;
                }
            }
            move.Score = Math.Min(Math.Min(score1, score2), score3);
        }

        private List<Move> GetNextMovesFromCurrentPiece(Piece piece)
        {
            var moves = new List<Move>();
            var posibleMoves = new List<Move>
            {
                new Move(piece.Position,new Point(piece.Position.X+1,piece.Position.Y+1),piece,Player),
                new Move(piece.Position,new Point(piece.Position.X,piece.Position.Y+1),piece,Player),
                new Move(piece.Position,new Point(piece.Position.X-1,piece.Position.Y+1),piece,Player),
                new Move(piece.Position,new Point(piece.Position.X+1,piece.Position.Y-1),piece,Player),
                new Move(piece.Position,new Point(piece.Position.X,piece.Position.Y-1),piece,Player),
                new Move(piece.Position,new Point(piece.Position.X-1,piece.Position.Y-1),piece,Player)
            };

            foreach (var move in posibleMoves)
            {
                var moveValidator = new MoveValidator(board, move);
                if (moveValidator.Validate())
                    moves.Add(move);
            }

            return moves;
        }
    }
}
