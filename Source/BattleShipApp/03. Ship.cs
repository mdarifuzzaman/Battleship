using System;
using System.Linq;

namespace BattleShipApp
{
    public class Ship : IShip
    {
        private readonly IHelper _helper = new Helper();

        /// <summary>
        /// Radis is inclusive of X
        /// Example: x = 5, y = 5, radius = 3 will produce 3 square i.e (5,5), (6,5) (7,5) Where Direction = X
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <param name="directions"></param>
        public Ship(string name, int x, int y, int radius, Directions directions = Directions.X)
        {
            Name = name;
            X = x;
            Y = y;
            Radius = radius;
            Directions = directions;
        }

        public string Name { get; }
        public int X { get; }
        public int Y { get; }
        public bool IsShipPlacedInHorizontalDirection => Directions == Directions.X;

        public int HitCount { get; private set; } = 0;

        public bool IsShipSunk { get; private set; } = false;

        public int Radius { get; }

        public Directions Directions { get; }

        public int[] OverlappingPoints()
        {
            int[] positionToCheck;
            positionToCheck = new int[Radius];
            if (IsShipPlacedInHorizontalDirection)
            {                
                for (int i = 0; i < Radius; i++)
                {
                    positionToCheck[i] = X + i;
                }
            }
            else
            {                
                for (int i = 0; i < Radius; i++)
                {
                    positionToCheck[i] = Y + i;
                }
            }
            return positionToCheck;
        }

        /// <summary>
        /// The board is indexed by (0 to 9 = 10, by 0 to 9 = 10) = 10x10
        /// </summary>
        /// <returns></returns>
        public bool IsOverflowing()
        {
            var overflowing = false;
            if (IsShipPlacedInHorizontalDirection)
            {
                if (Radius + X >=10 ) //going over 100
                {
                    overflowing = true;
                }
            }
            else
            {
                if (Radius + Y >= 10) //going over 100
                {
                    overflowing = true;
                }
            }
            return overflowing;
        }

        /// <summary>
        /// This is calculated by the help of a helper method LineSegementsIntersect
        /// It will calculate if there is any intersect of the 2 lines
        /// </summary>
        /// <param name="existingShip"></param>
        /// <returns></returns>
        public bool IsOverlapping(IShip[] existingShip)
        {
            var overlapping = false;
            foreach (var ship in existingShip)
            {
                if (ship is null) continue;
                if (ship.IsShipPlacedInHorizontalDirection)
                {
                    Vector intersection;
                    var isIntersecting = _helper.LineSegementsIntersect(
                        new Vector(ship.X, ship.Y),
                        new Vector(ship.X + ship.Radius, ship.Y),
                        new Vector(X, Y),
                        new Vector(IsShipPlacedInHorizontalDirection ? X + Radius: X, !IsShipPlacedInHorizontalDirection ? Y + Radius: Y),
                        out intersection,
                        considerCollinearOverlapAsIntersect: true);

                    if (isIntersecting)
                    {
                        overlapping = true;
                        break;
                    }
                }
                else
                {
                    Vector intersection;
                    var isIntersecting = _helper.LineSegementsIntersect(
                        new Vector(ship.X, ship.Y),
                        new Vector(ship.X, ship.Y + ship.Radius),
                        new Vector(X, Y),
                        new Vector(IsShipPlacedInHorizontalDirection ? X + Radius : X, !IsShipPlacedInHorizontalDirection ? Y + Radius : Y),
                        out intersection,
                        considerCollinearOverlapAsIntersect: true);
                    if (isIntersecting)
                    {
                        overlapping = true;
                        break;
                    }
                }

                //check ship is going over 100                
            }
            return overlapping;
        }
        
        /// <summary>
        /// Any given position, it will check if the current ship is also placed or not
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool CheckForHit(IPosition position)
        {
            var points = OverlappingPoints();
            bool isHit = false;
            foreach(var point in points)
            {
                if (IsShipPlacedInHorizontalDirection)
                {
                    if (point == position.X && position.Y == Y)
                    {
                        isHit = true;
                        break;
                    }
                }
                else
                {
                    if(point == position.Y && position.X == X)
                    {
                        isHit = true;
                        break;
                    }
                }
            }
            if (isHit)
            {
                HitCount++;
            }

            var shipLength = Radius;
            if(HitCount == shipLength)
            {
                IsShipSunk = true;
            }
            return isHit;
        }
    }
}

public enum Directions
{
    X,
    Y
}
