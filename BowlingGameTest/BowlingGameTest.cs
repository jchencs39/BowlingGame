using Microsoft.VisualStudio.TestTools.UnitTesting;
using BowlingGame.Respository;
using System;

namespace BowlingGameTest
{
    [TestClass]
    public class BowlingGameTest
    {
        private  BowlingGameService _gameService;
        private  Random _random;

        [TestInitialize]
        public void Initialize()
        {
            _gameService = new BowlingGameService();
            _random  = new Random();
        }

        [TestMethod]
        public void CanRollGutterGame()
        {
            //_gameService = new BowlingGameService();
            RollMany(0, 20);
            Assert.AreEqual(0, _gameService.Score());
        }

        [TestMethod]
        public void CanRollAllOnes()
        {
            //_gameService = new BowlingGameService();
            RollMany(1, 20);
            Assert.AreEqual(20, _gameService.Score());
        }

        [TestMethod]
        public void CanRollSpare()
        {
           
            _gameService.Roll(5);
            _gameService.Roll(5);
            _gameService.Roll(3);
            RollMany(0, 17);
            Assert.AreEqual(16, _gameService.Score());
        }
        [TestMethod]
        public void CanRollStrike()
        {
            
            _gameService.Roll(10);
            _gameService.Roll(4);
            _gameService.Roll(3);
            RollMany(0, 16);
            Assert.AreEqual(24, _gameService.Score());
        }
        [TestMethod]
        public void CanRollPerfectGame()
        {
           
            RollMany(10, 12);
            Assert.AreEqual(300, _gameService.Score());
        }

        [TestMethod]
        public void RandomRolls()
        {
            int min = 0;
            int max = 10;
            int pin1 = _random.Next(min, max);
            int pin2 =0;
            if (pin1<10)
            {
                pin2 = _random.Next(min, max - pin1);
            }
            Assert.IsTrue( pin1+ pin2 <= 10);
        }
        private void RollMany(int pins, int rolls)
        {
            for (int i = 0; i < rolls; i++)
            {
                _gameService.Roll(pins);
            }
        }

    }
}
