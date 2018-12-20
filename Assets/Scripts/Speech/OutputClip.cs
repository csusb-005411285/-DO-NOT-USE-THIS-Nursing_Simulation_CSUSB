using UnityEngine;

namespace Speech
{
    /// Scriptable object for holding output audio clips for a specific phrase
    public class OutputClip : ScriptableObject
    {
        //TODO editor warning explaining the placement of audio files in array (how each item corresponds with a character)
        /// string of the output phrase this object holds audio for
        [TextArea]
        public string outputPhrase;

        /// list of audio clips for specific phrase, [0]=first character, [1]=second character, etc...
        [Tooltip("[0]=first character voice, [1]=second character voice, etc...")]
        public AudioClip[] outputAudioClips;

        /// returns the audio clip of the currently selected character in CharacterConfig
        /// <returns>AudioClip of the currently selected character in CharacteConfigr</returns>
        public AudioClip GetAudioClip()
        {
            int characterArraySelection = CharacterConfig.currentCharacter.characterNumber;
            if (outputAudioClips[CharacterConfig.currentCharacter.characterNumber] != null)
            {
                return outputAudioClips[CharacterConfig.currentCharacter.characterNumber];
            }
            else
            {
                Debug.LogError("audio missing error");//FIXME better comment
                return null;
            }
        }
    }
}

