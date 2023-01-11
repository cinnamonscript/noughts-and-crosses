using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class TicTacToeBot : AlphaBetaBot
    {
        public TicTacToeBot(string name, int id) : base(name, id)
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



            // check validity
            // assign position to player

    }
}
