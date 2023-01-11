using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class TicTacToeGame : Game
    {
        public TicTacToeGame() : base(3)
        {

        }
        public override char GetPieceCharacter(Piece piece)
        {
            switch (piece)
            {
                case Piece.Player1: return 'X';
                case Piece.Player2: return 'O';
                case Piece.Blank: return ' ';
                case Piece.Invalid: return '!';
            }
            throw new ArgumentOutOfRangeException("piece");
        }
        Piece StartingPieces = Piece.Blank;
        public override void ResetBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    _board[row, col] = GetPieceCharacter(StartingPieces);
                }
            }
        }
        public override void PrintBoard()
        {
            Console.WriteLine("    1   2   3  ");
            for (int row = 0; row < 3; row++)
            {
                Console.WriteLine("   --- --- --- ");
                Console.Write((row + 1) + " | ");
                for (int col = 0; col < 3; col++)
                {
                    Console.Write(_board[row, col]);
                    Console.Write(" | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("   --- --- --- ");
        }
        public override bool TryMove(Move move, Piece piece)
        {
            var row = move.Row;
            var col = move.Col;
            // Before the bounds of the array
            if (row < 0 || col < 0) return false;
            if (row > 2 || col > 2) return false;
            // Check that the desired move is into a blank square
            if (_board[row, col] == GetPieceCharacter(StartingPieces))
            {
                //_board[row, col] = GetPieceCharacter(piece);
                return true;
            }
            else return false;
        }

        public override void MakeMove(Move move, Piece piece)
        {
            var row = move.Row;
            var col = move.Col;
            _board[row, col] = GetPieceCharacter(piece);
        }
        public override bool CheckWin(char[,] board, Piece activePiece)
        {
            char piece = GetPieceCharacter(activePiece);
            if (piece == board[0, 0] && piece == board[0, 1] && piece == board[0, 2])
            {
                return true;
            }
            else if ((piece == board[1, 0] && piece == board[1, 1] && piece == board[1, 2]))
            {
                return true;
            }
            else if ((piece == board[2, 0] && piece == board[2, 1] && piece == board[2, 2]))
            {
                return true;
            }
            else if ((piece == board[0, 0] && piece == board[1, 0] && piece == board[2, 0]))
            {
                return true;
            }
            else if ((piece == board[0, 1] && piece == board[1, 1] && piece == board[2, 1]))
            {
                return true;
            }
            else if ((piece == board[0, 2] && piece == board[1, 2] && piece == board[2, 2]))
            {
                return true;
            }
            else if ((piece == board[0, 0] && piece == board[1, 1] && piece == board[2, 2]))
            {
                return true;
            }
            else if ((piece == board[0, 2] && piece == board[1, 1] && piece == board[2, 0]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override char[,] GetBoard() //Clone of the array
        {
            return (char[,])_board.Clone();
        }

        public override void UpdateBoard(char[,] board)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    _board[row, col] = board[row, col];
                }
            }
        }

        public override bool NoSpace()
        {
            if (_board[0, 0] != GetPieceCharacter(StartingPieces)
                && _board[0, 1] != GetPieceCharacter(StartingPieces)
                && _board[0, 2] != GetPieceCharacter(StartingPieces)
                && _board[1, 0] != GetPieceCharacter(StartingPieces)
                && _board[1, 1] != GetPieceCharacter(StartingPieces)
                && _board[1, 2] != GetPieceCharacter(StartingPieces)
                && _board[2, 0] != GetPieceCharacter(StartingPieces)
                && _board[2, 1] != GetPieceCharacter(StartingPieces)
                && _board[2, 2] != GetPieceCharacter(StartingPieces)
                )
            { return true; }
            else
                return false;
        }

        public override List<int> ReadEmptyRow()
        {
            List<int> emptyRow = new List<int>();

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (_board[row, col] == GetPieceCharacter(StartingPieces))
                    {
                        emptyRow.Add(row);
                    }
                }
            }
            return emptyRow;
        }

        public override List<int> ReadEmptyColumn()
        {
            List<int> emptyColumn = new List<int>();
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (_board[row, col] == GetPieceCharacter(StartingPieces))
                    {
                        emptyColumn.Add(col);
                    }
                }
            }
            return emptyColumn;
        }
    }
}
