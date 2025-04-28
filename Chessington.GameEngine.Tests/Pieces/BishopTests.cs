using System.Linq;
using Chessington.GameEngine.Pieces;
using FluentAssertions;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests.Pieces
{
    [TestFixture]
    public class BishopTests
    {
        [Test]
        public void BishopCanMoveFourDirections()
        {
            var board = new Board();
            var bishop = new Bishop(Player.White);
            board.AddPiece(Square.At(3, 3), bishop);
            
            var moveList = bishop.GetAvailableMoves(board);

            moveList.Should().HaveCount(13);
        }

        [Test]
        public void BishopMovesDiagonally()
        {
            var board = new Board();
            var bishop = new Bishop(Player.White);
            board.AddPiece(Square.At(0, 0), bishop);
            
            var pawn = new Pawn(Player.Black);
            board.AddPiece(Square.At(1, 1), pawn);
            
            var moveList = bishop.GetAvailableMoves(board);

            var positionToMove = moveList.ToList()[0];
            positionToMove.Row.Should().Be(1);
            positionToMove.Col.Should().Be(1);
        }

        [Test]
        public void BishopCanTakeOppositePieces()
        {
            var board = new Board();
            var bishop = new Bishop(Player.White);
            board.AddPiece(Square.At(0, 0), bishop);
            
            var pawn = new Pawn(Player.Black);
            board.AddPiece(Square.At(1, 1), pawn);
            
            var moveList = bishop.GetAvailableMoves(board);

            moveList.Should().HaveCount(1);
        }

        [Test]
        public void BishopCannotTakeOwnPieces()
        {
            var board = new Board();
            var bishop = new Bishop(Player.White);
            board.AddPiece(Square.At(0, 0), bishop);
            
            var pawn = new Pawn(Player.White);
            board.AddPiece(Square.At(1, 1), pawn);
            
            var moveList = bishop.GetAvailableMoves(board);

            moveList.Should().HaveCount(0);
        }
        
    }
}