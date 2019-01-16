using Amazon.TranscribeService;
using Amazon.TranscribeService.Model;

namespace AWS
{
    public static class Transcribe
    {
        /// <summary>
        /// Run the aws transcribe service on an audio file within the s3 bucket.
        /// </summary>
        /// <param name="audioFileName">The audio file to be transcribed from the S3 bucket.</param>
        public static void RunTranscribe(string audioFileName)
        {
            AmazonTranscribeServiceClient client = new AmazonTranscribeServiceClient(CredentialManager.getCredential(), CredentialManager.getRegionEndpoint());
            StartTranscriptionJobRequest req = new StartTranscriptionJobRequest();

            req.MediaFormat = MediaFormat.Mp3;
            req.LanguageCode = LanguageCode.EnUS;
            req.Media = new Media();
            //Example format for media file uri: "s3://nursingbucket/bucket.mp3"
            req.Media.MediaFileUri = $"s3://nursingbucket/{audioFileName}.mp3";
            req.OutputBucketName = "";
            req.TranscriptionJobName = "";
            req.Settings = new Settings();
            //req.Settings.


            client.StartTranscriptionJob(req);
        }
        //TODO figure out a way to do this through an audio stream.

        public static void RunTranscribeStream()
        {
            
        }
    }
}
