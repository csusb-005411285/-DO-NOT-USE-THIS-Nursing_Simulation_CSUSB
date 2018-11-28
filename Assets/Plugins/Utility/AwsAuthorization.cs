using SharpConfig;

namespace CSUSBNursingSimulator.Utility
{
    public class AwsAuthorization : IAuthorizationInterface
    {
        private Configuration Cfg = new Configuration();

        public AwsAuthorization(string FilePath = "cred.cfg")
        {
            FileName = FilePath;
            LoadCredentialsFromFile();
        }

        public string SecretKey { get; protected set; }

        public string SecretToken { get; protected set; }

        public string AccessToken { get; protected set; }

        public string FileName { get; protected set; }

        public void LoadCredentialsFromFile()
        {
            Cfg = Configuration.LoadFromFile(FileName);
            SecretKey = Cfg["AWSCredentials"]["SecretKey"].StringValue;
            SecretToken = Cfg["AWSCredentials"]["SecretToken"].StringValue;
            AccessToken = Cfg["AWSCredentials"]["AccessToken"].StringValue;
        }
    }
}