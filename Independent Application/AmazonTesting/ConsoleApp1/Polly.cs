using System.IO;
using Amazon.Polly;
using Amazon.Polly.Model;

namespace AWS
{
    public static class Polly
    {

        private static AmazonPollyClient pc;

        static Polly()   //TODO move credentials into external file
        {
            pc = new AmazonPollyClient(CredentialManager.getCredential(), CredentialManager.getRegionEndpoint());
        }

        public static void runPolly(string inputText)   //TODO make polly config settings accessible from editor [sreq.XXXX]
        {
            SynthesizeSpeechRequest sreq = new SynthesizeSpeechRequest();
            sreq.Text = inputText;
            sreq.OutputFormat = OutputFormat.Mp3;
            sreq.VoiceId = VoiceId.Matthew;
            SynthesizeSpeechResponse sres = pc.SynthesizeSpeech(sreq);

            using (var fileStream = File.Create(@"PollyOutput.mp3"))

            {
                sres.AudioStream.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
            }
        }
    }
}
