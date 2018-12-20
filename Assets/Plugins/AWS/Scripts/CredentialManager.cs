#if UNITY_EDITOR
using UnityEditor;
#endif
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

        /// returns user AWSCredential object
        public static AWSCredentials getCredential()
        {
            UpdateCredentials();
            return AWSCredintial;
        }

        /// returns user AWSRegionEndpoint object
        public static Amazon.RegionEndpoint getRegionEndpoint()
        {
            UpdateCredentials();
            return AWSregionEndpoint;
        }

        /// loads credential data from AWS_CREDENTIALS.cfg file
        private static void UpdateCredentials()
        {
            LoadCredentialsFromConfigFile();

            if (AWSaccessKey == "")
            {
                UnityEngine.Debug.LogError("AWS credentials missing in /AWS_CONFIG.cfg");
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

        //=======================================================================================================
        //SharpConfig related stuff

        private static void LoadCredentialsFromConfigFile()
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

#if UNITY_EDITOR
    [InitializeOnLoad]
    class CredentialManagerInitializer
    {
        static CredentialManagerInitializer(){
            CredentialManager.getCredential();
        }
    }
#endif
}
