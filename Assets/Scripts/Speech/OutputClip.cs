using UnityEngine;

namespace Speech
{
    /// Scriptable object for holding output audio clips for a specific phrase
    [CreateAssetMenu(menuName = "Speech/Output Clip")]
    public class OutputClip : ScriptableObject
    {
        /// string of the output phrase this object holds audio for
        public string outputPhrase;
        /// list of audio clips for specific phrase, [0]=first character, [1]=second character, etc...
        public string[] outputClips = new string[1];  
    }
}

