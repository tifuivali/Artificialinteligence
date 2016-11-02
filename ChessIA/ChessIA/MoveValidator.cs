namespace ChessIA
{
    public class MoveValidator
    {
        private readonly Board _board;
        private readonly Move _move;

        public MoveValidator(Board board, Move move)
        {
            _board = board;
            _move = move;
        }

        public bool Validate()
        {
            if (!IsUserPiece())
            {
                return false;
            }

            var toX = _move.ToPoint.X;
            var toY = _move.ToPoint.Y;
            var fromX = _move.FromPoint.X;
            var fromY = _move.FromPoint.Y;
            var piece = _move.Piece;
            if (IsOutOfBoard(toX, toY))
                return false;

            var existsPiece = _board.GetPiece(toX, toY);
            if (existsPiece != null && existsPiece.Color == piece.Color)
            {
                return false;
            }

            return ValidateMoveForward(toX, toY, fromX, fromY, piece);
        }

        private bool IsUserPiece()
        {
            if (_move.Piece == null)
                return false;
            return _move.Piece.Color == _move.CurrentPlayer.PieceColor;
        }

        private bool IsOutOfBoard(int toX, int toY)
        {
            if (toX < 0 || toX > 7)
                return true;

            return toY < 0 || toY > 7;
        }

        private bool ValidateMoveForward(int toX, int toY, int fromX, int fromY, Piece piece)
        {
            if (piece.Color == PieceColor.Black)
            {

                if (toY <= fromY)
                    return false;
                if (toY > fromY + 1)
                    return false;
                var p = _board.GetPiece(fromX + 1,fromY+1);
                var p1 = _board.GetPiece(fromX - 1, fromY + 1);
                if ((toX == fromX + 1 && p == null) || (toX == fromX-1 && p1 == null))
                    return false;
            }

            if (piece.Color == PieceColor.White)
            {
                if (toY >= fromY)
                    return false;
                if (toY < fromY - 1)
                    return false;
                if (toY > fromY + 1)
                    return false;
                var p = _board.GetPiece(fromX + 1, fromY - 1);
                var p1 = _board.GetPiece(fromX - 1, fromY - 1);
                if ((toX == fromX + 1 && p == null) || (toX == fromX - 1 && p1 == null))
                    return false;

            }
            return true;
        }
    }
}
