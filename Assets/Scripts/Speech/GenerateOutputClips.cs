#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;


//Dont look at it, its so ugly.    run away now, ye have been warned


namespace Speech
{
    public static class GenerateOutputClips
    {

        //TODO properly generate and attach audio for all output objects
        //will need to extend cloud manager for this purpose
        public static bool forceRegenerateAudioClips = false;

        public static void GenerateClips()
        {
            string[] speechOutputObjectList = AssetDatabase.FindAssets("t:SpeechOutput");

            foreach(string speechOutputObject in speechOutputObjectList)
            {
                
                SpeechOutput currentSpeechOutputObject = (SpeechOutput)AssetDatabase.LoadAssetAtPath(
                    AssetDatabase.GUIDToAssetPath(speechOutputObject), typeof(SpeechOutput));

                string objectName = currentSpeechOutputObject.name;

                for (int i = 0; i < currentSpeechOutputObject.outputPhrases.Length; i++)
                {

                    string currentOutputPhrase = currentSpeechOutputObject.outputPhrases[i];
                    if (currentOutputPhrase == "")
                    {
                        Debug.LogWarning(objectName+" skipped: empty line on list: "+i);
                        //to fix, open the object that was referenced in the warning and remove empty list items
                        continue;
                    }

                    if (System.IO.File.Exists(Tools.Directory.GetHomeDirectory()+@"Assets\SpeechIO\OutputClips\"+
                        objectName+@"\"+currentOutputPhrase+@".asset"))   //clip already exists
                    {
                        string[] existingSpeechOutputObjectList = AssetDatabase.FindAssets("n:" + currentOutputPhrase);
                        foreach (string existingSpeechOutputObject in existingSpeechOutputObjectList)   //generate for existing clips
                        {
                            OutputClip currentExistingSpeechOutputObject = (OutputClip)AssetDatabase.LoadAssetAtPath(
                                AssetDatabase.GUIDToAssetPath(existingSpeechOutputObject), typeof(OutputClip));
                            prepareOutputClip(currentExistingSpeechOutputObject, currentSpeechOutputObject.outputPhrases[i]);
                        }
                    }
                    else {  //generate new clip
                        Debug.Log("Generating: "+ objectName+": "+currentOutputPhrase);
                        OutputClip myClip = new OutputClip();
                        prepareOutputClip(myClip, currentSpeechOutputObject.outputPhrases[i]);

                        string newDirectory = Tools.Directory.GetHomeDirectory() + @"Assets\SpeechIO\OutputClips\" + objectName + @"\";
                        System.IO.Directory.CreateDirectory(newDirectory);
                        AssetDatabase.Refresh();

                        AssetDatabase.CreateAsset(myClip, "Assets/SpeechIO/OutputClips/" + objectName + "/" + currentOutputPhrase + ".asset");
                    }
                }
            }

            //TODO join all threads and attach audio to OutputClips
        }
        
        private static void prepareOutputClip(OutputClip clip, string phrase)
        {
            clip.outputPhrase = phrase;

            //TODO CloudManager.startPollyJob(phrase);

            if (forceRegenerateAudioClips == true)
            {
                ;//TODO?
            }
            //TODO find if audio is already generated
            //TODO boolean check for regenerating all generated files
        }

        public static void assignOutputClipsToSpeechOutput()
        {
            ;//TODO
        }
    }
}
#endif