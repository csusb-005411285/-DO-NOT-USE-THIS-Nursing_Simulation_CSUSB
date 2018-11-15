using Amazon.Runtime;


//TODO see if this class can be removed entirely
namespace AWS
{
    public class Credential : AWSCredentials
    {
        ImmutableCredentials cred;
        public Credential(string accessKey, string secretKey, string secretToken)
        {
            cred = new ImmutableCredentials(accessKey, secretKey, secretToken);
        }

        public override ImmutableCredentials GetCredentials()
        {
            return cred;
        }
    }
}



