using UnityEngine;
using System.Threading;
using AI.Parser;

namespace AI
{
    //class that spawns inputParser threads and manages interaction with parser data
    //TODO should this clsss be static?
    public class ParserManager
    {

        //private Thread parserThread = new Thread(inputParserJob);

        public ParserManager(Speech.SpeechOrganizerArrayObject speechOrganizerListObj)
        {
            if (speechOrganizerListObj == null)
            {
                Debug.LogError("ParserData Initialization Error: SpeechOrganizerArray is null.");
                return;
            }
            ParserData.Initialize(speechOrganizerListObj);
        }

        //void startSearch(inputText)
        //clear parser data during start jobs

        private void inputParserJob(string input)
        {
            
            InputParser parser = new InputParser(1, input);
        }


        //getResult() => equals null until search is complete
    }
}
