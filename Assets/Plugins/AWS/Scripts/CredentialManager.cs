using SharpConfig; // https://github.com/cemdervis/SharpConfig
using System.IO;
using Amazon.Runtime;

namespace AWS
{
    public static class CredentialManager
    {

        private static string AWSaccessKey;
        private static string AWSsecretKey;
        private static string AWSsecretToken;

        private static Amazon.RegionEndpoint AWSregionEndpoint = Amazon.RegionEndpoint.USEast1; //TODO allow change of region

        private static AWSCredentials AWSCredintial;

        static CredentialManager() {
            UpdateCredentials();
        }

        /// returns user AWSCredential object
        public static AWSCredentials getCredential()
        {
            return AWSCredintial;
        }

        /// returns user AWSRegionEndpoint object
        public static Amazon.RegionEndpoint getRegionEndpoint()
        {
            return AWSregionEndpoint;
        }

        private static void UpdateCredentials()
        {
            LoadCredentialsFromConfigFile();

            if (AWSaccessKey == "")
            {
                UnityEngine.Debug.LogError("AWS credentials file is empty!");
            }

            if (AWSsecretToken != "")
            {  //if AWSsecretToken is used
                AWSCredintial = new SessionAWSCredentials(AWSaccessKey, AWSsecretKey, AWSsecretToken);
            }
            else
            {   //if AWSsecretToken is not used
                AWSCredintial = new BasicAWSCredentials(AWSaccessKey, AWSsecretKey);
            }
        }

        //FIXME private
        public static void LoadCredentialsFromConfigFile()
        {

            string settingsFileName = "AWS_CONFIG.cfg";
            Configuration AWScfg = new Configuration();

            //if file not found, create default file
            if (!File.Exists(settingsFileName))
            {
                AWScfg["default"]["aws_access_key_id"].StringValue = "";
                AWScfg["default"]["aws_secret_access_key"].StringValue = "";
                AWScfg["default"]["aws_session_token"].StringValue = "";

                AWScfg.SaveToFile(settingsFileName);
            }

            AWScfg = Configuration.LoadFromFile(settingsFileName);

            //Load data from config file
            AWSaccessKey = AWScfg["default"]["aws_access_key_id"].StringValue;
            AWSsecretKey = AWScfg["default"]["aws_secret_access_key"].StringValue;
            AWSsecretToken = AWScfg["default"]["aws_session_token"].StringValue;
        }

    }
}
