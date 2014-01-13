using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawns
{
    class Rules
    {
        public static bool isLegalMove(Piece movingPiece, byte x, byte y, PlayerColor currentPlayer)
        {
            if (movingPiece.playerColor == currentPlayer)
            {
                if (movingPiece.x == x)
                {
                    var ydist = movingPiece.y - y;
                    if (movingPiece.playerColor == PlayerColor.White)
                    {
                        if (ydist == 1 || (!movingPiece.hasMoved && ydist == 2))
                            return true;
                    }
                    else
                    {
                        if (ydist == -1 || (!movingPiece.hasMoved && ydist == -2))
                            return true;
                    }
                }
            }

            return false;
        }

        public static bool isLegalMove(Piece movingPiece, Piece targetPiece, PlayerColor currentPlayer)
        {
            if (movingPiece.playerColor == currentPlayer && targetPiece.playerColor != currentPlayer)
            {
                if (Math.Abs(movingPiece.x - targetPiece.x) == 1)
                {
                    if ((movingPiece.playerColor == PlayerColor.White &&
                        movingPiece.y - targetPiece.y == 1) ||
                        (movingPiece.playerColor == PlayerColor.Black &&
                        movingPiece.y - targetPiece.y == -1)) return true;
                }
            }
            return false;
        }

    }
}
