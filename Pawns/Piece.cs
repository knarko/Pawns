using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics;

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
        private PlayerColor playerColorValue {get;set;}
        private PieceType pieceTypeValue { get; set; }
        private byte xValue { get; set; }
        private byte yValue { get; set; }
        public bool hasMoved { get; set; }

        public class PieceConfiguration : EntityTypeConfiguration<Piece>
        {
            public PieceConfiguration()
            {
                Property(p => p.playerColorValue);
                Property(p => p.pieceTypeValue);
                Property(p => p.xValue);
                Property(p => p.yValue);
                Property(p => p.hasMoved);
                Ignore(p => p.x);
                Ignore(p => p.y);
            }
        }

        public Piece() { }

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
                Debug.WriteLine("y changed");
                hasMoved = true;
                yValue = value;
                NotifyPropertyChanged();
            }
        }

        public int PieceId { get; set; }
    }
}
