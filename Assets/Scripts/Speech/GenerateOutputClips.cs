#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;


//Dont look at it, its so ugly.    run away now, ye have been warned


namespace Speech
{
    public static class GenerateOutputClips
    {

        //TODO properly generate and attach audio for all output objects
       
        /// set true to force regenerate all Generated AudioClips
        public static bool forceRegenerateAudioClips = false;
        /// set integer of character whose voices you want to generate
        public static int characterNumber = 0;

        /// only runs in editor, generates and populates all sound clips
        public static void GenerateClips()
        {
            string[] speechOutputObjectList = AssetDatabase.FindAssets("t:SpeechOutput");

            foreach(string speechOutputObject in speechOutputObjectList)
            {   //for all SpeechOutput files:
                
                SpeechOutput currentSpeechOutputObject = (SpeechOutput)AssetDatabase.LoadAssetAtPath(
                    AssetDatabase.GUIDToAssetPath(speechOutputObject), typeof(SpeechOutput));

                string objectName = currentSpeechOutputObject.name;

                for (int i = 0; i < currentSpeechOutputObject.outputPhrases.Length; i++)
                {   //for all phrases in each speech output object:
                    string currentOutputPhrase = currentSpeechOutputObject.outputPhrases[i];
                    if (currentOutputPhrase == "")
                    {
                        Debug.LogWarning(objectName+" skipped: empty line on list: "+i);
                        //to fix, open the object that was referenced in the warning and remove empty list items
                        continue;
                    }

                    if (System.IO.File.Exists(Tools.Directory.GetHomeDirectory()+@"Assets\SpeechIO\OutputClips\"+
                        objectName+@"\"+currentOutputPhrase+@".asset"))
                    {   // if clip already exists, modify it:
                        string[] existingSpeechOutputObjectList = AssetDatabase.FindAssets("n:" + currentOutputPhrase);
                        foreach (string existingSpeechOutputObject in existingSpeechOutputObjectList)   //generate for existing clips
                        {   //foreach in case there are multiple identical named objects in filesystem
                            if (existingSpeechOutputObjectList.Length > 1)
                            {
                                Debug.LogWarning(currentOutputPhrase + " exists multiple times! skipping...");
                                break;
                                //multiple objects of same name found, skip object
                            }
                            OutputClip currentExistingSpeechOutputObject = (OutputClip)AssetDatabase.LoadAssetAtPath(
                                AssetDatabase.GUIDToAssetPath(existingSpeechOutputObject), typeof(OutputClip));
                            PrepareOutputClip(currentExistingSpeechOutputObject, currentSpeechOutputObject.outputPhrases[i]);
                            //TODO queue OutputClip for population
                        }
                    }
                    else {  //generate new clip:
                        Debug.Log("Generating: "+objectName+": "+currentOutputPhrase);
                        OutputClip myClip = new OutputClip();
                        PrepareOutputClip(myClip, currentSpeechOutputObject.outputPhrases[i]);

                        string newDirectory = Tools.Directory.GetHomeDirectory() + @"Assets\SpeechIO\OutputClips\" + objectName + @"\";
                        System.IO.Directory.CreateDirectory(newDirectory);
                        AssetDatabase.Refresh();

                        AssetDatabase.CreateAsset(myClip, "Assets/SpeechIO/OutputClips/" + objectName + "/" + currentOutputPhrase + ".asset");
                        //TODO queue OutputClip for population
                    }
                }
            }

            while (CloudManager.WaitForPollyJobs() != true)
            {
                System.Threading.Thread.Sleep(100);
            }

            //TODO attach audio to SpeechOutputClips

            Debug.Log("Complete! check console output for any errors");
            //TODO make this a popup?^
        }
        
        private static void PrepareOutputClip(OutputClip clip, string phrase)
        {
            clip.outputPhrase = phrase;
            //TODO queue OutputClip here?

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
//TODO test edge case of multiple identical phrases

#endif