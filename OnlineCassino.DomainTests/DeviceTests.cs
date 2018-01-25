using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace OnlineCassino.Domain.Tests
{
    [TestClass()]
    public class DeviceTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldFailCreateDeviceWithWrongParams()
        {
            var deviceType = new DeviceType(null);
        }

        [TestMethod()]
        public void ShouldCreateDeviceWithCorrectParams()
        {
            var description = "desktop";
            var deviceType = new DeviceType(description);

            Assert.IsNotNull(deviceType);
            Assert.AreEqual(deviceType.Description, description);
        }
    }
}