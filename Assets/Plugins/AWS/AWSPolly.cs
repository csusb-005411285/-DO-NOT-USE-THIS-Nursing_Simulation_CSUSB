using System.IO;
using Amazon.Polly;
using Amazon.Polly.Model;
using UnityEngine;

namespace PollyLib
{
    public static class AWSPolly
    {

        private static AmazonPollyClient pc;

        static AWSPolly()
        {
            Amazon.Runtime.AWSCredential cred = new Amazon.Runtime.AWSCredential(
                "ASIA2RT5FL2BKHNXWLWQ",
                "ta0uE8DZk6FmcniacHdOGoObgoYx9cq17LCMxHR/",
                "FQoGZXIvYXdzEEcaDKuVyK/fzl2XG2u3NyKBAnmVS++LMNOcnCTfWpIQ2fmTGb5/mUK+8qSOb/eRhV4ENEPIVYnFTD7x0Ghsk9j0bdLDMLBaTrbyFrlhHGCPQ9vmJZM2p4BHf/ihn8u/587FnaNbRUIDSjhb+GlnV6noR9665RnD2LHp95Comv5+LzdosyQUxi071BHat4OqqOwt13nN2a4+X7HRtBJpv5D0XA47GBmBkY1oOZ1mKIpv/r7gaU5ReONGygJfg2QH3jqDULGeQ4KzXa5qIcgED1XuU6O9q2lwEFzbUjl/5PRRbpnj7Z0xe96keLLeuYMRDbgGCuj8dCYt+71ammVCBJ3gfYYbsGNkFX5GyIt6+naSYPX6KPz4id8F");

            pc = new AmazonPollyClient(cred, Amazon.RegionEndpoint.USEast1);
        }

        public static void runPolly(string inputText)
        {
            Debug.LogError("starting");

            SynthesizeSpeechRequest sreq = new SynthesizeSpeechRequest();
            sreq.Text = inputText;
            sreq.OutputFormat = OutputFormat.Mp3;
            sreq.VoiceId = VoiceId.Matthew;
            SynthesizeSpeechResponse sres = pc.SynthesizeSpeech(sreq);

            using (var fileStream = File.Create(@"Output.mp3"))

            {
                sres.AudioStream.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
            }
            Debug.LogError("done");
        }
    }
}
