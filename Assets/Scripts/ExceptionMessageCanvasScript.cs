using UnityEngine;
using UnityEngine.UI;

public class ExceptionMessageCanvasScript : MonoBehaviour {

    private Canvas exceptionMessageCanvasObject;

    private Text exceptionMessageCanvasTextObject;

    [SerializeField]
    private bool showExceptionsOnScreen = false;

    void Awake()
    {
        Canvas[] canvasObjects = GetComponents<Canvas>();
        this.exceptionMessageCanvasObject = canvasObjects[0];
        Text[] canvasTextObjects = GetComponentsInChildren<Text>();
        this.exceptionMessageCanvasTextObject = canvasTextObjects[0];
        this.exceptionMessageCanvasObject.scaleFactor = 0;
    }

    void OnEnable()
    {
        Application.logMessageReceived += DisplayMessageOnCanvas;
    }

    private void DisplayMessageOnCanvas(string logString, string stackTrace, LogType type)
    {
        if (showExceptionsOnScreen && type == LogType.Exception)
        {
            this.exceptionMessageCanvasObject.scaleFactor = 2;
            this.exceptionMessageCanvasTextObject.text = logString + "\n Check the console for stack trace.";
            this.exceptionMessageCanvasTextObject.color = Color.red;
        }
    }

    void OnDisable()
    {
        Application.logMessageReceived -= DisplayMessageOnCanvas;
    }
}
