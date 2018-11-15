using System.Threading;
using AWS;
using UnityEngine;

public static class CloudManager	//TODO inactivity timeout
{
    //TODO implement AWS comprehend, S3, Transcribe

    private static Thread pollyThread = new Thread(pollyJob);
    private static bool pollyThreadIsRunning;
    private static string pollyInputText;

    static CloudManager()
    {
        pollyThreadIsRunning = false;
    }

    ///helper class for safely starting polly threads
    private static void pollyJob()
    {
        Polly.runPolly(pollyInputText);
        pollyThreadIsRunning = false;
        //TODO call event now that polly audio file is ready
    }

    /// <summary>
    /// spawns a thread to request text-to-speech audio from AWSPolly.
    /// Only one thread allowed at a time. calls _//TODO_ event when file is ready
    /// </summary>
    /// <param name="textToSpeechInput">text that will be converted into audio</param>
    public static void startPollyJob(string textToSpeechInput)
    {
        if (pollyThreadIsRunning != true)   //only run one polly job (thread) at a time
        {
            pollyThreadIsRunning = true;
            pollyInputText = textToSpeechInput;
            pollyThread = null; //clean up old thread for garbage collection
            pollyThread = new Thread(pollyJob); //create new thread
            pollyThread.Start();
        }
        else
        {
            Debug.LogError("calling startPollyJob() too fast. Only one polly job at a time is allowed.");
        }
    }
}