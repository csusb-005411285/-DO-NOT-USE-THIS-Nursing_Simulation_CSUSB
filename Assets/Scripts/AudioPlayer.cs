using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip sampleClip;
    public AudioSource a;
    public string audioDirectoryPath = @"C:\Users\Ryan\Desktop\AudioFolder\";
    public string audioFileName;
    public string audioTypeExtension = ".mp3";
    public AudioType audioType = AudioType.MPEG;

    private WWW www;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayAudioClip();

        SetAudioClip();
    }

    private void PlayAudioClip()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(DownloadAudioClip());
        }
    }

    private void SetAudioClip()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetAudioTypeExtension();

            www = new WWW(audioDirectoryPath + "ring" + audioTypeExtension);
            Debug.Log(www.url);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetAudioTypeExtension();

            www = new WWW(audioDirectoryPath + "menuselect" + audioTypeExtension);
            Debug.Log(www.url);
        }
    }

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
}
