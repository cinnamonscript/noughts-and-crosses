using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class PlayOrder
    {
        private readonly Game _game;
        private readonly Player _player1;
        private readonly Player _player2;
        public Player _currentPlayer;
        private Player _otherPlayer;
        public Player _tiePlayer;

        public PlayOrder(Game game, Player player1, Player player2)
        {
            _game = game;
            _player1 = player1;
            _player2 = player2;
        }

        /// <returns>The winning player.</returns>

        public Player Play()
        {
            Console.WriteLine("Toss a coin to see who starts first... ");
            _currentPlayer = RandomStart(); // select random starter
            //if (_currentPlayer == _player1) { _otherPlayer = _player2; }
            //else { _otherPlayer = _player1; }
            Console.WriteLine("............................ ");
            Console.WriteLine("............................ ");
            Console.WriteLine("{0} is the lucky starter!", _currentPlayer._name);

            Console.WriteLine("Remember..");
            Console.WriteLine("{0} is {1}", _player1._name, _game.GetPieceCharacter(_player1._piece));
            Console.WriteLine("{0} is {1}", _player2._name, _game.GetPieceCharacter(_player2._piece));


            _game.ResetBoard(); // nulls/initiates the board

            Stack<char[,]> boardState = new Stack<char[,]>();
            int turncount = 1;
            List<int> emptyRow = new List<int>();
            List<int> emptyColumn = new List<int>();

            //Game loop
            while (true)
            {
                _game.PrintBoard(); //Prints null board
                Piece active = _currentPlayer._piece; //Sets Piece of Active Player
                Piece inactive = _otherPlayer._piece; //Sets Piece of Inactive Player
                while (_game.CheckWin(active) == false && _game.CheckWin(inactive) == false) //Checks no winner
                {
                    Console.WriteLine("{0}'s turn", _currentPlayer._name); //Prompts Active Player
                    var move = new Move();
                    do { move = _currentPlayer.GetMove(_game, _otherPlayer); } //Make a move
                    while (!_game.TryMove(move, active)); //Check result of this, loop until valid
                    _game.MakeMove(move, active);
                    _game.PrintBoard(); //Print updated board now

                    //Pushing the board onto the Stack
                    char[,] board = _game.GetBoard(); // gets current board
                    boardState.Push(_game.GetBoard()); // pushes a clone of the board into Stack

                    if (_game.CheckWin(active)) //Check for a winner
                        return _currentPlayer;
                    else if (_game.CheckWin(inactive))
                    { return _otherPlayer; }
                    else if (!_game.CheckWin(active) && !_game.CheckWin(inactive) && _game.NoSpace())
                    { return _tiePlayer; }
                    else //If no winner, then change turns
                    {
                        _currentPlayer = ChangeTurns();
                        active = ActivePiece();
                    }
                    turncount++;
                    //emptyRow = _game.ReadEmptyRow();
                    //emptyColumn = _game.ReadEmptyColumn();

                    if ((_otherPlayer.GetType() == typeof(HumanPlayer)) && turncount < 4)
                    {
                        Console.WriteLine("Press (l) to load game or ENTER to continue >>");
                        var response = Console.ReadLine();
                        if (response == "l")
                        {
                            board = LoadGame();
                            _game.UpdateBoard(board);
                            Console.WriteLine("Welcome back to the last SAVED game!");
                            Console.WriteLine("The active player is {0} using {1}", _currentPlayer._name, _game.GetPieceCharacter(_currentPlayer._piece));
                            _game.PrintBoard();
                            active = _currentPlayer._piece;
                        }
                    }

                    if ((_otherPlayer.GetType() == typeof(HumanPlayer)) && turncount > 3) //After the 3rd turn offer Undo/Save/Load, Human v Bot
                    {
                        Console.WriteLine("Enter (s)ave, (u)ndo, (l)oad or ENTER to continue >>");
                        var response = Console.ReadLine();

                        if (response == "u" && (_currentPlayer.GetType() == typeof(RandomBot) || _currentPlayer.GetType() == typeof(AlphaBetaBot))) // if undoing move, then pop last one
                        {
                            boardState.Pop();//Pop the last player's turn
                            board = boardState.Peek(); //Peek at what is there and assign to board
                            _game.UpdateBoard(board); //Update the board with the peek
                            _game.PrintBoard(); //Print updated board
                            _currentPlayer = ChangeTurns();
                            active = ActivePiece();
                        }

                        else if (response == "u" && (_currentPlayer.GetType() == typeof(HumanPlayer))) //After the 3rd turn offer Undo/Save/Load, Human v Human
                        {
                            boardState.Pop();//Pop the last player's turn one
                            board = boardState.Peek(); //Peek at what is there and assign to board
                            _game.UpdateBoard(board); //Update the board with the peek
                            _game.PrintBoard(); //Print updated board
                            _currentPlayer = ChangeTurns();
                            active = ActivePiece();
                        }
                        else if (response == "s")
                        {
                            board = boardState.Peek();
                            SaveGame(board);
                        }
                        else if (response == "l")
                        {
                            board = LoadGame();
                            _game.UpdateBoard(board);
                            Console.WriteLine("Welcome back to the last SAVED game!");
                            Console.WriteLine("The active player is {0} using {1}", _currentPlayer._name, _game.GetPieceCharacter(_currentPlayer._piece));
                            _game.PrintBoard();
                            active = _currentPlayer._piece;
                        }
                        else
                            continue; // Continue with the while loop
                    }
                }
            }
        }
        private Player ChangeTurns()
        {
            if (_currentPlayer == _player1)
            {
                _otherPlayer = _player1;
                return _player2;
            }
            else
            {
                _otherPlayer = _player2;
                return _player1;
            }
        }
        private Player RandomStart()
        {
            Random starter = new Random();
            if (starter.Next(2) == 0)
            {
                _currentPlayer = _player1;
                _otherPlayer = _player2;
                return _player1;
            }
            else
            {
                _currentPlayer = _player2;
                _otherPlayer = _player1;
                return _player2;
            }
        }
        public Piece ActivePiece()
        {
            if (_currentPlayer == _player1)
                return Piece.Player1;
            else
                return Piece.Player2;
        }
        public void SaveGame(char[,] board) //The main limitation here is that it does not save the Player Type as well.
        {
            var _board = new char[3, 3];

            for (var x = 0; x < 3; ++x)
                for (var y = 0; y < 3; ++y)
                    _board[x, y] = board[x, y];

            var boardString = "";

            for (var x = 0; x < 3; ++x)
                for (var y = 0; y < 3; ++y)
                    boardString += board[x, y];

            // Console.WriteLine(boardString);

            var piece = _currentPlayer._piece;
            string activePiece = "";
            activePiece += _game.GetPieceCharacter(piece);
            var otherPiece = _otherPlayer._piece;
            string inactivePiece = "";
            inactivePiece += _game.GetPieceCharacter(otherPiece);

            string currentPlayerName = _currentPlayer._name;
            string otherPlayerName = _otherPlayer._name;

            File.WriteAllText("board.txt", boardString);
            File.WriteAllText("activename.txt", currentPlayerName);
            File.WriteAllText("inactivename.txt", otherPlayerName);
            File.WriteAllText("activepiece.txt", activePiece);
            File.WriteAllText("inactivepiece.txt", inactivePiece);
        }
        public char[,] LoadGame() //The main limitation here is it does not load the Player Type as well.
        {
            var contents = File.ReadAllText("board.txt");
            var _board = new char[3, 3];
            _board[0, 0] = char.Parse(contents.Substring(0, 1));
            _board[0, 1] = char.Parse(contents.Substring(1, 1));
            _board[0, 2] = char.Parse(contents.Substring(2, 1));
            _board[1, 0] = char.Parse(contents.Substring(3, 1));
            _board[1, 1] = char.Parse(contents.Substring(4, 1));
            _board[1, 2] = char.Parse(contents.Substring(5, 1));
            _board[2, 0] = char.Parse(contents.Substring(6, 1));
            _board[2, 1] = char.Parse(contents.Substring(7, 1));
            _board[2, 2] = char.Parse(contents.Substring(8, 1));

            _currentPlayer._name = File.ReadAllText("activename.txt");
            _otherPlayer._name = File.ReadAllText("inactivename.txt");
            if (char.Parse(File.ReadAllText("activepiece.txt")) == 'X')
            {
                _currentPlayer._piece = Piece.Player1;
                _otherPlayer._piece = Piece.Player2;
            }
            else if (char.Parse(File.ReadAllText("activepiece.txt")) == 'O')
            {
                _currentPlayer._piece = Piece.Player2;
                _otherPlayer._piece = Piece.Player1;
            }
            ;
            return (char[,])_board.Clone();
        }
    }
}