using System.Threading;
using AWS;
using UnityEngine;
using System.Collections;

public static class CloudManager	//TODO inactivity timeout
{
    //TODO implement AWS comprehend, S3, Transcribe

    private static int maxPollyThreads = 9;
    private static int runningPollyThreads = 0;
    private static Thread pollyThread = new Thread(pollyJob);
    private static Queue pollyJobQueue = new Queue();

    //private static Thread pollyThreadArray = new Thread(() => pollyJob(textToSpeechInput));
    //pollyThreadArray.Start();


    ///helper class for safely starting polly threads
    private static void pollyJob(object pollyInputText)
    {
        Polly.runPolly((string)pollyInputText);
        runningPollyThreads--;
    }

    /// spawns a thread to request text-to-speech audio from AWSPolly.
    /// <param name="textToSpeechInput">text that will be converted into audio</param>
    public static void startPollyJob(string textToSpeechInput)
    {
        pollyJobQueue.Enqueue(textToSpeechInput);
        if (runningPollyThreads < maxPollyThreads)
        {
            runningPollyThreads++;

            pollyThread = new Thread(() => pollyJob(textToSpeechInput));

            //TODO test this
        }
    }

    public static void joinPollyJobs()
    {
        ThreadPool.QueueUserWorkItem(_ => pollyJob());
    }
}