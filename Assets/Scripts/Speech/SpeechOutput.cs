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
        public OutputClip[] outputAudioClipList = new OutputClip[0];

        /// <summary>
        /// integer of last output clip that was returned by GetOutputClip
        /// </summary>
        private int lastClipUsed;

        /// returns OutputClip of a phrase in OutputPhrases array
        /// <param name="phraseNumber">(optional) defaults to random phrase from OutputPhrases array, can specify specific phrase to output</param>
        /// <returns>OutputClip that corresponds with selected phrase, defaults to random</returns>
        public OutputClip GetOutputClip(int phraseNumber = -1)
        {
            if (outputAudioClipList.Length == 0)
            {
#if UNITY_EDITOR
                SerializedObject serializedObject1 = new SerializedObject(this);
                Debug.LogWarning(serializedObject1.FindProperty("m_Name").stringValue +
                    " has no attached audio clips in outputAudioClipList[], Generate or attach audioClips to fix");
#endif
                return null;
            }

            if (phraseNumber < 0)   //default, return random clip from list, avoid reusing previous clip if possible
            {
                if (outputAudioClipList.Length == 1)  //if list contains only one output, return that output
                {
                    return outputAudioClipList[0];
                }
                int clipToUse = Random.Range(0, outputAudioClipList.Length);
                while (clipToUse == lastClipUsed)    //pick random output that was not last one used
                {
                    clipToUse = Random.Range(0, outputAudioClipList.Length);
                }
                lastClipUsed = clipToUse;
                return outputAudioClipList[clipToUse];
            }
            else if (phraseNumber > outputAudioClipList.Length - 1) //if number is longer than list, return last
            {
                lastClipUsed = outputAudioClipList.Length;
                return outputAudioClipList[outputAudioClipList.Length-1];
            }
            else  //return specifically requested number
            {
                lastClipUsed = phraseNumber;
                return outputAudioClipList[phraseNumber];
            }
        }
    }
}
