using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class PatientAI : MonoBehaviour
{
    public GlobalBlackboard globalBlackboard;
    public string dialogueString;
    public AudioClip dialogueAudioClip;

	// Use this for initialization
	void Start () 
	{
        
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    public void SetDialogue()
    {
        globalBlackboard.SetValue("patientDialogue", dialogueString);
        globalBlackboard.SetValue("dialogueAudioClip", dialogueAudioClip);
    }
}
