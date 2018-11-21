using UnityEngine;

namespace Speech
{
    /// Scriptable object for holding related output phrases for a subject
    [CreateAssetMenu(menuName = "Speech/Speech Output Object")]
    public class SpeechOutput : ScriptableObject
    {
        /// list of similar phrases for output
        public string[] outputPhrases = new string[1];
        /// array for holding outputClips for character
        private OutputClip[] outputClips = new OutputClip[1];

        /// returns OutputClip of a phrase in OutputPhrases array
        /// <param name="phraseNumber">defaults to random phrase from OutputPhrases array, can specify specific phrase to output</param>
        /// <returns>OutputClip that corresponds with selected phrase, defaults to random</returns>
        public OutputClip GetOutputClip(int phraseNumber = -1)
        {
            int characterArraySelection = CharacterManager.currentCharacter.characterNumber;

            if (phraseNumber < 0)   //default, return random clip from list
            {
                return outputClips[Random.Range(0, outputPhrases.Length)];
            }
            else if (phraseNumber > outputPhrases.Length-1) //if number is longer than list, return last
            {
                return outputClips[outputPhrases.Length];
            }
            else  //return specifically requested number
            {
                return outputClips[phraseNumber];
            }
        }

    }
}
