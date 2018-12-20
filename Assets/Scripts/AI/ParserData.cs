using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI.Parser
{
    public static class ParserData
    {
        //static class that holds data for threads

        public static string playerInput;

        public static Speech.SpeechOrganizer[] speechOrganizerArray;

        public static bool[] speechOrganizerSetActive;

        public static bool[] speechOrganizerWasTriggered;

        public static string[] closestString;

        public static float[] closestStringScore;

        public static void Initialize(Speech.SpeechOrganizerArrayObject SOInput)
        {
            speechOrganizerArray = SOInput.speechOrganizerArray;
            speechOrganizerSetActive = new bool[speechOrganizerArray.Length];
            speechOrganizerWasTriggered = new bool[speechOrganizerArray.Length];
            closestString = new string[speechOrganizerArray.Length];
            closestStringScore = new float[speechOrganizerArray.Length];
        }
    }
}
