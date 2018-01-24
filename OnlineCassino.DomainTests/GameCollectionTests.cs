using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace OnlineCassino.Domain.Tests
{
    [TestClass()]
    public class GameCollectionTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldFailCreateGameCollectionWithWrongParams()
        {
            var gameCollection = new GameCollection(null, 0);
        }

        [TestMethod()]
        public void ShouldCreateGameCollectionWithCorrectParams()
        {
            var gameCollection = new GameCollection("test game callection", 1);

            Assert.IsNotNull(gameCollection);
        }
    }
}