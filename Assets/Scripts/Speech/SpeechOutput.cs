#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Speech
{
    /// Scriptable object for holding related output phrases for a subject
    [CreateAssetMenu(menuName = "Speech/Speech Output Object")]
    public class SpeechOutput : ScriptableObject
    {

        /// list of similar phrases for output
        [TextArea]
        public string[] outputPhrases = new string[1];
        
        /// array for holding outputClips for character
        public OutputClip[] outputAudioClipList;

        /// returns OutputClip of a phrase in OutputPhrases array
        /// <param name="phraseNumber">(optional) defaults to random phrase from OutputPhrases array, can specify specific phrase to output</param>
        /// <returns>OutputClip that corresponds with selected phrase, defaults to random</returns>
        public OutputClip GetOutputClip(int phraseNumber = -1)
        {
            if (outputAudioClipList.Length == 0)
            {
#if UNITY_EDITOR
                SerializedObject serializedObject1 = new SerializedObject(this);
                Debug.LogError(serializedObject1.FindProperty("m_Name").stringValue +
                    " has no attached audio clips in outputAudioClipList[], Generate or attach audioClips to fix");
#endif
                return null;
            }

            if (phraseNumber < 0)   //default, return random clip from list
            {
                return outputAudioClipList[Random.Range(0, outputPhrases.Length)];
            }
            else if (phraseNumber > outputPhrases.Length - 1) //if number is longer than list, return last
            {
                return outputAudioClipList[outputPhrases.Length];
            }
            else  //return specifically requested number
            {
                return outputAudioClipList[phraseNumber];
            }
        }
    }
}
