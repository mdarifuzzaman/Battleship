using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipApp
{
    public class Player : IPlayer
    {
        public Player(string name, IBoard board)
        {
            Name = name;      
            Board = board;
        }
        public string Name { get; set; }
        protected IBoard Board { get; set; }


        public string AttackOpposition(IPosition position, Player oposition)
        {
            return oposition.Board.Attack(position, oposition.Board);
        }

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

        public void Initialize()
        {
            Board.Initialize();
        }

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
