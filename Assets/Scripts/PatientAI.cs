using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

/// <summary>
/// Class to set the necessary dialogue for AI patient
/// </summary>
public class PatientAI : MonoBehaviour
{
    [Header("Dialogue for AI: ")]
    public string dialogueString;                   // String data for the dialogue
    public AudioClip dialogueAudioClip;             // AudioClip data for the dialogue

    private GlobalBlackboard gBlackboard;      // Global Blackboard is used to access the variables that all Node Canvas trees can used

    public bool GBlackboardReady { get { return gBlackboard; } }    // Checks to see if gBlackboard is initialized

    /// <summary>
    /// Initializes variables upon scene awake
    /// </summary>
    private void Awake()
    {
        gBlackboard = FindObjectOfType<GlobalBlackboard>();    // Initializes gBlackBoard but not before state machine plays
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
        if (gBlackboard)
        {
            gBlackboard.SetValue("patientDialogue", dialogueString);
            gBlackboard.SetValue("dialogueAudioClip", dialogueAudioClip);
        }
        else
        {
            Debug.LogError("Cannot find global blackboard for setting dialogue!");
        }
    }

    /// <summary>
    /// Verifying that a dialogue is finished
    /// (Might be a temporary function until we check for flags)
    /// </summary>
    public void AIDialogueFinished()
    {
        if (gBlackboard)
        {
            gBlackboard.SetValue("isAIDialogueFinished", true);
        }
        else
        {
            Debug.LogError("Cannot find global blackboard for setting dialogue!");
        }
    }
}
