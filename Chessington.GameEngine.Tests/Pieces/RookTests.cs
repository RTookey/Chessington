using Chessington.GameEngine.Pieces;
using FluentAssertions;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests.Pieces
{
    [TestFixture]
    public class RookTests
    {
        [Test]
        public void RookCanMoveUpDownLeftRight()
        {
            var board = new Board();
            var rook = new Rook(Player.White);
            board.AddPiece(Square.At(1, 1), rook);
            
            var moveList = rook.GetAvailableMoves(board);

            moveList.Should().HaveCount(14);
        }

        [Test]
        public void RookCanMoveUpToSamePlayerPiece()
        {
            var board = new Board();
            var rook = new Rook(Player.White);
            board.AddPiece(Square.At(0, 0), rook);
            
            var pawn = new Pawn(Player.White);
            board.AddPiece(Square.At(0, 1), pawn);
            
            var moveList = rook.GetAvailableMoves(board);

            moveList.Should().HaveCount(7);
        }

        [Test]
        public void RookCanMoveUpToAndIncludingOppositePlayerPiece()
        {
            
            var board = new Board();
            var rook = new Rook(Player.White);
            board.AddPiece(Square.At(0, 0), rook);
            
            var pawn = new Pawn(Player.Black);
            board.AddPiece(Square.At(0, 1), pawn);
            
            var moveList = rook.GetAvailableMoves(board);

            moveList.Should().HaveCount(8);
        }
        
    }
}