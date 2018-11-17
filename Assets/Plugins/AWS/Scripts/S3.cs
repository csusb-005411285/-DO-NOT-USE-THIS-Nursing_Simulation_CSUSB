using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace AWS
{
    public class S3
    {

        private const string bucketName = "*** provide bucket name ***";
        private const string keyName = "*** provide a name for the uploaded object ***";
        private const string filePath = "*** provide the full path name of the file to upload ***";
        // Specify your bucket region (an example region is shown).
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        private static IAmazonS3 s3Client;

        //public static void Main()
        //{
        //    s3Client = new AmazonS3Client( bucketRegion);
        //    UploadFileAsync().Wait();
        //    //or
        //    UploadFileStreamAsync().Wait();
        //}

        public static void CreateBucket()
        {
            s3Client = new AmazonS3Client(CredentialManager.getCredential(), bucketRegion);
            try
            {
                if (AmazonS3Util.DoesS3BucketExist(s3Client, bucketName))
                {
                    var putBucketRequest = new PutBucketRequest()
                    {
                        BucketName = bucketName,
                        UseClientRegion = true
                    };

                    PutBucketResponse putBucketResponse = s3Client.PutBucket(putBucketRequest);
                }
                //Not sure if these are needed:
                //var getBucketLocationRequest = new GetBucketLocationRequest();
                //string bucketLocation = getBucketLocationRequest.BucketName;
            }
            catch (AmazonS3Exception e)
            {

                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }

        }


        private static void UploadFile()
        {
            s3Client = new AmazonS3Client(CredentialManager.getCredential(), bucketRegion);
            try
            {
                var fileTransferUtility = new TransferUtility(s3Client);
                //upload full file
                fileTransferUtility.Upload(filePath, bucketName, keyName);//keyname is name of object in cloud, otherwise filename is used
                Debug.Log("File Upload Complete");    //TODO remove debugs after confirming this works
            }

            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message: '{e.Message}' when writing an object.");

            }

            catch (Exception e)
            {
                Console.WriteLine($"Unknown encountered on server. Message: '{e.Message}' when writing an object.");
            }

          

         
        }

        private static void UploadFileStream()
        {
            s3Client = new AmazonS3Client(CredentialManager.getCredential(), bucketRegion);
            try
            {
                var fileTransferUtility = new TransferUtility(s3Client);

                //upload filestream
                using (var fileToUpload = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    fileTransferUtility.Upload(fileToUpload, bucketName, keyName);//keyname is name of object in cloud, otherwise filename is used
                }
                Debug.Log("Filestream Upload Complete");    //TODO remove debugs after confirming this works
            }

            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message: '{e.Message}' when writing an object.");
            }

            catch (Exception e)
            {
                Console.WriteLine($"Unknown encountered on server. Message: '{e.Message}' when writing an object.");
            }

        }

    }
}
