
namespace AWS
{
    public static class CredentialManager
    {

        private static string AWSaccessKey = "";    //TODO allow credential setup from external file
        private static string AWSsecretKey = "";
        private static string AWSsecretToken = "";
        private static Amazon.RegionEndpoint AWSregionEndpoint = Amazon.RegionEndpoint.USEast1; //TODO allow change of region

        private static Credential AWSCredintial;

        static CredentialManager() { 
            if (AWSsecretToken != ""){  //if AWSsecretToken is used
            AWSCredintial = new Credential(AWSaccessKey, AWSsecretKey, AWSsecretToken);
            }
            //else{   //TODO if AWSsecretToken is not used
            //AWSCredintial = new Amazon.Runtime.AWSCredential(AWSaccessKey, AWSsecretKey);
            //    Amazon.Runtime.BasicAWSCredentials(AWSaccessKey, AWSsecretKey);
            //}
        }

        /// returns user AWSCredential object
        public static Credential getCredential()
        {
            return AWSCredintial;
        }

        /// returns user AWSRegionEndpoint object
        public static Amazon.RegionEndpoint getRegionEndpoint()
        {
            return AWSregionEndpoint;
        }
    }
}
