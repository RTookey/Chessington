using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }


        public bool CheckOccupiedByDifferentPlayer(Board board, Square square)
        {
            var currentPiece = board.GetPiece(square);
            return currentPiece != null && currentPiece.Player != this.Player;
        }
        
        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            
            var currentSquare = board.FindPiece(this);
            int moveLimit = 1;

            if (Player == Player.White) moveLimit *= -1; 
            
            var availableMoves = new List<Square>();
            var nextSquare = new Square(currentSquare.Row + moveLimit, currentSquare.Col);
            var leftSquare = new Square(currentSquare.Row + moveLimit, currentSquare.Col - 1);
            var rightSquare = new Square(currentSquare.Row + moveLimit, currentSquare.Col + 1);

            if (board.CheckInRange(leftSquare) && CheckOccupiedByDifferentPlayer(board, leftSquare))
            {
                availableMoves.Add(leftSquare);
            }

            if (board.CheckInRange(rightSquare) && CheckOccupiedByDifferentPlayer(board, rightSquare))
            {
                availableMoves.Add(rightSquare);
            }

            if (board.CheckInRange(nextSquare) && board.GetPiece(nextSquare) == null)
            {
                availableMoves.Add(nextSquare);
            }
            
            return availableMoves;
        }
    }
}