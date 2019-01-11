﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using NodeCanvas.BehaviourTrees;

namespace AI
{
    /// <summary>
    /// Class to set the necessary dialogue for AI patient
    /// </summary>
    public class PatientAI : MonoBehaviour
    {
        [Header("Dialogue Thread Checks for AI: ")]
        public bool isGreetDialogue;
        public bool isNameIntroDialogue;
        public bool isPlanIntroDialogue;
        public bool isAskingQuestionDialogue;
        public bool isAnsweringQuestionDialogue;

        //public bool[] speechTypeTriggered;
        public bool[] boolArrayTest;

        [Header("Dialogue Data for AI: ")]
        public Speech.SpeechOrganizerArrayObject speechOrganizerList;
        public string dialogueString;                   // String data for the dialogue
        public AudioClip dialogueAudioClip;             // AudioClip data for the dialogue

        [Header("Dialogue Debugging for AI: ")]
        public string debugErrorString = "DebugError Message";
        public AudioClip debugErrorSound;

        private bool isTalking = false;
        private ParserManager parserManager;
        private GlobalBlackboard gBlackboard;      // Global Blackboard is used to access the variables that all Node Canvas trees can used

        public bool GBlackboardReady { get { return gBlackboard; } }    // Checks to see if gBlackboard is initialized
        public bool IsTalking { get { return isTalking; } set { isTalking = value; } }

        /// <summary>
        /// Initializes variables upon scene awake
        /// </summary>
        private void Awake()
        {
            parserManager = new ParserManager(speechOrganizerList);
            Debug.Log(parserManager);
            gBlackboard = FindObjectOfType<GlobalBlackboard>();    // Initializes gBlackBoard but not before state machine plays
        }

        /// <summary>
        /// Update function for Beahavior Tree
        /// (Must be the only function on the node and on repeat. Also must be the only node in the behavior)
        /// (Any update behavior functions in this script can go in this function)
        /// </summary>
        public void BehaviorTreeUpdate()
        {
            GetComponent<BehaviourTreeOwner>().repeat = true;   // Enabled repeat for update behavior

            VerifyDialogueType();
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
            Speech.OutputClip[] outputClipList = parserManager.getOutputs();

            //FIXME handle multiple output clips
            foreach (Speech.OutputClip clip in outputClipList)
            {
                //FIXME
                //BROKEN needs to be able to handle multiple dialogues at once
            }

            if (outputClipList.Length != 0)
            {
                if (outputClipList[0].outputPhrase == null || outputClipList[0].GetAudioClip() == null)
                {
                    dialogueString = outputClipList[0].outputPhrase;
                    dialogueAudioClip = outputClipList[0].GetAudioClip();
                }
                else
                {
                    Debug.LogError("Output Clip List is not empty, but element(s) output phrase and/or audio clip are null!");
                    Debug.LogError("Dialogue cannot play now!");

                    dialogueString = debugErrorString;
                    dialogueAudioClip = debugErrorSound;
                }
            }
            else
            {
                Debug.LogError("Output Clip List is empty!");
                Debug.LogError("Dialogue cannot play now!");

                dialogueString = debugErrorString;
                dialogueAudioClip = debugErrorSound;
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
                Debug.LogError("Cannot find global blackboard for confirming dialogue end!");
            }
        }

        /// <summary>
        /// Gets the previous dialogue spoken by the AI after the student nurse interupts the patient
        /// </summary>
        /// <returns></returns>
        private string GetInteruptedDialogue()                              // Used to get the previous 
        {
            gBlackboard.SetValue("patientDialogue", "Please hold on and let me finish. As I was saying, ...");  // This could be different each time
            gBlackboard.SetValue("dialogueAudioClip", null);    // Null for right now
            return dialogueString;
        }

        /// <summary>
        /// Set the previous dialogue spoken by the AI back after the AI states that it was interupted by the student nurse
        /// </summary>
        public void SetInteruptedDialogue()
        {
            dialogueString = GetInteruptedDialogue();  // This can be replaced with a string variable if needed
        }

        /// <summary>
        /// A function for the BeahaviorTreeUpdate() that checks which dialogue type to play
        /// (boolArrayTest is used for testing at the moment)
        /// </summary>
        public void VerifyDialogueType()
        {
            /*
            if (parserManager != null)
            {
                Debug.Log(parserManager);

                if (parserManager.speechOrganizerWasTriggered.Length != 0 && parserManager.speechOrganizerSetActive.Length != 0)
                {
                    for (int i = 0; i < parserManager.speechOrganizerWasTriggered.Length; i++)
                    {
                        if (parserManager.speechOrganizerWasTriggered[i])
                        {
                            switch (i)
                            {
                                case 0:
                                    isGreetDialogue = true;
                                    break;
                                case 1:
                                    isNameIntroDialogue = true;
                                    break;
                                case 2:
                                    isPlanIntroDialogue = true;
                                    break;
                                case 3:
                                    isAskingQuestionDialogue = true;
                                    break;
                                case 4:
                                    isAnsweringQuestionDialogue = true;
                                    break;
                            }

                            parserManager.speechOrganizerSetActive[i] = false;
                        }
                    }
                }
            }
            */

            if(boolArrayTest[0])
            {
                isGreetDialogue = true;
                boolArrayTest[0] = false;
            }
            else if (boolArrayTest[1])
            {
                isNameIntroDialogue = true;
                boolArrayTest[1] = false;
            }
            else if (boolArrayTest[2])
            {
                isPlanIntroDialogue = true;
                boolArrayTest[2] = false;
            }
            else if (boolArrayTest[3])
            {
                isAskingQuestionDialogue = true;
                boolArrayTest[3] = false;
            }
            else if (boolArrayTest[4])
            {
                isAnsweringQuestionDialogue = true;
                boolArrayTest[4] = false;
            }
        }
    }
}