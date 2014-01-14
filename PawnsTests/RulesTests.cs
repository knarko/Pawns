using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pawns;
using NUnit.Framework;
namespace Pawns.Tests
{
    [TestFixture()]
    public class RulesTests
    {
        [Test()]
        public void isLegalMoveTest()
        {
            /*
             * White moves
             */

            //Spawn location, 1 step
            Assert.IsTrue(Rules.isLegalMove(
                new Piece(PlayerColor.White, PieceType.Pawn, 0, 7), 
                0, 6, 
                PlayerColor.White));

            //Spawn location, 2 steps
            Assert.IsTrue(Rules.isLegalMove(
                new Piece(PlayerColor.White, PieceType.Pawn, 0, 7),
                0, 5,
                PlayerColor.White));

            //Spawn location, 3 steps - Fail
            Assert.IsFalse(Rules.isLegalMove(
                new Piece(PlayerColor.White, PieceType.Pawn, 0, 7),
                0, 4,
                PlayerColor.White));

            //Non-spawn, 1 step
            //Non-spawn, 2 steps
            //Non-spawn, 3 steps


            /*
             * Black moves
             */

            //Spawn location, 1 step
            Assert.IsTrue(Rules.isLegalMove(
                new Piece(PlayerColor.Black, PieceType.Pawn, 0, 0),
                0, 1,
                PlayerColor.Black));

            //Spawn location, 2 steps
            Assert.IsTrue(Rules.isLegalMove(
                new Piece(PlayerColor.Black, PieceType.Pawn, 0, 0),
                0, 2,
                PlayerColor.Black));

            //Spawn location, 3 steps - Fail
            Assert.IsFalse(Rules.isLegalMove(
                new Piece(PlayerColor.Black, PieceType.Pawn, 0, 0),
                0, 3,
                PlayerColor.Black));


            //Non-spawn, 1 step
            //Non-spawn, 2 steps
            //Non-spawn, 3 steps
        }

        [Test()]
        public void isLegalMoveTest1()
        {
            /*
             * White moves
             */

            //Spawn location, kill left
            Assert.IsTrue(Rules.isLegalMove(
                new Piece(PlayerColor.White, PieceType.Pawn, 0, 7), 
                new Piece(PlayerColor.Black, PieceType.Pawn, 1, 6), 
                PlayerColor.White));

            //Spawn location, kill right
            Assert.IsTrue(Rules.isLegalMove(
                new Piece(PlayerColor.White, PieceType.Pawn, 1, 7),
                new Piece(PlayerColor.Black, PieceType.Pawn, 0, 6),
                PlayerColor.White));

            //Spawn location, kill forward - Fail
            Assert.IsFalse(Rules.isLegalMove(
                new Piece(PlayerColor.White, PieceType.Pawn, 0, 7),
                new Piece(PlayerColor.Black, PieceType.Pawn, 0, 6),
                PlayerColor.White));


            /*
             * Black moves
             */

            //Spawn location, kill left
            Assert.IsTrue(Rules.isLegalMove(
                new Piece(PlayerColor.Black, PieceType.Pawn, 1, 0),
                new Piece(PlayerColor.White, PieceType.Pawn, 0, 1),
                PlayerColor.Black));

            //Spawn location, kill right
            Assert.IsTrue(Rules.isLegalMove(
                new Piece(PlayerColor.Black, PieceType.Pawn, 0, 0),
                new Piece(PlayerColor.White, PieceType.Pawn, 1, 1),
                PlayerColor.Black));

            //Spawn location, kill forward - Fail
            Assert.IsFalse(Rules.isLegalMove(
                new Piece(PlayerColor.Black, PieceType.Pawn, 0, 0),
                new Piece(PlayerColor.White, PieceType.Pawn, 0, 1),
                PlayerColor.Black));


        }
    }
}
