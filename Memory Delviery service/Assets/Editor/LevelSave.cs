using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(s_game))]
public class LevelSave : Editor {
    
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("SaveLevel")) {

            s_game lman = (s_game)target;
            lman.SaveLevel();
        }
        base.OnInspectorGUI();
    }
}