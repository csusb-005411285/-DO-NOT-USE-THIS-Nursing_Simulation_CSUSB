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

        public static void RunPolly(string pollyText)
        {
            string currentCharacterName = CharacterConfig.currentCharacter.name;

            SynthesizeSpeechRequest sreq = new SynthesizeSpeechRequest();
            sreq.Text = pollyText;
            sreq.OutputFormat = OutputFormat.Mp3;
            sreq.VoiceId = CharacterConfig.currentCharacter.pollyVoiceId;
            SynthesizeSpeechResponse sres = pc.SynthesizeSpeech(sreq);

            string newDirectory = Tools.Directory.GetHomeDirectory() + @"\Assets\Audio\GeneratedOutput\"+currentCharacterName+@"\";
            System.IO.Directory.CreateDirectory(newDirectory);

            using (var fileStream = File.Create(newDirectory+pollyText+".mp3"))
            {
                sres.AudioStream.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
            }
        }
    }
}
