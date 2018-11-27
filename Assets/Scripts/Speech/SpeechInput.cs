using UnityEngine;

namespace Speech
{
    /// Scriptable object for holding related input phrases for a subject
    [CreateAssetMenu(menuName = "Speech/Speech Input Object")]
    public class SpeechInput : ScriptableObject
    {
        /// list of similar phrases for input
        [TextArea]
        public string[] inputPhrases = new string[1];
    }
}
