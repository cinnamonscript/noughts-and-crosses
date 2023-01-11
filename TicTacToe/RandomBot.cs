using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class RandomBot : Player
    {
        public RandomBot(string name, int id) : base(name, id)
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
            Random number = new Random();
            int row = number.Next(0, 3);

            while (row < 0 && row > 2)
            {
                row = number.Next(0, 3);
            }

            int col = number.Next(0, 3);
            while (col < 0 && col > 2)
            {
                col = number.Next(0, 3);
            }
            return new Move { Row = row, Col = col };
        }
    }
}
