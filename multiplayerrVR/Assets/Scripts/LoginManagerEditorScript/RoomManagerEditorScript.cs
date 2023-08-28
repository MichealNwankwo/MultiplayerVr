#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
       EditorGUILayout.HelpBox("This Script is responsible for creating and joining rooms", MessageType.Info);

        RoomManager roomManager = (RoomManager)target;
        if (GUILayout.Button("Join Random Room"))
        {
            roomManager.JoinRandomRoom();
        }

        if (GUILayout.Button("JOIN SCHOOL ROOM"))
        {
            roomManager.OnEnterButtonClicked_School();
        }

        if (GUILayout.Button("JOIN OUTDOOR ROOM"))
        {
            roomManager.OnEnterButtonClicked_Outdoor();
        }
    }
}
#endif