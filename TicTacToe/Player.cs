using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal abstract class Player
    {
        protected int _id;
        public string _name;
        public Piece _piece;
        protected Player(string name, int id)
        {
            _name = name;
            _id = id;
        }
        public string Name { get; }
        public abstract Move GetMove(Game game, Player opponent);

    }
}
