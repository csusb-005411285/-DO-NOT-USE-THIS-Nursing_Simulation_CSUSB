namespace Amazon.Runtime
{
    public class AWSCredential : AWSCredentials
    {
        ImmutableCredentials cred;
        public AWSCredential(string accessKey, string secretKey, string secretToken)
        {
            cred = new ImmutableCredentials(accessKey, secretKey, secretToken);
        }

        public override ImmutableCredentials GetCredentials()
        {
            return cred;
        }
    }
}


