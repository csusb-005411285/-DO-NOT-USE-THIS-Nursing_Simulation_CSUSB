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

        private const string bucketName = "nursingbucket";
        private const string keyName = "testfile";
        private const string filePath = "D:/micInput.wav";
        // Specify your bucket region (an example region is shown).
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        private static IAmazonS3 s3Client;

        public static void UploadFile()
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
                Debug.LogError($"Error encountered on server. Message: '{e.Message}' when writing an object.");

            }
        }

        //FIXME does not work
        public static void UploadFileStream()
        {
            s3Client = new AmazonS3Client(CredentialManager.getCredential(), bucketRegion);
            try
            {
                var fileTransferUtility = new TransferUtility(s3Client);

                //upload filestream
                using (var fileToUpload = new FileStream(microphone.Microphone.waveFile.Filename, FileMode.Open, FileAccess.Read))//FIXME sumtin wrong here
                {
                    fileTransferUtility.Upload(fileToUpload, bucketName, keyName);//keyname is name of object in cloud, otherwise filename is used
                }
                Debug.Log("Filestream Upload Complete");    //TODO remove debugs after confirming this works
            }

            catch (AmazonS3Exception e)
            {
                Debug.LogError($"Error encountered on server. Message: '{e.Message}' when writing an object.");
            }
        }

    }
}
