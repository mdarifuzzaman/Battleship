using System;
using System.Collections.Generic;

namespace BattleShipApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***********************Battelships *****************************");

            //Initialize the players and boards
            var player1Name = "Player1";
            var player2Name = "Player3";
            var totalShipForTheGame = 4;
            var board1 = new Board(totalShipForTheGame);
            var board2 = new Board(totalShipForTheGame);

            var player1 = new Player(player1Name, board1);
            var player2 = new Player(player2Name, board2);

            player1.Initialize();
            player2.Initialize();

            //setup the board with ships for players
            //add the ships for player1
            IShip ship1 = new Ship("s1", 5, 5, 3);
            IShip ship2 = new Ship("s2", 1, 1, 8);
            IShip ship3 = new Ship("s3", 4, 4, 3);
            IShip ship4 = new Ship("s4", 1, 8, 5);
            player1.SetupBoard(new List<IShip>() { ship1, ship2, ship3, ship4 });

            //add the ships to the location
            IShip ship21 = new Ship("t1", 5, 5, 3);
            IShip ship22 = new Ship("t2", 1, 1, 3);
            IShip ship23 = new Ship("t3", 4, 4, 3, Directions.Y);
            IShip ship24 = new Ship("t4", 1, 8, 5);
            player2.SetupBoard(new List<IShip>() { ship21, ship22, ship23, ship24 });

            //Player1 is attacking
            var hitOrMiss = player1.AttackOpposition(new Position(5, 5), player2);
            Console.WriteLine($"Player1 -> Action -> Hit or Miss: {hitOrMiss}");
            hitOrMiss = player1.AttackOpposition(new Position(6, 5), player2);
            Console.WriteLine($"Player1 -> Action -> Hit or Miss: {hitOrMiss}");
            hitOrMiss = player1.AttackOpposition(new Position(7, 5), player2);
            Console.WriteLine($"Player1 -> Action -> Hit or Miss: {hitOrMiss}");

            //Player2 is attacking
            hitOrMiss = player2.AttackOpposition(new Position(6, 5), player1);
            Console.WriteLine($"Player2 -> Action -> Hit or Miss: {hitOrMiss}");
            hitOrMiss = player2.AttackOpposition(new Position(6, 6), player1);
            Console.WriteLine($"Player2 -> Action -> Hit or Miss: {hitOrMiss}");
            hitOrMiss = player2.AttackOpposition(new Position(7, 7), player1);
            Console.WriteLine($"Player2 -> Action -> Hit or Miss: {hitOrMiss}");
            hitOrMiss = player2.AttackOpposition(new Position(8, 8), player1);
            Console.WriteLine($"Player2 -> Action -> Hit or Miss: {hitOrMiss}");

            var winner = player1.CheckWinnerAgainst(player2);
            if (winner)
            {
                Console.WriteLine("Player1 -> is winner !!!!!!!!!!!!!");                
            }

            winner = player2.CheckWinnerAgainst(player1);
            if (winner)
            {
                Console.WriteLine("Player2 -> is winner !!!!!!!!!!!!!");
            }

            //current board
            //load the ship data
            PrintState(player1);
            PrintState(player2);

            Console.ReadLine();
        }

        static void PrintState(Player player)
        {
            Console.WriteLine($"\nBoard current state for {player.Name}: ");
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Console.Write(player.GetBoard()[x, y] + " ");
                }
                Console.WriteLine("");
            }
        }
    }
}
