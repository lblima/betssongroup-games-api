using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace OnlineCassino.Domain.Tests
{
    [TestClass()]
    public class UserTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldFailCreateUserWithWrongParams()
        {
            var user = new User(null, null);
        }

        [TestMethod()]
        public void ShouldCreateUserWithCorrectParams()
        {
            var userName = "Leonardo";
            var user = new User(userName, "xxxxxxxxxxxxxx");

            Assert.IsNotNull(user);
            Assert.AreEqual(user.Name, userName);
        }
    }
}