using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using AI.Parser;

namespace AI
{
    //class that spawns inputParser threads and manages interaction with parser data
    public class ParserManager
    {
        //ParserManager should not exist more than once
        private static bool parserManagerExists = false;

        //private Thread parserThread = new Thread(inputParserJob);

        public bool[] speechOrganizerSetActive;

        public bool[] speechOrganizerWasTriggered; //FIXME make this mirror ParserData in start job and get Output

        public ParserManager(Speech.SpeechOrganizerArrayObject speechOrganizerListObj)
        {
            if(parserManagerExists == true)
            {
                Debug.LogError("A ParserManager instance already exists.");
                return;
            }
            if (speechOrganizerListObj == null)
            {
                Debug.LogError("ParserData Initialization Error: SpeechOrganizerArray is null.");
                return;
            }
            parserManagerExists = true;
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
#pragma warning disable 0219
                    InputParser parserJob = new InputParser(i, inputText);
#pragma warning restore 0219 //supress non usage warning

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

#pragma warning disable 0219
            InputParser parser = new InputParser(1, input);
#pragma warning restore 0219 //supress non usage warning

        }

        /// <summary>
        /// Returns a list of OutputClips that were triggered by input, returns null when StartSearch() is running
        /// </summary>
        /// <returns>array of type Speech.outputClip</returns>
        public Speech.OutputClip[] getOutputs()
        {
            if (searchIsRunning == true)
            {
                return null;
            }

            List<Speech.OutputClip> outputClipList = new List<Speech.OutputClip>();
            for (int i = 0; i < ParserData.speechOrganizerWasTriggered.Length; i++)
            {
                if (ParserData.speechOrganizerWasTriggered[i] == true)
                {
                    outputClipList.Add(ParserData.speechOrganizerArray[i].speechOutput.GetOutputClip());
                }
            }
            
            return outputClipList.ToArray();
        }

    }
}