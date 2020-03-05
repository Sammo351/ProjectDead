using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor (typeof (EnemySpawner))]
public class EditorSpawner : Editor {

    protected virtual void OnSceneGUI () {
        EnemySpawner spawner = (EnemySpawner) target;

        EditorGUI.BeginChangeCheck ();
        Vector3 newTargetPosition = Handles.PositionHandle (spawner.destination, Quaternion.identity);
        if (EditorGUI.EndChangeCheck ()) {
            Undo.RecordObject (spawner, "Change Look At Target Position");
            spawner.destination = newTargetPosition;
        }
    }
}