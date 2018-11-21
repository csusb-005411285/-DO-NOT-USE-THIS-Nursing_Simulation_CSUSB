using UnityEngine;

namespace Speech
{
    /// Scriptable object for holding output audio clips for a specific phrase
    [CreateAssetMenu(menuName = "Speech/Output Clip")]
    public class OutputClip : ScriptableObject
    {
        //TODO editor warning explaining the placement of audio files in array (how each item corresponds with a character)
        /// string of the output phrase this object holds audio for
        public string outputPhrase;
        /// list of audio clips for specific phrase, [0]=first character, [1]=second character, etc...
        public string[] outputAudioClips = new string[1];  
    }
}

