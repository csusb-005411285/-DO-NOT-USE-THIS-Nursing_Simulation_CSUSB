using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        public bool isMisunderstandingDialogue;

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
        //private ParserManager ParserManager;
        private GlobalBlackboard gBlackboard;      // Global Blackboard is used to access the variables that all Node Canvas trees can used

        public bool GBlackboardReady { get { return gBlackboard; } }    // Checks to see if gBlackboard is initialized
        public bool IsTalking { get { return isTalking; } set { isTalking = value; } }  // Checks if AI is talking in each state

        public Queue<int> speechQueue = new Queue<int>();

        /// <summary>
        /// Initializes variables upon scene awake
        /// </summary>
        private void Awake()
        {
            ParserManager.Initialize(speechOrganizerList);
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
            //VerifyDialogueType();
        }

        /// <summary>
        /// Used to find a dialogue from scriptable object hierarchy starting from "speechOragnizerList"
        /// </summary>
        public void FindDialogue()
        {
            Speech.OutputClip[] outputClipList = ParserManager.getOutputs();

            if (outputClipList == null)
            {
                Debug.LogError("Output Clip List is not found! Dialogue cannot play now!");

                dialogueString = debugErrorString;
                dialogueAudioClip = debugErrorSound;
            }
            else
            {
                //FIXME handle multiple output clips
                foreach (Speech.OutputClip clip in outputClipList)
                {
                    //FIXME
                    //BROKEN needs to be able to handle multiple dialogues at once

                }

                if (outputClipList.Length != 0)
                {
                    if (outputClipList[0] == null)
                    {
                        Debug.LogError("Output Clip List element is not found! Dialogue cannot play now!");

                        dialogueString = debugErrorString;
                        dialogueAudioClip = debugErrorSound;
                    }
                    else
                    {
                        if (outputClipList[0].outputPhrase == null || outputClipList[0].GetAudioClip() == null)
                        {
                            dialogueString = outputClipList[0].outputPhrase;
                            dialogueAudioClip = outputClipList[0].GetAudioClip();
                        }
                        else
                        {
                            Debug.LogError("Output Clip List is not empty, but element(s) output phrase and/or audio clip are null! Dialogue cannot play now!");

                            dialogueString = debugErrorString;
                            dialogueAudioClip = debugErrorSound;
                        }
                    }
                }
                else
                {
                    Debug.LogError("Output Clip List is empty! Dialogue cannot play now!");

                    dialogueString = debugErrorString;
                    dialogueAudioClip = debugErrorSound;
                }
            }

        }

        /// <summary>
        /// Sets dialogue after finding the dialogue of type
        /// </summary>
        public void SetDialogue(BehaviourTree behaviour)
        {
            if (gBlackboard)
            {
                if (behaviour.name == "AI_Interupted")
                {
                    gBlackboard.SetValue("patientDialogue", "Please let me finish!");
                    gBlackboard.SetValue("dialogueAudioClip", null);
                }
                else
                {
                    gBlackboard.SetValue("patientDialogue", dialogueString);
                    gBlackboard.SetValue("dialogueAudioClip", dialogueAudioClip);
                }
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
        /// Function for the parser manager to interpret what the student nurse said
        /// Sets the audioClip and the text for the AI to speak in response to the student nurse
        /// </summary>
        /// <param name="speechText"></param>
        public void Interpret(Text speechText)
        {
            // Check all speechOrganizerSetActive jobs as true and disable later
            for (int i = 0; i < ParserManager.speechOrganizerSetActive.Length; i++)
            {
                ParserManager.speechOrganizerSetActive[i] = true;
            }

            // start search
            ParserManager.startSearch(speechText.text);
            speechText.text = "";

            Speech.OutputClip[] clipList;
            // check if search is null
            do
            {
                clipList = ParserManager.getOutputs();
            } while (clipList == null);

            // then output results from parse
            for (int i = 0; i < clipList.Length; i++)
            {
                //get audio clip and string
                if (ParserManager.speechOrganizerWasTriggered[i])
                {
                    FindDialogue();
                }
            }
        }

        /// <summary>
        /// Debug function for state machine to check for any available audio clips
        /// </summary>
        /// <returns></returns>
        public bool HasAudioClips()
        {
            Speech.OutputClip[] outputClipList = ParserManager.getOutputs();

            if (ParserManager.getOutputs() == null)
            {
                Debug.Log("Audio Clips List is not present!");
                return false;
            }
            else
            {
                Debug.Log("Audio Clips List is present!");

                if (outputClipList[0] == null)
                {
                    Debug.Log("Audio Clips List element is not present!");
                    return false;
                }
                else
                {
                    Debug.Log("Audio Clips List element is present!");
                    return true;
                }
            }
        }

        /// <summary>
        /// Debug function for the state machine to check if the ParserManager.speechOrganizerWasTriggered is null or not
        /// AI will undergo a misunderstanding behavior so it doesn't progress through the state machine
        /// </summary>
        /// <returns></returns>
        public bool ParseManagerInUse()
        {
            if (ParserManager.speechOrganizerWasTriggered != null)
            {
                isMisunderstandingDialogue = false;
                Debug.Log("ParseManager.speechOrganizerWasTriggered array is being used.");
                return true;
            }
            else
            {
                isMisunderstandingDialogue = true;
                Debug.LogError("ParseManager.speechOrganizerWasTriggered array is currently null! Cannot execute dialogue.");
                gBlackboard.SetValue("patientDialogue", "I'm sorry. Could you say that once more.");
                gBlackboard.SetValue("dialogueAudioClip", null);
                return false;
            }
        }

        /// <summary>
        /// A function for the BeahaviorTreeUpdate() that checks which dialogue type to play
        /// Also adds the dialogue type to the queue
        /// (boolArrayTest is used for testing at the moment)
        /// </summary>
        public void EnqueueDialogue()
        {
            int noDialogueCount = 0;

            if (ParserManager.speechOrganizerWasTriggered.Length != 0)
            {
                for (int i = 0; i < ParserManager.speechOrganizerWasTriggered.Length; i++)
                {
                    if (ParserManager.speechOrganizerWasTriggered[i])
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
                        speechQueue.Enqueue(i);
                       //ParserManager.speechOrganizerSetActive[i] = false;
                    }
                    else
                    {
                        Debug.Log("Speech Oraganizer was Triggered failed: " + noDialogueCount);
                        noDialogueCount++;
                    }
                }
                Debug.Log("Items in Speech Queue: " + speechQueue.Count);
                Debug.Log("First Speech Element in Speech Queue: " + speechQueue.Peek());
            }
            
            if (noDialogueCount >= ParserManager.speechOrganizerWasTriggered.Length)
            {
                Debug.Log("AI doesn't understand what student nurse is saying!");
                isMisunderstandingDialogue = true;
                gBlackboard.SetValue("patientDialogue", "I'm sorry. Could you say that once more.");
                gBlackboard.SetValue("dialogueAudioClip", null);
            }
            else
            {
                Debug.Log("AI understands what the student nurse is saying!");
            }

            /*
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
            */
        }

        /// <summary>
        /// Dequeues the oldest speech in the list
        /// </summary>
        public void DequeueDialogue()
        {
            speechQueue.Dequeue();
            Debug.Log("Items in Speech Queue: " + speechQueue.Count);
            Debug.Log("First Speech Element in Speech Queue: " + speechQueue.Peek());
        }

        /// <summary>
        /// Checks to see if the queue count is empty. If so, then the AI would go back into a Wait and Listen state.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool QueueDialogueCount(int count = 0)
        {
            Debug.Log("AI Dialogue in Queue: " + speechQueue.Count);
            return speechQueue.Count == count;
        }
    }
}