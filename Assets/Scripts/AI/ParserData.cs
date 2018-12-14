using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public static class ParserData
    {
        //static class that holds data for threads

        public static string playerInput;

        public static bool[] speechOrganizerIsActive;

        public static bool[] speechOrganizerIsTriggered;

        public static void Initialize(Speech.SpeechOrganizerArrayObject speechOrganizerListObj)
        {
            Speech.SpeechOrganizer[] list = speechOrganizerListObj.speechOrganizerArray;
            speechOrganizerIsActive = new bool[list.Length];
            speechOrganizerIsTriggered = new bool[list.Length];
        }
    }
}
