using UnityEngine;
using UnityEditor;

namespace Speech
{
    public static class GenerateOutputClips
    {

        //TODO properly generate and attach audio for all output objects
        //will need to extend cloud manager for this purpose

        public static void GenerateClips()
        {
            string[] foundObjects = AssetDatabase.FindAssets("t:SpeechOutput");
            foreach (string objects in foundObjects)
            {
                SpeechOutput mySpeechObj = (SpeechOutput)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(objects), typeof(SpeechOutput));
                string objName = mySpeechObj.outputPhrases[0];
                OutputClip myClip = new OutputClip();
                myClip.outputPhrase = "hello friend" + (string)objName;
                AssetDatabase.CreateAsset(myClip, "Assets/SpeechIO/_GeneratedOutput/"+ objName + "myObj.asset");

            }
        }
    }
}
