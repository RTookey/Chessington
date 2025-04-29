using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public int Direction { get; set; }

        public Pawn(Player player) : base(player)
        {
            Direction = 1 * (Player == Player.White ? -1 : 1);
        }
        
        public override IEnumerable<Square> GetCheckRoutes(Board board)
        {
            var currentSquare = board.FindPiece(this);

            var checkRoutes = new List<Square>();
            var leftSquare = new Square(currentSquare.Row + Direction, currentSquare.Col - 1);
            var rightSquare = new Square(currentSquare.Row + Direction, currentSquare.Col + 1);

            if (board.CheckInRange(leftSquare))
            {
                checkRoutes.Add(leftSquare);
            }

            if (board.CheckInRange(rightSquare))
            {
                checkRoutes.Add(rightSquare);
            }
            
            return checkRoutes;
        }

        
        public bool CheckOccupiedByDifferentPlayer(Board board, Square square)
        {
            var currentPiece = board.GetPiece(square);
            return currentPiece != null && currentPiece.Player != this.Player;
        }
        
        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            
            var currentSquare = board.FindPiece(this);
            
            var availableMoves = new List<Square>();
            var nextSquare = new Square(currentSquare.Row + Direction, currentSquare.Col);
            var leftSquare = new Square(currentSquare.Row + Direction, currentSquare.Col - 1);
            var rightSquare = new Square(currentSquare.Row + Direction, currentSquare.Col + 1);

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