using System;
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
    public class Pieces : ObservableCollection<Piece>
    {
        public Pieces()
        {
            for (byte row = 6; row < 8; ++row)
                for (byte col = 0; col < 8; ++col)
                    Add(new Piece(PlayerColor.White, PieceType.Pawn, col, row));

            for (byte row = 0; row < 2; ++row)
                for (byte col = 0; col < 8; ++col)
                    Add(new Piece(PlayerColor.Black, PieceType.Pawn, col, row));
        }
    }

    public partial class MainWindow : Window
    {
        Pieces gameState = new Pieces();
        Piece movingPiece = null;
        PlayerColor currentPlayer;
        Point dragPoint;
        

        public MainWindow()
        {
            InitializeComponent();
            ChessBoard.ItemsSource = gameState;
            currentPlayer = PlayerColor.White;
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

            if (Rules.isLegalMove(movingPiece, x, y, currentPlayer))
            {
                movePiece(x, y);
            }
        }

        private void ChessPiece_PreviewDrop(object sender, DragEventArgs e)
        {
            e.Handled = true;
            Piece targetPiece = ((Piece)(((Image)sender).DataContext));
            if (Rules.isLegalMove(movingPiece, targetPiece, currentPlayer))
            {
                gameState.Remove(targetPiece);
                movePiece(targetPiece.x, targetPiece.y);
            }
        }

        private void movePiece(byte x, byte y)
        {
            movingPiece.x = x;
            movingPiece.y = y;
            currentPlayer = (currentPlayer == PlayerColor.White) ? PlayerColor.Black : PlayerColor.White;
        }
    }
}
