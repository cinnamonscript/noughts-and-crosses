
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class GamePlay
    {
        public static void Main()
        {
            Console.WriteLine("===================================================");
            Console.WriteLine("             WELCOME TO TIAC-TAC-TOE               ");
            Console.WriteLine("===================================================");

            Console.WriteLine("Rules: ");
            Console.WriteLine(" ");
            Console.WriteLine("The game is played a 3x3 grid.");
            Console.WriteLine(" ");
            Console.WriteLine("In turns, players ('X' or 'O') to place ");
            Console.WriteLine("their turn on an available grid.");
            Console.WriteLine(" ");
            Console.WriteLine("The first player to get 3 of their marks ");
            Console.WriteLine("in a row (down, across, or diagonally) is the winner.");
            Console.WriteLine(" ");
            Console.WriteLine("When all 9 squares are full, the game is over. ");
            Console.WriteLine("If no player has 3 in a row, the game ends in a tie.");
            Console.WriteLine(" ");
            Console.WriteLine("You will have the option to Undo your move.");
            Console.WriteLine("You can also save the game, and load the");
            Console.WriteLine("last saved game.");
            Console.WriteLine(" ");
            Console.WriteLine("Press ENTER to PLAY >>");
            Console.ReadLine();

            var player1 = GetPlayer(1);            
            var player2 = GetPlayer(2);
            var replay = false;

            do
            {
                Console.WriteLine("Welcome {0} and {1} to Tic Tac TOE-to-TOE!", player1._name, player2._name);

                var game = new TicTacToeGame();
                var playOrder = new PlayOrder(game, player1, player2);

                var winningPlayer = playOrder.Play();
                if (winningPlayer == playOrder._tiePlayer)
                {
                    Console.WriteLine("{0} and {1} went TOE to TOE and it was a TIE!", player1._name, player2._name);
                }
                else
                    Console.WriteLine("{0} is vicTOErious!", winningPlayer._name);

                Console.WriteLine("Try again? y/n");
                var response = Console.ReadLine();
                replay = Replay(response);
            }
            while (replay);

        }
        private static Player GetPlayer(int id)
        {
            while (true)
            {
                if (id == 1)
                {
                    Console.WriteLine("Oh.. Hi there, what's your name?");
                    var name = Console.ReadLine();
                    return new HumanPlayer(name, id);
                    Console.WriteLine("You'll be player {0}", id);
                }
                else
                {
                    Console.WriteLine("Would you like to play with another human or computer? Enter h/c >>");
                    var type = Console.ReadLine();

                    if (type != "c" && type != "h")
                    {
                        Console.WriteLine("Invalid.");
                        continue;
                    }

                    if (type == "h")
                    {
                        Console.WriteLine("Enter name for player {0}: ", id);
                        var name = Console.ReadLine();
                        return new HumanPlayer(name, id);
                        Console.WriteLine("{0} will be player {1}", name, id);
                    }
                    Console.WriteLine("Please select difficulty level easy or difficult. Enter e/d >>");
                    var level = char.Parse(Console.ReadLine());
                    while (level != 'e' && level != 'd')
                    {
                        Console.WriteLine("Invalid. Please select difficulty level easy or difficult. Enter e/d >>");
                        level = char.Parse(Console.ReadLine());
                    }
                    if (level == 'e')
                    {
                        return new RandomBot("RandoBot", id);
                    }
                    else
                    {
                        return new AlphaBetaBot("SmartoBot", id);
                    }
                }
            }
        }
        private static bool Replay(string response)
        {
            if (response == "y")
            { return true; }
            else if (response == "n")
            {
                Console.WriteLine("Thanks for playing!");
                return false;
            }
            else
            {
                Console.WriteLine("Invalid response....");
                Console.WriteLine("Thanks for playing!");
                return false;
            }
        }
    }
}

