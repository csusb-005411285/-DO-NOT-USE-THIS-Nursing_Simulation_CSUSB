using UnityEngine;
using System.Threading;

namespace AI
{
    public class ParserManager
    {
        //class that spawns inputParser threads

        //private Thread parserThread = new Thread(inputParserJob);

        public ParserManager(Speech.SpeechOrganizerArrayObject speechOrganizerListObj)
        {
            if (speechOrganizerListObj == null)
            {
                Debug.LogError("ParserData Initialization Error: SpeechOrganizerArray is null.");
                Time.timeScale = 0;
                return;
            }
            ParserData.Initialize(speechOrganizerListObj);
        }

        //void startSearch(inputText)

        private void inputParserJob()
        {

        }

        //TODO know what input string ws triggered for debug
    }
}
