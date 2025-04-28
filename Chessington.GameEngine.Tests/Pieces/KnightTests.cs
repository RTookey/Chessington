using System.Linq;
using Chessington.GameEngine.Pieces;
using FluentAssertions;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests.Pieces
{
    [TestFixture]
    public class KnightTests
    {
        [Test]
        public void KnightCanMoveAllDirections()
        {
            var board = new Board();
            var knight = new Knight(Player.White);
            board.AddPiece(Square.At(3, 4), knight);
            
            var moveList = knight.GetAvailableMoves(board);

            moveList.Should().HaveCount(8);
        }
        
        [Test]
        public void KnightCanJumpPieces()
        {
            var board = new Board();
            var knight = new Knight(Player.White);
            board.AddPiece(Square.At(3, 4), knight);
            
            var pawn = new Pawn(Player.Black);
            board.AddPiece(Square.At(3, 3), pawn);
            
            var pawnTwo = new Pawn(Player.Black);
            board.AddPiece(Square.At(3, 5), pawnTwo);
            
            var pawnThree = new Pawn(Player.Black);
            board.AddPiece(Square.At(2, 3), pawnThree);
            
            var pawnFour = new Pawn(Player.Black);
            board.AddPiece(Square.At(2, 4), pawnFour);
            
            var pawnFive = new Pawn(Player.Black);
            board.AddPiece(Square.At(2, 5), pawnFive);
            
            var pawnSix = new Pawn(Player.Black);
            board.AddPiece(Square.At(4, 3), pawnSix);
            
            var pawnSeven = new Pawn(Player.Black);
            board.AddPiece(Square.At(4, 4), pawnSeven);
            
            var pawnEight = new Pawn(Player.Black);
            board.AddPiece(Square.At(4, 5), pawnEight);
            
            var moveList = knight.GetAvailableMoves(board);

            moveList.Should().HaveCount(8);
        }
        
        [Test]
        public void KnightMovesInLShape()
        {
            var board = new Board();
            var knight = new Knight(Player.White);
            board.AddPiece(Square.At(0, 0), knight);

            var pawn = new Pawn(Player.White);
            board.AddPiece(Square.At(1, 2), pawn);
            
            var moveList = knight.GetAvailableMoves(board);

            moveList.Should().HaveCount(1);
            var square = moveList.ToList()[0];
            square.Row.Should().Be(2);
            square.Col.Should().Be(1);

        }
        
        [Test]
        public void KnightCannotTakeOwnPieces()
        {
            var board = new Board();
            var knight = new Knight(Player.White);
            board.AddPiece(Square.At(0, 0), knight);

            var pawn = new Pawn(Player.White);
            board.AddPiece(Square.At(1, 2), pawn);
            
            var pawnTwo = new Pawn(Player.White);
            board.AddPiece(Square.At(2, 1), pawnTwo);

            
            var moveList = knight.GetAvailableMoves(board);

            moveList.Should().HaveCount(0);
        }
        
        [Test]
        public void KnightCanTakeOppositePieces()
        {
            var board = new Board();
            var knight = new Knight(Player.White);
            board.AddPiece(Square.At(0, 0), knight);

            var pawn = new Pawn(Player.Black);
            board.AddPiece(Square.At(1, 2), pawn);
            
            var pawnTwo = new Pawn(Player.Black);
            board.AddPiece(Square.At(2, 1), pawnTwo);
            
            var moveList = knight.GetAvailableMoves(board);

            moveList.Should().HaveCount(2);
        }
        
        
    }
}