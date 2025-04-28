using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Knight : Piece
    {
        public Knight(Player player)
            : base(player) { }

        public IEnumerable<Square> GetAvailableMovesByDirection(Board board, Square currentSquare, (int, int) direction)
        {
            var availableMoves = new List<Square>();
            
            Square nextSquare = new Square(currentSquare.Row + direction.Item1, currentSquare.Col + direction.Item2);

            if (!board.CheckInRange(nextSquare)) return availableMoves; 
                
            Piece nextPiece = board.GetPiece(nextSquare);
                
            if (nextPiece == null)
            {
                availableMoves.Add(nextSquare);
            }
            else if (nextPiece.Player != Player) 
            {
                availableMoves.Add(nextSquare);
            }
            
            return availableMoves;
        }
        
        
        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var availableMoves = new List<Square>();
            
            List<(int, int)> directionList = new List<(int, int)>() { (2, 1), (-2, -1), (-2, 1), (2, -1), (1, 2), (-1, -2), (-1, 2), (1, -2) };

            foreach (var direction in directionList)
            {
                availableMoves.AddRange(GetAvailableMovesByDirection(board, currentSquare, direction));
            }
            
            return availableMoves; 
        }
    }
}