using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

/// <summary>
/// Class that handles the audio clips for AI
/// </summary>
public class AudioPlayer : MonoBehaviour
{
    public AudioClip sampleClip;
    public List<string> sampleClips = new List<string>();
    public AudioSource a;

    private WWW www;

    // Use this for initialization
    void Start ()
    {
        FindAIAudioFiles();
    }
	
	// Update is called once per frame
	void Update ()
    {
        PlayAudioClip();
    }

    /// <summary>
    /// Sets the list of names of all existing audioClip of .wav or .mp3 extension
    /// </summary>
    private void FindAIAudioFiles()
    {
        Debug.Log(Directory.GetTempDirectory());
        foreach (string file in System.IO.Directory.GetFiles(Directory.GetTempDirectory()))
        {
            if (file.EndsWith(".wav") || file.EndsWith(".mp3"))
            {
                string clipName = System.IO.Path.GetFileName(file);
                sampleClips.Add(clipName);
                Debug.Log(clipName);
            }
        }
    }

    /// <summary>
    /// Test to play audio clip (will most likely be a public function that will call the coroutine in one frame)
    /// </summary>
    public void PlayAudioClip()
    {
        if (Input.GetKeyDown(KeyCode.Return))   // Comment this out when using for calling outside class
        StartCoroutine(DownloadAudioClip());
    }

    /// <summary>
    /// Test to set the audioClip path (will likely be a public function that will be called once)
    /// </summary>
    private WWW GetAIAudioClip()
    {
        if (sampleClips.Count > 0)
        {
            www = new WWW(Directory.GetTempDirectory() + sampleClips[Random.Range(0, sampleClips.Count)]);
        }
        else
        {
            www = new WWW(Directory.GetTempDirectory());
        }
        Debug.Log(www.url);

        return www;
    }
    
    /// <summary>
    /// Coroutine that waits for the www path, checks if it downloaded the audioClip, and plays audioClip in one shot
    /// </summary>
    /// <returns></returns>
    private IEnumerator DownloadAudioClip()
    {
        www = GetAIAudioClip();
        yield return www;
        Debug.Log(www.isDone);
        if (www.isDone)
        {
            if (www.GetAudioClip())
            {
                sampleClip = www.GetAudioClip(false, true);
                a.PlayOneShot(sampleClip);
            }
        }
    }
}
