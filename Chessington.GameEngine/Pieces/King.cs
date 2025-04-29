using System.Collections.Generic;

namespace Chessington.GameEngine.Pieces
{
    public class King : Piece
    {
        public King(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetCheckRoutes(Board board)
        {
            return new List<Square>();
        }

        
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
            
            List<(int, int)> directionList = new List<(int, int)>() { (0, 1), (1, 0), (0, -1), (-1, 0), (-1, -1), (1, 1), (-1, 1), (1, -1) };

            foreach (var direction in directionList)
            {
                availableMoves.AddRange(GetAvailableMovesByDirection(board, currentSquare, direction));
            }
            
            return availableMoves; 
        }
    }
}