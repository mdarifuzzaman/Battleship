using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipApp
{
    public class Player : IPlayer
    {
        /// <summary>
        /// Constructor to pass playername and board
        /// Board can be constructed inside the player without passing from outside
        /// But to support dependency and make the code testable, it's better to inject
        /// </summary>
        /// <param name="name"></param>
        /// <param name="board"></param>
        public Player(string name, IBoard board)
        {
            Name = name;      
            Board = board;
        }
        public string Name { get; set; }
        protected IBoard Board { get; set; }

        /// <summary>
        /// A player is attacking the opposition by placing a position on the board
        /// Position starts from 0x0 and finishes at 9x9
        /// </summary>
        /// <param name="position"></param>
        /// <param name="oposition"></param>
        /// <returns></returns>
        public string AttackOpposition(IPosition position, Player oposition)
        {
            if(position.X < 0 || position.X > 9 || position.Y < 0 || position.Y > 9)
            {
                throw new ArgumentException("Position is not invalid. Must be under 0-9");
            }

            return oposition.Board.Attack(position, oposition.Board);
        }

        /// <summary>
        /// We can call this method after each attach and hit
        /// </summary>
        /// <param name="opposition"></param>
        /// <returns></returns>
        public bool CheckWinnerAgainst(Player opposition)
        {
            return Board.CheckMeWinner(opposition.Board);
        }

        public string[,] GetBoard()
        {
            return Board.TenByTenBoard;
        }

        public IEnumerable<IShip> GetUsedShips()
        {
            return Board.Ships;
        }

        /// <summary>
        /// To initialize the board
        /// I intentionally make it explicit to call
        /// By doing initialize from constructor might break the dependency 
        /// </summary>
        public void Initialize()
        {
            Board.Initialize();
        }

        /// <summary>
        /// Setup the board with ships
        /// </summary>
        /// <param name="ships"></param>
        public void SetupBoard(IEnumerable<IShip> ships)
        {
            var shipCounter = 1;
            foreach(var ship in ships)
            {
                Board.AddShip(ship, shipCounter);
                shipCounter++;
            }
        }
    }
}
