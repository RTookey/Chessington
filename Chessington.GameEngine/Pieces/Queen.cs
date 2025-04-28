using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
            : base(player) { }

        public IEnumerable<Square> GetAvailableMovesByDirection(Board board, Square currentSquare, (int, int) direction)
        {
            var availableMoves = new List<Square>();

            bool IsEmpty = true; 
            
            while (IsEmpty)
            {
                Square nextSquare = new Square(currentSquare.Row + direction.Item1, currentSquare.Col + direction.Item2);
                currentSquare = nextSquare;

                if (!board.CheckInRange(nextSquare)) break; 
                
                Piece nextPiece = board.GetPiece(nextSquare);
                
                if (nextPiece == null)
                {
                    availableMoves.Add(nextSquare);
                }
                else
                {
                    if (nextPiece.Player != Player) availableMoves.Add(nextSquare);
                    IsEmpty = false; 
                }
            }
            
            return availableMoves;
        }

        
        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var availableMoves = new List<Square>();
            
            List<(int, int)> directionList = new List<(int, int)>() { (0, 1), (0, -1), (1, 0), (-1, 0), (1, 1), (-1, -1), (-1, 1), (1, -1) };

            foreach (var direction in directionList)
            {
                availableMoves.AddRange(GetAvailableMovesByDirection(board, currentSquare, direction));
            }
            
            return availableMoves; 
        }
    }
}