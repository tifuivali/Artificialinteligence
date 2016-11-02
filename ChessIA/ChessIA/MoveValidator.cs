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
            var existsPiece = _board.GetPiece(toX, toY);
            if (existsPiece != null && existsPiece.Color == piece.Color)
            {
                return false;
            }
            return ValidateMoveForward(toX, toY, fromX, fromY, piece);
        }

        private bool IsUserPiece()
        {
            return _move.Piece.Color == _move.CurrentPlayer.PieceColor;
        }

        private bool ValidateMoveForward(int toX, int toY, int fromX, int fromY, Piece piece)
        {
            if (piece.Color == PieceColor.Black)
            {
                if (toY <= fromY)
                    return false;
                if (toY > fromY + 1)
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

            }

            return toX <= fromX + 1 && toX >= fromX - 1;
        }
    }
}
