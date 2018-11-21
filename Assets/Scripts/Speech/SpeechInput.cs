using UnityEngine;

namespace Speech
{
    /// Scriptable object for holding related input phrases for a subject
    [CreateAssetMenu(menuName = "Speech/Speech Input Object")]
    public class SpeechInput : ScriptableObject
    {
        /// linked SpeechOutput object
        public SpeechOutput outputObject;
        /// list of similar phrases for input
        public string[] inputPhrases = new string[1];
    }
}
