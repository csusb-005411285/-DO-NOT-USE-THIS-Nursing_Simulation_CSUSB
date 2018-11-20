using UnityEngine;

namespace speech
{
    [CreateAssetMenu(menuName = "Speech/Speech Input Object")]
    public class SpeechInput : ScriptableObject
    {

        public string[] InputPhrases = new string[1];

    }
}
