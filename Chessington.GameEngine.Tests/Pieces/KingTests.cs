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

        
        // king cannot move in directions other pieces have in their moves? 
        
        // can only move by 1 at a time? 
        
    }
}