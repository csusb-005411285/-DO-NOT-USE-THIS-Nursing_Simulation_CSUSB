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
    public AudioSource a;
    public string audioFileName;
    public string audioTypeExtension = ".mp3";
    public AudioType audioType = AudioType.MPEG;

    private WWW www;

    // Use this for initialization
    void Start ()
    {
        Debug.Log(Directory.GetTempDirectory());
	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayAudioClip();

        SetAudioClip();
    }

    /// <summary>
    /// Test to play audio clip (will most likely be a public function that will call the coroutine in one frame)
    /// </summary>
    private void PlayAudioClip()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(DownloadAudioClip());
        }
    }

    /// <summary>
    /// Test to set the audioClip path (will likely be a public function that will be called once)
    /// </summary>
    private void SetAudioClip()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetAudioTypeExtension();

            www = new WWW(Directory.GetTempDirectory() + "ring" + audioTypeExtension);
            Debug.Log(www.url);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetAudioTypeExtension();

            www = new WWW(Directory.GetTempDirectory() + "menuselect" + audioTypeExtension);
            Debug.Log(www.url);
        }
    }

    /// <summary>
    /// Sets the AudioType setting to check for either .mp3 or .wav files
    /// </summary>
    private void SetAudioTypeExtension()
    {
        switch(audioType)
        {
            case AudioType.MPEG:
                audioTypeExtension = ".mp3";
                break;
            case AudioType.WAV:
                audioTypeExtension = ".wav";
                break;
        }
    }

    /// <summary>
    /// Coroutine that waits for the www path, checks if it downloaded the audioClip, and plays audioClip in one shot
    /// </summary>
    /// <returns></returns>
    private IEnumerator DownloadAudioClip()
    {
        yield return www;
        Debug.Log(www.isDone);
        if (www.isDone)
        {
            sampleClip = www.GetAudioClip(false, true, audioType);
            a.PlayOneShot(sampleClip);
        }
    }

    /// <summary>
    /// A public function that allows a string to be passed in the argument to select the audioClip that will be played.
    /// </summary>
    /// <param name="clipName"></param>
    public void SetAudioClipName(string clipName)
    {
        audioFileName = clipName;
    }
}
