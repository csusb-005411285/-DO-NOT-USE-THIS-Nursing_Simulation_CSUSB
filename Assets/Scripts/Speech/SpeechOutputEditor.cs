#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Speech
{
    [CustomEditor(typeof(SpeechOutput), true)]//TODO see if custom editor can be toggled
    public class SpeechOutputEditor : Editor
    {
        private int characterVoiceSelection = 0;
        //TODO save current character, generate for all characters.

        private SpeechOutput speechOutput;
        private string thisObjectName;

        void OnEnable()
        {
            speechOutput = ((SpeechOutput)target);
            SerializedObject serializedObject1 = new SerializedObject(speechOutput);
            thisObjectName = serializedObject1.FindProperty("m_Name").stringValue;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Generate Audio Clips"))
            {
                speechOutput.outputAudioClipList = new OutputClip[speechOutput.outputPhrases.Length];

                string newDirectory = Tools.Directory.GetHomeDirectory() + @"Assets\SpeechIO\OutputClips\" + thisObjectName + @"\";
                if (System.IO.Directory.Exists(newDirectory))
                {
                    System.IO.Directory.Delete(newDirectory, true);
                }
                System.IO.Directory.CreateDirectory(newDirectory);
                AssetDatabase.Refresh();

                for (int i = 0; i < speechOutput.outputPhrases.Length; i++) //for each output Phrase, create OutputClip and generate Audio
                {
                    //Debug.Log("Generating audio for string: \"" + speechOutput.outputPhrases[i] + "\"");
                    CloudManager.StartPollyJob(speechOutput.outputPhrases[i]);

                    OutputClip myClip = ScriptableObject.CreateInstance<OutputClip>();
                    //PrepareOutputClip(myClip, currentSpeechOutputObject.outputPhrases[i]);

                    AssetDatabase.CreateAsset(myClip, "Assets/SpeechIO/OutputClips/"+thisObjectName+"/"+speechOutput.outputPhrases[i]+".asset");
                    speechOutput.outputAudioClipList[i] = myClip;

                    //set OutputClip parameters
                    myClip.outputPhrase = speechOutput.outputPhrases[i];
                }

                while (CloudManager.WaitForPollyJobs() != true)    //wait for audio clips to download
                {
                    System.Threading.Thread.Sleep(100);
                }

                for (int i = 0; i < speechOutput.outputPhrases.Length; i++) //for each output Phrase, attach audio to OutputClip
                {
                    //TODO attach audio files to audio objects
                }
                AssetDatabase.Refresh();
            }

        }
    }
}
#endif