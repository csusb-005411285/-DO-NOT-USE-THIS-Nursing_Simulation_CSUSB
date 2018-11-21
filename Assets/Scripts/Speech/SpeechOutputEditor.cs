//using UnityEngine;
//using UnityEditor;

//namespace Speech
//{
//    [CustomEditor(typeof(SpeechOutput), true)]
//    public class SpeechOutputEditor : Editor
//    {

//        //TODO create button to generate and play speech sample in editor properly
//        //will need to extend cloud manager for this purpose

//        //TODO reduce number of ((casts)) in this file

//        //int AudioClipSelection = 0;

//        public override void OnInspectorGUI()
//        {
//            DrawDefaultInspector();

//            if (GUILayout.Button("PreviewClip"))
//            {
//                //Debug.Log("Preview Clip: " + (string)((SpeechOutput)target).OutputPhrases[AudioClipSelection]);
//                Debug.LogWarning("preview not implemented");
//                //TODO
//            }

//            //string[] OutputPhraseArray = ((SpeechOutput)target).OutputPhrases;

//            //AudioClipSelection = EditorGUILayout.IntField("selection:", AudioClipSelection);
//            //if (AudioClipSelection > OutputPhraseArray.Length)
//            //{
//            //    string tmp = "Selection Out of Bounds";
//            //    EditorGUILayout.TextField("ERROR:", tmp);
//            //}

//            //if (OutputPhraseArray.Length > ((SpeechOutput)target).OutputClips.Length)
//            //{
//            //    AudioClip[] temp = new AudioClip[OutputPhraseArray.Length];
//            //    ((SpeechOutput)target).OutputClips.CopyTo(temp, 0);
//            //    ((SpeechOutput)target).OutputClips = temp;
//            //}
//            //else if (OutputPhraseArray.Length < ((SpeechOutput)target).OutputClips.Length)
//            //{
//            //    SerializedObject serializedObject1 = new UnityEditor.SerializedObject(((SpeechOutput)target));
//            //    Debug.LogError(serializedObject1.FindProperty("m_Name").stringValue +
//            //        ": OutputPhrasesArray shorter than OutputClipArray. Reduce the OutputClip Array size.");
//            //}
//        }
//    }
//}