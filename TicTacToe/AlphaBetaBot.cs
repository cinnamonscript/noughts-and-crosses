using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class AlphaBetaBot : Player
    {
        public AlphaBetaBot(string name, int id) : base(name, id)
        {
            _name = name;
            _id = id;
            if (id == 1)
            {
                _piece = Piece.Player1;
            }
            else if (id == 2)
            {
                _piece = Piece.Player2;
            }
            else
            {
                _piece = Piece.Invalid;
            }
        }

        public override Move GetMove(Game game, Player opponent)
        {
            char[,]_board = game.GetBoard();
            return GetBestMove(_board, this, game.ReadEmptyRow(), game.ReadEmptyColumn(), game, opponent);
        }

        private SmartMove GetBestMove(char[,] _board, Player _player, List<int> _emptyRow, List<int> _emptyColumn, Game _game, Player opponent)
        {
            _board = _game.GetBoard();
            List<SmartMove> aiMoves = new List<SmartMove>();
            SmartMove terminalMove = new SmartMove();
            int count = 0;
            
            if (_game.CheckWin(_board, opponent._piece) == true) //If Human (P1) wins
            {
                terminalMove.score = -10;
                return terminalMove;
            }

            else if (_game.CheckWin(_board, this._piece) == true) //If P2 wins
            {
                terminalMove.score = 10;
                return terminalMove;
            }

            else if (_emptyRow.Count <= 0) //Draw
            {
                terminalMove.score = 0;
                return terminalMove;
            }

            else if (count >= 1)
            {
                count++;
                terminalMove.score = -50;
                return terminalMove;// No WLD
            }
            else
            {
                count++;
            }

            //Recursive function is called at each iteration
            for (int i = 0; i < _emptyRow.Count; i++)
            {
                int row = _emptyRow[i];
                int col = _emptyColumn[i];
                //Console.WriteLine("Row {0} and col {1}", row, col);
                SmartMove move = new SmartMove();
                var board = (char[,])_board.Clone();
                var remainingEmptyRows = _emptyRow.Skip(i + 1).ToList();
                var remainingEmptyCols = _emptyColumn.Skip(i + 1).ToList();

                if (_player == this) //If piece if AI piece
                {
                    var _playerPiece = _game.GetPieceCharacter(_player._piece);
                    board[row, col] = _playerPiece; //** AI adds a simulated piece on test board to check win, lose, draw
                    _player = opponent; //Switch to human,
                    var result = GetBestMove(board, this, remainingEmptyRows, remainingEmptyCols, _game, opponent); //AIMove
                    move = result;
                    aiMoves.Add(move);
                    move.score = result.score;
                    move.Row = row;
                    move.Col = col;
                }
                if (_player == opponent) // If Human

                {
                    var _playerPiece = _game.GetPieceCharacter(_player._piece);
                    board[row, col] = _playerPiece; // AI adds a simulated piece on test board to check win, lose, draw
                    _player = this;
                    var result = GetBestMove(board, opponent, remainingEmptyRows, remainingEmptyCols, _game, opponent);
                    move = result;
                    aiMoves.Add(move);
                    move.score = result.score;
                    move.Row = row;
                    move.Col = col;
                }
                board[row, col] = _game.GetPieceCharacter(Piece.Blank);
            }

            var bestMove = 0;

            if (_player == this) //AI Player
            {
                int bestScore = -1000;
                for (int i = 0; i < aiMoves.Count; i++)
                {
                    if (aiMoves[i].score > bestScore) //Sorting list
                    {
                        bestMove = i;
                        bestScore = aiMoves[i].score;
                    }
                }
            }
            else if (_player == opponent) //Human Player
            {
                int worstScore = 1000;
                for (int i = 0; i < aiMoves.Count; i++)
                {
                    if (aiMoves[i].score < worstScore) //Sorting list
                    {
                        bestMove = i;
                        worstScore = aiMoves[i].score;
                    }
                }
            }
            count = 0;
            return aiMoves[bestMove];
        }
    }
}
