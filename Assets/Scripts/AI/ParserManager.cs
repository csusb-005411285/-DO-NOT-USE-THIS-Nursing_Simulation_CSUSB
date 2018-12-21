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
        public bool[] speechOrganizerSetActive;

        public ParserManager(Speech.SpeechOrganizerArrayObject speechOrganizerListObj)
        {
            if (speechOrganizerListObj == null)
            {
                Debug.LogError("ParserData Initialization Error: SpeechOrganizerArray is null.");
                return;
            }
            speechOrganizerSetActive = new bool[speechOrganizerListObj.speechOrganizerArray.Length];
            ParserData.Initialize(speechOrganizerListObj);
        }

        //void startSearch(inputText)
        //clear parser data during start jobs
        private bool searchIsRunning = false;

        /// <summary>
        /// Starts searching for a match between input and expected inputObjects, activate input objects with speechOrganizerSetActive[] array
        /// </summary>
        /// <param name="inputText">The text that the user is to say to the bot</param>
        /// <returns>True if search is started, false if search is still running</returns>
        public bool startSearch(string inputText)
        {
            if (searchIsRunning == true)
            {
                return false;
            }
            searchIsRunning = true;

            //clear Parser Data
            for (int i = 0; i < ParserData.speechOrganizerArray.Length; i++)
            {
                ParserData.speechOrganizerWasTriggered[i] = false;
                ParserData.closestString[i] = "";
                ParserData.closestStringScore[i] = 100;
            }

            int threadCount = 0;
            //not threaded currently, might benefit from threading when search space increases
            for (int i = 0; i < ParserData.speechOrganizerArray.Length; i++)
            {
                if (ParserData.speechOrganizerSetActive[i] == true) //only run parser on active Organizers
                {
                    threadCount++;
                    InputParser parserJob = new InputParser(i, inputText);
                }
            }
            if (threadCount == 0)
            {
                Debug.LogWarning("No speechOrganizers are active, set speechOrganizerSetActive[] elements before running startSearch()");
            }

            searchIsRunning = false;
            return true;    //return that search has started
        }

        private void inputParserJob(string input)
        {

            InputParser parser = new InputParser(1, input);
        }

        /// <summary>
        /// Returns a list of OutputClips that were triggered by input
        /// </summary>
        /// <returns>array of type Speech.outputClip</returns>
        public Speech.OutputClip[] getresult()
        {
            if (searchIsRunning == true)
            {
                return null;
            }

            Speech.OutputClip[] outputclipArray = new Speech.OutputClip[ParserData.speechOrganizerWasTriggered.Length]; //TODO make appropiate size
            for (int i = 0; i < ParserData.speechOrganizerWasTriggered.Length; i++)
            {
                if (ParserData.speechOrganizerWasTriggered[i] == true)
                {
                    //TODO
                    //add triggered output clips to array
                }
            }
            //TODO return OutputClip array
            return null; //FIXME
        }

    }
}