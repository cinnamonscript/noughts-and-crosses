using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    abstract class Game

    {
        protected readonly char[,] _board;
        protected Game(int size)
        {
            _board = new char[size, size];
        }
        public abstract char GetPieceCharacter(Piece piece);
        public abstract void ResetBoard();
        public abstract void PrintBoard();
       // public abstract void UpdateBoard(Move move, Piece piece);
        public abstract bool TryMove(Move move, Piece piece);
        public abstract void MakeMove(Move move, Piece piece);
        public abstract bool CheckWin(char[,] board, Piece piece);
        public bool CheckWin(Piece piece)
        {
            return CheckWin(_board, piece);
        }
        public abstract char[,] GetBoard();
        public abstract bool NoSpace();
        public abstract void UpdateBoard(char[,] board);
        public abstract List<int> ReadEmptyRow();
        public abstract List<int> ReadEmptyColumn();
    }

    /*
    Additional features needed = 
    getBoard() : Pieces[,]
    */


}
