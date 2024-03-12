using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueHandler))]
public class DropDownEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DialogueHandler script = (DialogueHandler)target;

        GUIContent ActorsList = new GUIContent("ActorsList");
        script.listIdx = EditorGUILayout.Popup(ActorsList, script.listIdx, script.actors.ToArray());
    }

}
