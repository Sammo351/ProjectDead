using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor (typeof (Weapon))]
public class EditorWeapon : Editor {
    private static readonly Color bColor = new Color (0f, 0f, 1f, 0.1f);
    GameObject t;
    public Weapon GetPrimaryWeapon () {
        return t.GetComponent<Inventory> ().GetPrimaryWeapon ();
    }
    public override void OnInspectorGUI () {
        DrawDefaultInspector ();
        t = ((Weapon) target).gameObject;
        OnHeaderGUI ();

    }
    protected override void OnHeaderGUI () {
        var rect = EditorGUILayout.GetControlRect (false, 0f);
        rect.height = EditorGUIUtility.singleLineHeight;
        rect.y = -20;
        rect.x = 55;
        rect.xMax -= rect.x * 2f;
        Weapon wp = GetPrimaryWeapon ();
        bool isThisWeapon = wp == ((Weapon) target);
        if (!isThisWeapon) {
            return;
        }
        EditorGUI.DrawRect (rect, bColor);

        string header = isThisWeapon ? "\t\t\t" + wp.weaponName : "";

        EditorGUI.LabelField (rect, header, EditorStyles.boldLabel);
    }

}