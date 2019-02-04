using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentNurse : MonoBehaviour
{
    [SerializeField] private Text voiceToText;
    public bool isInterrupting = false;

    private AI.PatientAI patientAI;

	// Use this for initialization
	void Start ()
    {
        patientAI = GameObject.FindObjectOfType<AI.PatientAI>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        InterruptPatient();

    }

    /// <summary>
    /// Function that determines if the student nurse is done speaking a sentence/question
    /// The next phase would be to parse what was just said.
    /// </summary>
    /// <returns></returns>
    public bool IsSpeechFinalized()
    {
        if (voiceToText.text.Contains("Final"))
        {
            //voiceToText.text = "";
            Debug.Log("Dialogue Finalized");
            return true;
        }
        else
        {
            return false;
        }
    }

    private void InterruptPatient()
    {
        if (voiceToText.text.Contains("Interim") && patientAI.IsTalking)
        {
            //voiceToText.text = "";
            Debug.Log("Patient Interupted");
            isInterrupting = true;
        }
        else
        {
            //Debug.Log("Patient Not Interupted");
            isInterrupting = false;
        }
    }

    /// <summary>
    /// Function that gets the voice from the student nurse as an interuption
    /// Should be checked when patient is in the middle of talking
    /// </summary>
    /// <returns></returns>
    public bool IsPatientInterupted(AI.PatientAI patient)
    {
        if (isInterrupting)
        {
            //voiceToText.text = "";
            //Debug.Log("Patient Interupted");
            return true;
        }
        else
        {
            //Debug.Log("Patient Not Interupted");
            return false;
        }
        /*
        else if((voiceToText.text != "" && !patient.IsTalking) || (voiceToText.text == "" && patient.IsTalking))
        {
            Debug.Log("Patient Not Interupted");
            return false;
        }
        else
        {
            Debug.Log("Both Patient Interuption conditions are not met!");
            return false;
        }
        */
    }
}
