using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class PatientAI : MonoBehaviour
{
    [Header("Dialogue for AI: ")]
    public string dialogueString;                   // String data for the dialogue
    public AudioClip dialogueAudioClip;             // AudioClip data for the dialogue

    private GlobalBlackboard globalBlackboard;      // Global Blackboard is used to access the variables that all Node Canvas trees can used

    /// <summary>
    /// Initializes variables upon scene awake
    /// </summary>
    private void Awake()
    {
        globalBlackboard = FindObjectOfType<GlobalBlackboard>();    
    }

    /// <summary>
    /// Used to find a greeting from a dialogue collection
    /// </summary>
    public void FindGreetingDialogue()
    {

    }

    /// <summary>
    /// Used to find a question to ask from a dialogue collection
    /// </summary>
    public void FindQuestionDialogue()
    {

    }

    /// <summary>
    /// Sets dialogue after finding the dialogue of type
    /// </summary>
    public void SetDialogue()
    {
        globalBlackboard.SetValue("patientDialogue", dialogueString);
        globalBlackboard.SetValue("dialogueAudioClip", dialogueAudioClip);
    }

    /// <summary>
    /// Verifying that a dialogue is finished
    /// (Might be a temporary function until we check for flags)
    /// </summary>
    public void AIDialogueFinished()
    {
        globalBlackboard.SetValue("isAIDialogueFinished", true);
    }
}
