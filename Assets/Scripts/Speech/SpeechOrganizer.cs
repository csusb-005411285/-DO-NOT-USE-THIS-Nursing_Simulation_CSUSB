using UnityEngine;

namespace Speech
{
    [CreateAssetMenu(menuName = "Speech/Speech Object Organizer")]
    public class SpeechOrganizer : ScriptableObject
    {

        /// <summary>
        /// the Speech Input object with phrases that will trigger corresponding Output
        /// </summary>
        public SpeechInput speechInput;

        /// <summary>
        /// the Speech Output object with audio clips of the AI bots reply
        /// </summary>
        public SpeechOutput speechOutput;

    }
}
