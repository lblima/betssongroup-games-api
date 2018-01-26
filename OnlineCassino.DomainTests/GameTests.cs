using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace OnlineCassino.Domain.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldFailCreateGameWithWrongParams()
        {
            var game = new Game(null, 0, DateTime.Now, GameCategory.Blackjack);
        }

        [TestMethod()]
        public void ShouldCreateGameWithCorrectParams()
        {
            var gameName = "test game";
            var game = new Game(gameName, 1, DateTime.Now.AddDays(15), GameCategory.ClassicSlots);

            Assert.IsNotNull(game);
            Assert.AreEqual(gameName, game.DisplayName);
        }
    }
}