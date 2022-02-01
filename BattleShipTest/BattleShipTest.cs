using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShipApp.Test
{
    [TestClass]
    public class BattleShipTest
    {
        [TestMethod]
        public void PlayerNameIsCorrectlyPlaced()
        {
            var player = new Player("player1", new Board(10));
            Assert.AreEqual("player1", player.Name);
        }

        [TestMethod]
        public void ShipsAreCorrectlyCreated()
        {
            var player = new Player("player1", new Board(10));
            player.Initialize();
            Assert.AreEqual(10 * 10, player.GetBoard().Length);
        }

        [TestMethod]
        public void OverlappingShipShouldNotBeAccepted()
        {
            var board = new Board(10);
            board.Initialize();
            var player = new Player("player1", board);
            IShip ship1 = new Ship("s1", 5, 5, 3);
            IShip ship2 = new Ship("s2", 1, 1, 8);
            IShip ship3 = new Ship("s3", 4, 4, 3);
            IShip ship4 = new Ship("s4", 4, 3, 5, Directions.Y); //this is an overlapping ship
            player.SetupBoard(new List<IShip>() { ship1, ship2, ship3});
            Assert.ThrowsException<InvalidOperationException>(() => player.SetupBoard(new List<IShip>() { ship4 }));
        }

        [TestMethod]
        public void OverflowingShipShouldBeAccepted()
        {
            var board = new Board(10);
            board.Initialize();
            var player = new Player("player1", board);
            IShip ship1 = new Ship("s1", 5, 5, 3);
            IShip ship2 = new Ship("s2", 1, 1, 8);
            IShip ship3 = new Ship("s3", 4, 4, 3, 0);
            IShip ship4 = new Ship("s4", 4, 8, 5, Directions.Y); //overflowing ship = Y = 8 + 5 = 13 which is >= 10 
            player.SetupBoard(new List<IShip>() { ship1, ship2, ship3 });
            Assert.ThrowsException<InvalidOperationException>(() => player.SetupBoard(new List<IShip>() { ship4 }));
        }

        [TestMethod]
        public void ValidShipsShouldBeCreated()
        {
            var board = new Board(10);
            board.Initialize();
            var player = new Player("player1", board);
            IShip ship1 = new Ship("s1", 5, 5, 3);
            IShip ship2 = new Ship("s2", 1, 1, 8);
            IShip ship3 = new Ship("s3", 4, 4, 3, 0);
            IShip ship4 = new Ship("s4", 4, 5, 3, Directions.Y); //overflowing ship = Y = 8 + 5 = 13 which is >= 10 
            player.SetupBoard(new List<IShip>() { ship1, ship2, ship3, ship4 });
            Assert.AreEqual(4, player.GetUsedShips().Count(e => e != null));
        }

        [TestMethod]
        public void AttackOppositionShouldMarkAsMiss_When_ThereIsNoShip()
        {
            var board1 = new Board(4);
            var board2 = new Board(4);
            var player1 = new Player("player1", board1); 
            var player2 = new Player("player2", board2);

            player1.Initialize();
            player2.Initialize();

            IShip ship1 = new Ship("s1", 5, 5, 3);
            player1.SetupBoard(new List<IShip>() { ship1 });

            IShip ship21 = new Ship("t1", 5, 5, 3);
            player2.SetupBoard(new List<IShip>() { ship21 });

            var hitOrMiss = player1.AttackOpposition(new Position(1, 5), player2);
            Assert.AreEqual("miss", hitOrMiss);
            Assert.AreEqual(0, ship21.HitCount);
        }

        [TestMethod]
        public void AttackOppositionShouldMarkAsHit_When_ThereIsAShip()
        {
            var board1 = new Board(4);
            var board2 = new Board(4);
            var player1 = new Player("player1", board1);
            var player2 = new Player("player2", board2);

            player1.Initialize();
            player2.Initialize();

            IShip ship1 = new Ship("s1", 5, 5, 3);
            player1.SetupBoard(new List<IShip>() { ship1 });

            IShip ship21 = new Ship("t1", 5, 5, 3);
            player2.SetupBoard(new List<IShip>() { ship21 });

            var hitOrMiss = player1.AttackOpposition(new Position(7, 5), player2);
            Assert.AreEqual("hit", hitOrMiss);
            Assert.AreEqual(1, ship21.HitCount);
        }
    }
}
