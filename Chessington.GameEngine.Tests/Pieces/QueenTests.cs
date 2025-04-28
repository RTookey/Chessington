using Chessington.GameEngine.Pieces;
using FluentAssertions;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests.Pieces
{
    [TestFixture]
    public class QueenTests
    {
        [Test]
        public void QueenCanMoveAllDirections()
        {
            var board = new Board();
            var queen = new Queen(Player.White);
            board.AddPiece(Square.At(3, 3), queen);
            
            var moveList = queen.GetAvailableMoves(board);

            moveList.Should().HaveCount(27);
        }
        
        [Test]
        public void QueenCanTakeOppositePieces()
        {
            var board = new Board();
            var queen = new Queen(Player.White);
            board.AddPiece(Square.At(3, 3), queen);
            
            var pawn = new Pawn(Player.Black);
            board.AddPiece(Square.At(3, 0), pawn);
            
            var moveList = queen.GetAvailableMoves(board);

            moveList.Should().HaveCount(27);
            
        }

        [Test]
        public void QueenCannotTakeOwnPieces()
        {
            var board = new Board();
            var queen = new Queen(Player.White);
            board.AddPiece(Square.At(3, 3), queen);
            
            var pawn = new Pawn(Player.White);
            board.AddPiece(Square.At(3, 0), pawn);
            
            var moveList = queen.GetAvailableMoves(board);

            moveList.Should().HaveCount(26);

        }
    }
}