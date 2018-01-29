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
            var gameCollectionName = "test game callection";
            var gameCollectionIndex = 1;
            var gameCollection = new GameCollection(gameCollectionName, gameCollectionIndex);

            Assert.IsNotNull(gameCollection);
            Assert.AreEqual(gameCollectionIndex, gameCollection.DisplayIndex);
            Assert.AreEqual(gameCollectionName, gameCollection.DisplayName);
        }
    }
}