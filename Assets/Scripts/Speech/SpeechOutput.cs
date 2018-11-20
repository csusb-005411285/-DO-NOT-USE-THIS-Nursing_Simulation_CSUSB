using UnityEngine;

namespace speech
{
    [CreateAssetMenu(menuName = "Speech/Speech Output Object")]
    public class SpeechOutput : ScriptableObject
    {

        public string[] OutputPhrases = new string[1];
        public AudioClip[] OutputClips = new AudioClip[1]; //<- multi dimensional array to handle multiple "patient" voices?

    }
}
