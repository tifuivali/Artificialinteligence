namespace ChessIA.Strategies
{
    public class MirrorStrategy : IStrategy
    {
        private readonly Move opponnentLastMove;
        private readonly Board board;
        private readonly Player _currentPlayer;

        public MirrorStrategy(Move opponentLastMove, Board board, Player currentPlayer)
        {
            opponnentLastMove = opponentLastMove;
            this.board = board;
            _currentPlayer = currentPlayer;
        }


        public Move GetMove()
        {
            var move = new Move();
            var toX = opponnentLastMove.ToPoint.X;
            var toY = 8 - opponnentLastMove.ToPoint.Y-1;
            var fromX = opponnentLastMove.FromPoint.X;
            var fromY = 8 - opponnentLastMove.FromPoint.Y-1;
            if (board.GetPiece(fromX, fromY) != null)
            {
                move = new Move(toX, toY, fromX, fromY, board.GetPiece(fromX, fromY), _currentPlayer);
            }
            else
            {
                for (int y = fromY; y < 8; y++)
                {
                    var piece = board.GetPiece(fromX, y);
                    if (piece != null)
                    {
                        move = move = new Move(toX, toY, fromX, y, board.GetPiece(fromX, fromY), _currentPlayer);
                        break;
                    }
                }
                for (int y = fromY; y < 8; y++)
                {
                    var yt = 0;
                    if (_currentPlayer.PieceColor == PieceColor.Black)
                    {
                        yt = y - 1;
                    }
                    else
                    {
                        yt = y + 1;
                    }
                    for (int x1 = fromX, x2 = fromX; x1 > 0 && x2 < 8; x1--,x2++)
                    {
                      
                        var piece1 = board.GetPiece(x1, y);
                        var piece2 = board.GetPiece(x2, y);
                        if (piece1 != null)
                        {
                        
                            return new Move(x1,yt, x1, y, board.GetPiece(x1, y), _currentPlayer);
                        }
                        if (piece2 != null)
                        {
                            return new Move(x2, yt, x2, y, board.GetPiece(x1, y), _currentPlayer);
                        }
                    }
                }

            }
            return move;
        }
    }
}
