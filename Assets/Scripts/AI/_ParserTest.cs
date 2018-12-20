using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.Parser;
using System.Diagnostics;

public class _ParserTest : MonoBehaviour
{
    public Speech.SpeechOrganizerArrayObject speechOrganizerList;

    void Start()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();


        string playerInput = "hello there"; //the string that the player would have said to the bot ========

        ParserManager parserManager = new ParserManager(speechOrganizerList);
        InputParser parserJob = new InputParser(0, playerInput, true);


        UnityEngine.Debug.Log("Closest String: " + ParserData.closestString[0]);
        UnityEngine.Debug.Log("Closest String Score: " + ParserData.closestStringScore[0]);

        sw.Stop();
        UnityEngine.Debug.Log("Elapsed= " + sw.Elapsed);
    }
}