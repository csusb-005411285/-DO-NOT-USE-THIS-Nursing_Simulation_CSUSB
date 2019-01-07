using UnityEngine;
using AI.Parser;
using System.Diagnostics;

namespace AI.Test
{
    //[CreateAssetMenu(menuName = "Test/Test Input Parsing")]
    public class _ParserTest : ScriptableObject
    {
        public Speech.SpeechOrganizerArrayObject speechOrganizerList;

        public bool verboseDebug = false;

        public bool timerOn = false;

        public bool onlyShowPassingThreshold = false;   //TODO use this, debug output weather passes threshold

        public int inputObjectToTestAgainst = -1;

        ///object to check against, -1 loops through all objects in SpeechOrganizer
        public string playerInputToTest = "Hello World!";

        public void startTest()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
#pragma warning disable 0219
            ParserManager parserManager = new ParserManager(speechOrganizerList);
#pragma warning restore 0219 //supress non usage warning

            UnityEngine.Debug.Log("Input: \"" + playerInputToTest + "\"");

            if (inputObjectToTestAgainst < 0)    //test against all inputs
            {
                for (int i = 0; i < speechOrganizerList.speechOrganizerArray.Length; i++)
                {
#pragma warning disable 0219
                    InputParser parserJob = new InputParser(i, playerInputToTest, verboseDebug);
#pragma warning restore 0219 //supress non usage warning

                    UnityEngine.Debug.Log("Closest String: "+ParserData.closestString[i]+"; Score: "+ParserData.closestStringScore[i]);
                }
            }
            else   //test against specified input object
            {
#pragma warning disable 0219
                InputParser parserJob = new InputParser(inputObjectToTestAgainst, playerInputToTest, verboseDebug);
#pragma warning restore 0219 //supress non usage warning

                UnityEngine.Debug.Log("Closest String: "+ParserData.closestString[inputObjectToTestAgainst]+"; Score: "+ParserData.closestStringScore[inputObjectToTestAgainst]);
            }

            sw.Stop();
            if (timerOn == true)
            {
                UnityEngine.Debug.Log("Elapsed= " + sw.Elapsed);
            }
        }
    }
}