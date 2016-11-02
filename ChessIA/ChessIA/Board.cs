using System.Collections.Generic;
using System.Linq;

namespace ChessIA
{
    public class Board
    {
        private readonly List<Piece> _pieces;

        public Board()
        {
            _pieces = new List<Piece>();
            for (int i = 0; i < 8*8; i++)
            {
                _pieces.Add(null);
            }
            PopulatePieces();
        }

        public Piece GetPiece(int x, int y)
        {
            int i = y * 8 + x;
            return _pieces[i];
        }

        public List<Piece> GetPieces()
        {
            return _pieces.ToList();
        }

        public void SetPiece(int x, int y, Piece piece)
        {
            int i = y * 8 + x;
            _pieces[i] = piece;
        }

        private void PopulatePieces()
        {
            for (int i = 0; i < 8; i++)
            {
                SetPiece(i, 1, new Piece(PieceType.Pawn, PieceColor.Black));
                SetPiece(i, 6, new Piece(PieceType.Pawn, PieceColor.White));
            }
        }
    }
}
