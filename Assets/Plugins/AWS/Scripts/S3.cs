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

        private static async Task UploadFileAsync()
        {
            var fileTransferUtility = new TransferUtility(s3Client);

            //upload full file
            await fileTransferUtility.UploadAsync(filePath, bucketName, keyName);//keyname is name of object in cloud, otherwise filename is used
            Debug.Log("File Upload Complete");    //TODO remove debugs after confirming this works
        }

        private static async Task UploadFileStreamAsync()
        {
            var fileTransferUtility = new TransferUtility(s3Client);

            //upload filestream
            using (var fileToUpload = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                await fileTransferUtility.UploadAsync(fileToUpload, bucketName, keyName);//keyname is name of object in cloud, otherwise filename is used
            }
            Debug.Log("Filestream Upload Complete");    //TODO remove debugs after confirming this works
        }

    }
}
