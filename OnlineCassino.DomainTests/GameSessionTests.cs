using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace OnlineCassino.Domain.Tests
{
    [TestClass()]
    public class GameSessionTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailCreateGameSessionWithWrongParams()
        {
            var gameSession = new GameSession(null, null);
        }

        [TestMethod()]
        public void ShouldCreateGameSessionWithCorrectParams()
        {
            var gameName = "test game";
            var gameSession = new GameSession(new Game(gameName, 1, DateTime.Now.AddDays(15), GameCategory.ClassicSlots), new User("Leo", "xxxxxxxxxxxxxx"));

            Assert.IsNotNull(gameSession);
            Assert.IsNotNull(gameSession.Game);
            Assert.AreEqual(gameName, gameSession.Game.DisplayName);
        }
    }
}