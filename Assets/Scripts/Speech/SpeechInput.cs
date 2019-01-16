using UnityEngine;

namespace Speech
{
    /// Scriptable object for holding related input phrases for a subject
    [CreateAssetMenu(menuName = "Speech/Speech Input Object")]
    public class SpeechInput : ScriptableObject
    {
        /// <summary>
        /// list of similar phrases for input
        /// </summary>
        [TextArea]
        public string[] inputPhrases = new string[1];

        /// <summary>
        /// score of required closeness for a match to be considered, 0 means anything will match, 100 requires a perfect match
        /// </summary>
        [Range(0, 100)]
        public int threshold = 100;
    }
}
