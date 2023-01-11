using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class HumanPlayer : Player
    {
        public HumanPlayer(string name, int id) : base(name, id)
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
            int row;
            Console.Write("Please enter row >> ");
            row = int.Parse(Console.ReadLine()) - 1;

            int col;
            Console.Write("Please enter column >> ");
            col = int.Parse(Console.ReadLine()) - 1;

            return new Move { Row = row, Col = col };
        }
    }
}
