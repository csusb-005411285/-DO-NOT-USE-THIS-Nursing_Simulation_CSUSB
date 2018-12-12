using System;
using System.IO;
using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using UnityEngine;
using Directory = Tools.Directory;

namespace AWS
{
    public interface ICredentialManager
    {
        AWSCredentials GetCredentials();

        RegionEndpoint GetRegionalEndpoint();
    }

    public class CredentialManagerUtility : ICredentialManager
    {
        public AWSCredentials GetCredentials()
        {
            return CredentialManager.getCredential();
        }

        public RegionEndpoint GetRegionalEndpoint()
        {
            return CredentialManager.getRegionEndpoint();
        }
    }

    public class PollyUtility
    {
        private readonly AmazonPollyClient _apc;

        private readonly AWSCredentials _credential;

        private readonly RegionEndpoint _endpoint;

        private readonly CredentialManagerUtility cmu;

        public PollyUtility()
        {
            cmu = new CredentialManagerUtility();
            _credential = cmu.GetCredentials();
            _endpoint = cmu.GetRegionalEndpoint();
            _apc = new AmazonPollyClient(_credential, _endpoint);
        }

        public SynthesizeSpeechResponse GetSynthesizedSpeechFromPolly(string pollyText)
        {
            var sreq = new SynthesizeSpeechRequest();
            sreq.Text = pollyText;
            sreq.OutputFormat = OutputFormat.Mp3;
            sreq.VoiceId = CharacterConfig.currentCharacter.pollyVoiceId;
            return _apc.SynthesizeSpeech(sreq);
        }

        public void GenerateMp3File(SynthesizeSpeechResponse sres, string pollyText)
        {
            var currentCharacterName = CharacterConfig.currentCharacter.name;
            var newDirectory = Directory.GetHomeDirectory() + @"\Assets\Audio\GeneratedOutput\" + currentCharacterName +
                               @"\";
            System.IO.Directory.CreateDirectory(newDirectory);

            using (var fileStream = File.Create(newDirectory + pollyText + ".mp3"))
            {
                sres.AudioStream.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
            }
        }
    }

    public static class Polly
    {
        private static AmazonPollyClient pc;

        static Polly() //TODO move credentials into external file
        {
            pc = new AmazonPollyClient(CredentialManager.getCredential(), CredentialManager.getRegionEndpoint());
        }

        public static void RunPolly(string pollyText)
        {
            try
            {
                var pu = new PollyUtility();
                var sres = pu.GetSynthesizedSpeechFromPolly(pollyText);
                pu.GenerateMp3File(sres, pollyText);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                throw;
            }
        }
    }
}