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
            var gameSession = new GameSession(new Game("test game", 1, DateTime.Now.AddDays(15), GameCategory.ClassicSlots), new User("Leo", "xxxxxxxxxxxxxx"));

            Assert.IsNotNull(gameSession);
        }
    }
}