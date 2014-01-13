using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Pawns
{
    public enum PieceType
    {
        King, Queen, Rook, Knight, Bishop, Pawn
    }

    public enum PlayerColor
    {
        White,
        Black
    }


    // ToDo?: Compare setter values to previous value for PropertyChanged
    public class Piece : INotifyPropertyChanged
    {
        private PlayerColor playerColorValue;
        private PieceType pieceTypeValue;
        private byte xValue;
        private byte yValue;
        public bool hasMoved { get; private set; }

        public Piece(PlayerColor playerColor, PieceType pieceType, byte x, byte y)
        {
            playerColorValue = playerColor;
            pieceTypeValue = pieceType;
            xValue = x;
            yValue = y;
            hasMoved= false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public PlayerColor playerColor
        {
            get
            {
                return playerColorValue;
            }
        }

        public PieceType pieceType
        {
            get
            {
                return pieceTypeValue;
            }
            set
            {
                pieceTypeValue = value;
                NotifyPropertyChanged();
            }
        }

        public byte x
        {
            get
            {
                return xValue;
            }
            set
            {
                hasMoved = true;
                xValue = value;
                NotifyPropertyChanged();
            }
        }

        public byte y
        {
            get
            {
                return yValue;
            }
            set
            {
                hasMoved = true;
                yValue = value;
                NotifyPropertyChanged();
            }
        }
    }
}
