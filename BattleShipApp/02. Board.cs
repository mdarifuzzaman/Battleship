using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShipApp
{
    public class Board : IBoard
    {
        public Board(int numberOfShips)
        {            
            Ships = new Ship[numberOfShips];            
        }

        public virtual void AddShip(IShip ship, int shipNumber)
        {
            if (ship.IsOverflowing())
            {
                throw new InvalidOperationException("Ship is overflowing. Please check");
            }
            if (ship.IsOverlapping(Ships))
            {
                throw new InvalidOperationException("Ship is overlapping. Please check");
            }
            Ships[shipNumber - 1] = ship;
            DrawShip(ship);
        }
        
        public IShip[] Ships { get; }
        public int TotalShip => Ships.Count(e => e != null);
        public string[,] TenByTenBoard { get; private set; }        
        public string Attack(IPosition position, IBoard oppositionBoard)
        {
            //logic to set the position
            var hitMissStr = "miss";
            var isHit = oppositionBoard.CheckForHit(position);
            if (isHit)
            {
                hitMissStr = "hit";
            }
            return hitMissStr;
        }
        public bool CheckMeWinner(IBoard oppositionBoard)
        {
            var isWinner = true;
            foreach(var ship in oppositionBoard.Ships)
            {
                if (!ship.IsShipSunk)
                {
                    isWinner = false;
                    break;
                }
            }
            return isWinner;
        }
        public void Initialize()
        {
            TenByTenBoard = new string[10, 10];
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    TenByTenBoard[x, y] = "__";
                }
            }            
        }

        public bool CheckForHit(IPosition position)
        {
            var isHit = false;
            foreach(var ship in Ships)
            {
                if (ship == null) continue;
                isHit = ship.CheckForHit(position);
                if (isHit)
                {
                    TenByTenBoard[position.X, position.Y] = "H";
                    break;
                }
                else
                {
                    TenByTenBoard[position.X, position.Y] = "M";
                }
            }
            return isHit;
        }

        private void DrawShip(IShip ship)
        {
            var points = ship.OverlappingPoints();
            var x = ship.X;
            var y = ship.Y;
            TenByTenBoard[x, y] = ship.Name;
            foreach (var point in points)
            {
                if (ship.IsShipPlacedInHorizontalDirection)
                {
                    TenByTenBoard[point, y] = ship.Name;
                }
                else
                {
                    TenByTenBoard[x, point] = ship.Name;
                }
            }
        }
    }
}
