using System;
using System.Collections.Generic;
using System.Linq;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    public class Board
    {
        private readonly Piece[,] board;
        public Player CurrentPlayer { get; private set; }
        public IList<Piece> CapturedPieces { get; private set; } 

        public string CheckMessage { get; private set; }
        
        public Board()
            : this(Player.White) { }

        public Board(Player currentPlayer, Piece[,] boardState = null)
        {
            board = boardState ?? new Piece[GameSettings.BoardSize, GameSettings.BoardSize]; 
            CurrentPlayer = currentPlayer;
            CapturedPieces = new List<Piece>();
        }

        public void AddPiece(Square square, Piece pawn)
        {
            board[square.Row, square.Col] = pawn;
        }
    
        public Piece GetPiece(Square square)
        {
            return board[square.Row, square.Col];
        }
        
        public Square FindPiece(Piece piece)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
                for (var col = 0; col < GameSettings.BoardSize; col++)
                    if (board[row, col] == piece)
                        return Square.At(row, col);

            throw new ArgumentException("The supplied piece is not on the board.", "piece");
        }
        
        
        public bool CheckInRange(Square square)
        {
            return square.Row > -1 && square.Row < GameSettings.BoardSize && square.Col < GameSettings.BoardSize && square.Col > -1;
        }

        public bool IsCheck()
        {
            List<Square> routes = new List<Square>();
            Square kingsPosition = new Square(); 
            for (var row = 0; row < GameSettings.BoardSize; row++)
            for (var col = 0; col < GameSettings.BoardSize; col++)
                if (board[row, col] != null && board[row, col].Player != CurrentPlayer)
                    routes.AddRange(board[row, col].GetCheckRoutes(this));
                else if (board[row, col] != null && board[row, col].Player == CurrentPlayer && board[row, col] is King)
                {
                    kingsPosition = new Square(row, col);
                }
            return routes.Any(s => s.Row == kingsPosition.Row && s.Col == kingsPosition.Col); 
        }
        
        
        public void MovePiece(Square from, Square to)
        {
            var movingPiece = board[from.Row, from.Col];
            if (movingPiece == null) { return; }

            if (movingPiece.Player != CurrentPlayer)
            {
                throw new ArgumentException("The supplied piece does not belong to the current player.");
            }
            
            // Move the piece and set the 'from' square to be empty.
            Piece pieceCaptured = board[to.Row, to.Col];
            board[to.Row, to.Col] = board[from.Row, from.Col];
            board[from.Row, from.Col] = null;

            if (IsCheck())
            {
                board[from.Row, from.Col] = board[to.Row, to.Col];
                board[to.Row, to.Col] = pieceCaptured;
                CheckMessage = "Illegal move: King is in check";
                OnKingInCheck(CheckMessage);
                return; 
            } else
            {
                CheckMessage = "Make Your Move";
                OnKingInCheck(CheckMessage);
            }

            if (pieceCaptured != null)
            {
                OnPieceCaptured(pieceCaptured);
            }
            
            CurrentPlayer = movingPiece.Player == Player.White ? Player.Black : Player.White;
            OnCurrentPlayerChanged(CurrentPlayer);
        }
        
        public delegate void PieceCapturedEventHandler(Piece piece);
        
        public event PieceCapturedEventHandler PieceCaptured;

        protected virtual void OnPieceCaptured(Piece piece)
        {
            var handler = PieceCaptured;
            if (handler != null) handler(piece);
        }

        public delegate void CurrentPlayerChangedEventHandler(Player player);

        public event CurrentPlayerChangedEventHandler CurrentPlayerChanged;

        public delegate void KingInCheckEventHandler(string message);
        
        public event KingInCheckEventHandler KingInCheck;

        protected virtual void OnKingInCheck(string message)
        {
            var handler = KingInCheck;
            if (handler != null) handler(message);
        }
        
        protected virtual void OnCurrentPlayerChanged(Player player)
        {
            var handler = CurrentPlayerChanged;
            if (handler != null) handler(player);
        }
    }
}
