
using SharpConfig; // https://github.com/cemdervis/SharpConfig
using System.IO;

namespace CSUSBNursingSimulator.Utility
{
    public class AuthorizationController
    {
        public string SecretKey { get; protected set; }

        public string SecretToken { get; protected set; }

        public string AccessToken { get; protected set; }

        public string FileName { get; protected set; }

    public AuthorizationController(string Filepath)
        {
            this.FileName = Filepath;
            this.LoadCredentialsFromFile();
        }

        private void LoadCredentialsFromFile()
        {
            Configuration cfg = new Configuration();
            cfg = Configuration.LoadFromFile(FileName);
            this.SecretKey = cfg["AWSCredentials"]["SecretKey"].StringValue;
            this.AccessToken = cfg["AWSCredentials"]["AccessToken"].StringValue;
            this.SecretToken = cfg["AWSCredentials"]["SecretToken"].StringValue;
        }
    }
}