using System.IO;
using CSUSBNursingSimulator.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nursing_Simulator_CSUSB_Unit_Test
{
    [TestClass]
    public class AuthorizationControllerTest
    {
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestCredentialsFileNotAvailable()
        {
            var Au = new AwsAuthorization("NonExistantFile");
        }

        [TestMethod]
        [DeploymentItem(@"cred.cfg", ".")]
        public void TestGetSecretKey()
        {
            var Au = new AwsAuthorization("cred.cfg");
            var SecretKey = Au.SecretKey;
            Equals(SecretKey, "foo");
        }

        [TestMethod]
        [DeploymentItem(@"cred.cfg", ".")]
        public void TestGetSecretToken()
        {
            var Au = new AwsAuthorization("cred.cfg");
            var SecretKey = Au.SecretToken;
            Equals(SecretKey, "bar");
        }

        [TestMethod]
        [DeploymentItem(@"cred.cfg", ".")]
        public void TestGetAccessToken()
        {
            var Au = new AwsAuthorization("cred.cfg");
            var SecretKey = Au.AccessToken;
            Equals(SecretKey, "buzz");
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestGetNewCredentialsFile()
        {
            var Au = new AwsAuthorization("watsonCred.cfg");
            var SecretKey = Au.AccessToken;
            Equals(SecretKey, "buzz");
        }
    }
}