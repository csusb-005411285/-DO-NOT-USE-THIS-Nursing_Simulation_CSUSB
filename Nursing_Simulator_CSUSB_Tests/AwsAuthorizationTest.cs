using System;
using System.IO;
using CSUSBNursingSimulator.Utility;
using NUnit.Framework;

namespace Nursing_Simulator_CSUSB_Tests
{
    [TestFixture]
    public class AwsAuthorizationTests
    {
        [Test]
        public void TestIfCredentialsFileNotAvailableItThrowsException()
        {
            Assert.Throws<FileNotFoundException>(() => new AwsAuthorization("NonExistentFile"));
        }

        [Test]
        public void TestGetSecretKeyReturnsCorrectValue()
        {
            var au = new AwsAuthorization("cred.cfg");
            var secretKey = au.SecretKey;
            Assert.AreEqual(secretKey, "foo");
        }

        [Test]
        public void TestWhenInvalidAccessKeyIsAccessedItReturnsEmptyKey()
        {
            var au = new AwsAuthorization("cred.cfg");
            var accessToken = au.AccessToken;
            Assert.AreEqual(accessToken, string.Empty);
        }
    }

}