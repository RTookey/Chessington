using System.Linq;
using Chessington.GameEngine.Pieces;
using FluentAssertions;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests.Pieces
{
    [TestFixture]
    public class PawnTests
    {
        [Test]
        public void PawnCanMoveForward()
        {
            var board = new Board();
            var pawn = new Pawn(Player.White);
            board.AddPiece(Square.At(1, 4), pawn);
            
            var moveList = pawn.GetAvailableMoves(board);

            moveList.Should().HaveCount(1);
        }

        [Test]
        public void PawnCanMoveOneSpaceForward()
        {
            var board = new Board();
            var pawn = new Pawn(Player.White);
            board.AddPiece(Square.At(1, 4), pawn);
            
            var moveList = pawn.GetAvailableMoves(board);
            var nextSquare = moveList.ToList()[0];
            
            nextSquare.Row.Should().Be(0);
        }

        [Test]
        public void PawnCannotMoveForwardIfOccupied()
        {
            var board = new Board();
            var pawn = new Pawn(Player.White);
            board.AddPiece(Square.At(1, 4), pawn);
            
            var blackPawn = new Pawn(Player.Black);
            board.AddPiece(Square.At(0, 4), blackPawn);
            
            var moveList = pawn.GetAvailableMoves(board);

            moveList.Should().HaveCount(0);
            
        }

        [Test]
        public void PawnCanMoveDiagonalIfOccupiedByBlack()
        {
            var board = new Board();
            var pawn = new Pawn(Player.White);
            board.AddPiece(Square.At(1, 4), pawn);
            
            var blockingPawn = new Pawn(Player.Black);
            board.AddPiece(Square.At(0, 4), blockingPawn);
            
            var blackPawnOne = new Pawn(Player.Black);
            board.AddPiece(Square.At(0, 3), blackPawnOne);
        
            var blackPawnTwo = new Pawn(Player.Black);
            board.AddPiece(Square.At(0, 5), blackPawnTwo);
            
            var moveList = pawn.GetAvailableMoves(board);

            moveList.Should().HaveCount(2);

        }
        
        
        [Test]
        public void PawnCanMoveIntoOccupiedSpace()
        {
            var board = new Board();
            var pawn = new Pawn(Player.White);
            board.AddPiece(Square.At(1, 4), pawn);
            
            var blockingPawn = new Pawn(Player.Black);
            board.AddPiece(Square.At(0, 4), blockingPawn);
            
            var blackPawnOne = new Pawn(Player.Black);
            board.AddPiece(Square.At(0, 3), blackPawnOne);
        
            var moveList = pawn.GetAvailableMoves(board);

            var nextSquare = moveList.ToList()[0];
            
            nextSquare.Row.Should().Be(0);
            nextSquare.Col.Should().Be(3);
        }
        
        
    }
}