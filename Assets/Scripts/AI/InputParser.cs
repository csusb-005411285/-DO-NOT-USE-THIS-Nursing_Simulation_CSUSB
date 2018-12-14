using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class InputParser {

        //the threaded logic for the input parses
        public InputParser(int threadNum, string input, Speech.SpeechOrganizer speechOrganizer)
        {
            string[] stringsToCompare = speechOrganizer.speechInput.inputPhrases;

            //comparison algorithm here

        }


    }
}
