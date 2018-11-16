
using SharpConfig; // https://github.com/cemdervis/SharpConfig
using System.IO;

namespace CSUSBNursingSimulator.Utility
{
    public class AuthorizationController
    {
        private string SecretKey { get; set; }

        private string SecretToken { get; set; }

        private string AccessToken { get; set; }

        private const string FileName = "cred.cfg";

        public AuthorizationController()
        {
            this.LoadCredentialsFromFile();
        }

        private void LoadCredentialsFromFile()
        {
            if (!File.Exists(FileName))
            {
                throw new FileNotFoundException();
            }
            Configuration cfg = new Configuration();
            cfg = Configuration.LoadFromFile(FileName);
    }
    }
}