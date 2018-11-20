using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.IO;
using System;
using System.Threading.Tasks;

namespace AWS
{
    public class S3
    {

        private static string bucketName = "";
        private static string keyName = "";
        private static string filePath = "";
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

        //public static void CreateBucket()
        //{
        //    s3Client = new AmazonS3Client(CredentialManager.getCredential(), bucketRegion);
        //    try
        //    {
        //        if (AmazonS3Util.DoesS3BucketExist(s3Client, bucketName))
        //        {
        //            var putBucketRequest = new PutBucketRequest()
        //            {
        //                BucketName = bucketName,
        //                UseClientRegion = true
        //            };

        //            PutBucketResponse putBucketResponse = s3Client.PutBucket(putBucketRequest);
        //        }
        //        //Not sure if these are needed:
        //        //var getBucketLocationRequest = new GetBucketLocationRequest();
        //        //string bucketLocation = getBucketLocationRequest.BucketName;
        //    }
        //    catch (AmazonS3Exception e)
        //    {

        //        Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
        //    }

        //}


        public static void UploadFile(string _filePath, string _bucketName, string _keyName)
        {
            s3Client = new AmazonS3Client(CredentialManager.getCredential(), bucketRegion);
            if (_filePath != "" && _bucketName != "" && _keyName != "")
            {
                filePath = _filePath;
                bucketName = _bucketName;
                keyName = _keyName;
            }
            try
            {
                var fileTransferUtility = new TransferUtility(s3Client);
                //upload full file
                fileTransferUtility.Upload(filePath, bucketName, keyName);//keyname is name of object in cloud, otherwise filename is used
                Console.WriteLine("File Upload Complete");    //TODO remove debugs after confirming this works
            }

            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message: '{e.Message}' when writing an object.");

            }

            //catch (Exception e)
            //{
            //    Debug.LogError($"Unknown encountered on server. Message: '{e.Message}' when writing an object.");
            //}

          

         
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
                Console.WriteLine("Filestream Upload Complete");    //TODO remove debugs after confirming this works
            }

            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message: '{e.Message}' when writing an object.");
            }

            //catch (Exception e)
            //{
            //    Debug.LogError($"Unknown encountered on server. Message: '{e.Message}' when writing an object.");
            //}

        }

    }
}
