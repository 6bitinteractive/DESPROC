using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogueTrigger : MonoBehaviour
{
    public bool questGroupEnabled;
    public Dialogue[] dialogueArray;

    // Move quest group to custom inspector later
    //[HideInInspector]
    public string questName;
    //[HideInInspector]
    public Dialogue[] questDialogueArray;
    public GameEvent dialogueEndTrigger;

    public void TriggerDialogue()
    {
        // Uses FindObjectOfType, find alternative methods for this later on
        FindObjectOfType<DialogueManager>().StartDialogue(this);
    }
}

// Console warning: Unexpected top level layout group! Missing GUILayout.EndScrollView/EndVertical/EndHorizontal
//[CustomEditor(typeof(DialogueTrigger))]
//public class DialogueTriggerEditor : Editor
//{
//    private SerializedObject m_Object;
//    private SerializedProperty m_Property;

//    public void OnEnable()
//    {
//        m_Object = new SerializedObject(target);
//    }

//    public override void OnInspectorGUI()
//    {
//        DialogueTrigger dt = (DialogueTrigger)target;

//        // Show default inspector property editor
//        DrawDefaultInspector();

//        serializedObject.Update();

//        dt.questGroupEnabled = EditorGUILayout.BeginToggleGroup("Quest Group:", dt.questGroupEnabled);
//        if (dt.questGroupEnabled)
//        {
//            dt.questName = EditorGUILayout.TextField("Quest Name:", dt.questName);

//            m_Property = m_Object.FindProperty("questDialogueArray");
//            EditorGUILayout.PropertyField(m_Property, new GUIContent("Quest Dialogue Array"), true);
//            m_Object.ApplyModifiedProperties();
//        }
//    }
//}