using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

namespace AI
{
    /// <summary>
    /// Class to set the necessary dialogue for AI patient
    /// </summary>
    public class PatientAI : MonoBehaviour
    {
        public Speech.SpeechOrganizerArrayObject speechOrganizerList;
        ParserManager parserManager;

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
            parserManager = new ParserManager(speechOrganizerList);
            gBlackboard = FindObjectOfType<GlobalBlackboard>();    // Initializes gBlackBoard but not before state machine plays
        }

        /// <summary>
        /// Used to find a dialogue from scriptable object hierarchy starting from "speechOragnizerList"
        /// Element 0: Greeting Speech
        /// Element 1: NameIntro Speech
        /// Element 2: PlanIntro Speech
        /// Element 3: AskingQuestions Speech
        /// Element 4: AnsweringQuestions Speech
        /// Element 5: AddressingIssues Speech
        /// ...
        /// (Elements of Speech will be added over time)
        /// </summary>
        public void FindDialogue(int speechElement)
        {
            if (!speechOrganizerList.speechOrganizerArray[speechElement].speechOutput.GetOutputClip())
            {
                Debug.LogError("Cannot find OutputClip for dialogue string and audioClip! Play error string and sound instead!");
            }
            else
            {
                dialogueString = speechOrganizerList.speechOrganizerArray[speechElement].speechOutput.GetOutputClip().outputPhrase;
                dialogueAudioClip = speechOrganizerList.speechOrganizerArray[speechElement].speechOutput.GetOutputClip().GetAudioClip();
            }
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
}