using UnityEngine;

namespace Speech
{
    [CreateAssetMenu(menuName = "Speech/Speech Object Organizer")]
    public class SpeechOrganizer : ScriptableObject
    {
        /// <summary>
        /// the Speech Input object with phrases that will trigger corresponding Output
        /// </summary>
        [SerializeField, Tooltip("Input object to check for")]
        private SpeechInput speechInput;

        /// <summary>
        /// the Speech Output object with audio clips of the AI bots reply
        /// </summary>
        [SerializeField, Tooltip("Output object that will be triggered by input")]
        private SpeechOutput speechOutput;

        //TODO summary
        public SpeechInput getInputObject()
        {
            return speechInput;
        }

        //TODO summary
        public SpeechOutput getOutputObject()
        {
            return speechOutput;
        }

    }
}
