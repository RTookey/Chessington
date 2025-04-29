using System.Linq;
using Chessington.GameEngine.Pieces;
using FluentAssertions;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests.Pieces
{
    [TestFixture]
    public class KingTests
    {
        [Test]
        public void KingCanMoveAllDirections()
        {
            var board = new Board();
            var king = new King(Player.White);
            board.AddPiece(Square.At(4, 4), king);
            
            var moveList = king.GetAvailableMoves(board);

            moveList.Should().HaveCount(8);
        }

        [Test]
        public void KingCanOnlyMoveOneSquare()
        {
            var board = new Board();
            var king = new King(Player.White);
            board.AddPiece(Square.At(0, 0), king);

            var pawn = new Pawn(Player.White);
            board.AddPiece(Square.At(0, 1), pawn);
            
            var pawnTwo = new Pawn(Player.White);
            board.AddPiece(Square.At(1, 0), pawn);
            
            var moveList = king.GetAvailableMoves(board);
            var nextSquare = moveList.ToList()[0];
            
            nextSquare.Row.Should().Be(1);
            nextSquare.Col.Should().Be(1);
            
        }
        
    }
}