using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Pawns
{
    public class Gamestate
    {
        public int GamestateId { get; set; }
        public virtual ObservableCollection<Piece> pieces{get;set;}
        public virtual PlayerColor currentPlayer { get; set; }
        public Gamestate() { }
    }

    public class GamestateContext : DbContext
    {
        public DbSet<Gamestate> Gamestates { get; set; }
        public DbSet<Piece> Pieces { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                    .Configurations.Add(new Piece.PieceConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }

    public partial class MainWindow : Window
    {
        Gamestate gameState;
        Piece movingPiece = null;
        GamestateContext db;
        Point dragPoint;
        bool newgame = false;
        

        public MainWindow()
        {
            db = new GamestateContext();
            InitializeComponent();
            
            var query = (from b in db.Gamestates
                        select b).ToList();

            if (query.Count != 0)
            {
                gameState = query.First();
                var msg = MessageBox.Show("Continue last game?","Game",MessageBoxButton.YesNo);
                if (msg == MessageBoxResult.No)
                {
                    newgame = true;
                }
            }
            else
            {
                newgame = true;
            }

            if(newgame ==true)
            {
                gameState = query.FirstOrDefault();
                db.Gamestates.Remove(gameState);
                if (db.Pieces.Count() > 0)
                {
                    var querypieces = (from b in db.Pieces
                                       select b).ToList();

                    db.Pieces.RemoveRange(querypieces);
                }

                db.SaveChanges();
                gameState = new Gamestate() { };
                gameState.pieces = new ObservableCollection<Piece>();
                for (byte row = 6; row < 8; ++row)
                    for (byte col = 0; col < 8; ++col)
                        gameState.pieces.Add(new Piece(PlayerColor.White, PieceType.Pawn, col, row));
                for (byte row = 0; row < 2; ++row)
                    for (byte col = 0; col < 8; ++col)
                        gameState.pieces.Add(new Piece(PlayerColor.Black, PieceType.Pawn, col, row));
                db.Gamestates.Add(gameState);
                gameState.currentPlayer = PlayerColor.White;
            }
            ChessBoard.ItemsSource = gameState.pieces;
        }

        private void ChessPiece_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            movingPiece = ((Piece)(((Image)sender).DataContext));
            dragPoint = e.GetPosition(null);
        }

        // ToDo: If using SystemParameters.MinimumXYDragDistance, consider using grid_mousemove. If this Image is 
        // left too fast, or the travelled mouse distance within the Image is too short, this event will not fire.
        private void ChessPiece_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Vector diff = dragPoint - e.GetPosition(null);
            Image img = sender as Image;


            if (e.LeftButton == MouseButtonState.Pressed &&
                // Math.Abs(diff.X) >= SystemParameters.MinimumHorizontalDragDistance &&
                // Math.Abs(diff.Y) >= SystemParameters.MinimumVerticalDragDistance &&
                img != null)
            {
                Debug.WriteLine("dragging" + diff);
                DragDrop.DoDragDrop((DependencyObject)sender, ((Image)sender).Source, DragDropEffects.Move);
            }
        }

        private void ChessBoardGrid_Drop(object sender, DragEventArgs e)
        {
            e.Handled = true;
            var x = (byte)(e.GetPosition((Grid)sender).X / ((Grid)sender).ColumnDefinitions[0].ActualWidth);
            var y = (byte)(e.GetPosition((Grid)sender).Y / ((Grid)sender).RowDefinitions[0].ActualHeight);

            if (Rules.isLegalMove(movingPiece, x, y, gameState.currentPlayer))
            {
                movePiece(x, y);
            }
        }

        private void ChessPiece_PreviewDrop(object sender, DragEventArgs e)
        {
            e.Handled = true;
            Piece targetPiece = ((Piece)(((Image)sender).DataContext));
            if (Rules.isLegalMove(movingPiece, targetPiece, gameState.currentPlayer))
            {
                gameState.pieces.Remove(targetPiece);
                movePiece(targetPiece.x, targetPiece.y);
            }
        }

        private void movePiece(byte x, byte y)
        {
            movingPiece.x = x;
            movingPiece.y = y;
            gameState.currentPlayer = (gameState.currentPlayer == PlayerColor.White) ? PlayerColor.Black : PlayerColor.White;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.SaveChanges();
        }
    }
}
