using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ChessIA.Properties;

namespace ChessIA
{
    public partial class Table : Form
    {
        public Table()
        {
            InitializeComponent();
            ChessTablePicture.MouseDown += Table_MouseDown;
        }

        #region Properties

        private Game Game { get; set; }
        private Piece CurrentPiece { get; set; }
        private Dictionary<Piece, Bitmap> PieceBitmaps { get; set; }
        private int TileWidth { get; set; }
        private int TileHeight { get; set; }
        private Move Move { get; set; }

        #endregion

        #region Events

        private void Table_OnLoad(object sender, EventArgs e)
        {
            InitializeGame();
            DrawGame();
        }

        private void Table_MouseDown(object sender, MouseEventArgs e)
        {
            PickOrDropPiece(e);
            DrawGame();
        }

        #endregion

        #region Methods

        private void InitializeGame()
        {
            TileWidth = 64;
            TileHeight = 64;

            Game= new Game();
            Game.OnPlayerChanged += Game_OnPlayerChanged;
            Game.OnGameComplete += Game_OnGameComplete;     
            Game.Start();
            Move = new Move();
            PieceBitmaps = new Dictionary<Piece, Bitmap>();
            PieceBitmaps.Add(new Piece(PieceType.Pawn, PieceColor.Black), new Bitmap(Resources.pawn_gold));
            PieceBitmaps.Add(new Piece(PieceType.Pawn, PieceColor.White), new Bitmap(Resources.pawn_white));
        }

        private void Game_OnGameComplete(object sender, GameEventArgs e)
        {
            if (MessageBox.Show($"Felicitari {e.Player.Name} ati castigat!", "Uraaaaa!", MessageBoxButtons.OK) ==
                DialogResult.OK)
            {
                InitializeGame();
            }
        }

        private void Game_OnPlayerChanged(object sender, GameEventArgs e)
        {
            PlayerName.Text = e.Player.Name;
            PlayerColor.Text = e.Player.PieceColor.ToString();
            if (e.Player.PieceColor == PieceColor.Black)
            {
                PlayerColor.ForeColor= Color.Black;
            }
            else
            {
                PlayerColor.ForeColor = Color.BurlyWood;
            }
        }

        private void DrawGame()
        {
            var tileSize = new Size(TileWidth, TileHeight);
            Bitmap bitmap = CreateBoard(tileSize);
            ChessTablePicture.Image = bitmap;
            DrawPieces(bitmap);
        }

        private Bitmap CreateBoard(Size tileSize)
        {
            int tileWidth = tileSize.Width;
            int tileHeight = tileSize.Height;
            var bitmap = new Bitmap(tileWidth * 8, tileHeight * 8);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        Brush brush = (x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0) ? Brushes.Black : Brushes.White;
                        graphics.FillRectangle(brush, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                    }
                }
            }
            return bitmap;
        }

        private void DrawPieces(Bitmap bitmap)
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Board board = Game.Board;
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        Piece piece = board.GetPiece(x, y);
                        if (piece != null)
                        {
                            Bitmap bitmap1 = PieceBitmaps[piece];

                            graphics.DrawImageUnscaled(bitmap1, new Point(x * TileWidth, y * TileHeight));
                        }
                    }
                }
            }
        }

        private void PickOrDropPiece(MouseEventArgs e)
        {
            Point location = e.Location;
            int x = location.X / TileWidth;
            int y = location.Y / TileHeight;
            bool pickOrDrop = CurrentPiece == null;
            if (pickOrDrop)
            {
                // Pick a piece
                Piece piece = Game.Board.GetPiece(x, y);
                if (piece != null)
                {
                    LabelStatus.Text = string.Format("You picked a {0} {1} at location {2},{3}", piece.Color, piece.Type, x,
                        y);
                    Move.FromPoint = new Point(x,y);
                }
                else
                {
                    LabelStatus.Text = "Nothing there !";
                }
                CurrentPiece = piece;
                Move.Piece = CurrentPiece;
            }
            else
            {
                // Drop picked piece
                LabelStatus.Text = string.Format("You dropped a {0} {1} at location {2},{3}", CurrentPiece.Color,
                    CurrentPiece.Type, x,
                    y);
                Move.ToPoint = new Point(x,y);

                Game.Move(Move);
                CurrentPiece = null;
            }
        }

        #endregion

    }
}