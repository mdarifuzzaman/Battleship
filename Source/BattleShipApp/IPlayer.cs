using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipApp
{
    public interface IPlayer
    {
        string Name { get; set; }
        void Initialize();
        string AttackOpposition(IPosition position, Player oposition);
        void SetupBoard(IEnumerable<IShip> ships);
        bool CheckWinnerAgainst(Player opposition);
        IEnumerable<IShip> GetUsedShips();
        string[,] GetBoard();
    }
}
