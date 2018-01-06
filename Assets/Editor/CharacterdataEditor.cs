using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Characterdata))]
public class CharacterdataEditor : Editor
{
    Characterdata t;
    SerializedObject GetTarget;
    SerializedProperty charRef;
    SerializedProperty characterName;
    SerializedProperty forwardWalkingSpeed;
    SerializedProperty backwardsWalkingSpeed;
    SerializedProperty hasAForwardRun;
    SerializedProperty hasAForwardDash;
    SerializedProperty hasAForwardHoverDash;
    SerializedProperty hasAForwardTeleportDash;
    SerializedProperty forwarddashSpeed;
    SerializedProperty forwardrunSpeed;
    SerializedProperty hasABackRun;
    SerializedProperty hasABackDash;
    SerializedProperty hasABackTeleportDash;
    SerializedProperty hasABackHoverDash;
    SerializedProperty backdashSpeed;
    SerializedProperty backrunSpeed;
    SerializedProperty numberOfJumps;
    SerializedProperty airControlFactor;
    SerializedProperty verticalJumpSpeed;
    SerializedProperty horizontalJumpSpeed;

    Globals globals;

    int ListSize;
    int totalFrames;

    float characterBarHeight;
    float pixelsPerFrame;
    float widthOfSegment;
    float widthOfPreviousSegment;
    string labelText;
    GUIStyle text;
    int sd;

    void OnEnable()
    {
        t = (Characterdata)target;
        GetTarget = new SerializedObject(t);
        charRef = GetTarget.FindProperty("character");

        text = new GUIStyle();
        text.normal.textColor = Color.black;
        text.fontSize = 14;
        text.fontStyle = FontStyle.Bold;

        globals = Resources.Load<Globals>("Globals");
    }

    public override void OnInspectorGUI()
    {
        Handles.BeginGUI();

        characterBarHeight = 20.0f;

        //Update our list
        GetTarget.Update();

        EditorGUILayout.Space();
        //Display our list to the inspector window
        #region Get Properties

        characterName = charRef.FindPropertyRelative("characterName");
        forwardWalkingSpeed = charRef.FindPropertyRelative("forwardWalkingSpeed");
        backwardsWalkingSpeed = charRef.FindPropertyRelative("backwardsWalkingSpeed");
        hasAForwardRun = charRef.FindPropertyRelative("hasAForwardRun");
        hasAForwardDash = charRef.FindPropertyRelative("hasAForwardDash");
        hasAForwardHoverDash = charRef.FindPropertyRelative("hasAForwardHoverDash");
        hasAForwardTeleportDash = charRef.FindPropertyRelative("hasAForwardHoverDash");
        forwarddashSpeed = charRef.FindPropertyRelative("forwarddashSpeed");
        forwardrunSpeed = charRef.FindPropertyRelative("forwardrunSpeed");
        hasABackRun = charRef.FindPropertyRelative("hasABackRun");
        hasABackDash = charRef.FindPropertyRelative("hasABackDash");
        hasABackTeleportDash = charRef.FindPropertyRelative("hasABackTeleportDash");
        hasABackHoverDash = charRef.FindPropertyRelative("hasABackHoverDash");
        backdashSpeed = charRef.FindPropertyRelative("backdashSpeed");
        backrunSpeed = charRef.FindPropertyRelative("backrunSpeed");
        numberOfJumps = charRef.FindPropertyRelative("numberOfJumps");

        #endregion

        // Complete information view
        EditorGUIUtility.labelWidth = 0.0f;
        characterName.stringValue = EditorGUILayout.TextField("Character name", characterName.stringValue);

        forwardWalkingSpeed.intValue = EditorGUILayout.IntField("Forward walking peed", forwardWalkingSpeed.intValue);
        backwardsWalkingSpeed.intValue = EditorGUILayout.IntField("Backwards walking speed", backwardsWalkingSpeed.intValue);

        #region Forward dash
        EditorGUILayout.BeginHorizontal();

        EditorGUIUtility.labelWidth = Screen.width / 6;

        hasAForwardDash.boolValue = EditorGUILayout.Toggle("Dash", hasAForwardDash.boolValue);
        hasAForwardRun.boolValue = EditorGUILayout.Toggle("Run", hasAForwardRun.boolValue);
        hasAForwardHoverDash.boolValue = EditorGUILayout.Toggle("Hover", hasAForwardHoverDash.boolValue);
        hasAForwardTeleportDash.boolValue = EditorGUILayout.Toggle("Teleport", hasAForwardTeleportDash.boolValue);

        EditorGUILayout.EndHorizontal();

        if (hasAForwardDash.boolValue == true)
        {
            // Speed and length and if airborne
        }

        if (hasAForwardRun.boolValue == true)
        {
            // Speed and acceleration
        }

        if (hasAForwardHoverDash.boolValue == true)
        {
            // Angle
        }

        if (hasAForwardTeleportDash.boolValue == true)
        {
            // Angle
        }

        #endregion

        #region Back dash
        EditorGUILayout.BeginHorizontal();

        EditorGUIUtility.labelWidth = Screen.width / 6;

        hasABackDash.boolValue = EditorGUILayout.Toggle("Back dash", hasABackDash.boolValue);
        hasABackRun.boolValue = EditorGUILayout.Toggle("Back run", hasABackRun.boolValue);
        hasABackHoverDash.boolValue = EditorGUILayout.Toggle("Hover", hasABackHoverDash.boolValue);
        hasABackTeleportDash.boolValue = EditorGUILayout.Toggle("Hover", hasABackTeleportDash.boolValue);

        EditorGUILayout.EndHorizontal();

        if (hasABackDash.boolValue == true)
        {
            // Angle
        }

        if (hasABackRun.boolValue == true)
        {
            // Angle
        }

        if (hasABackHoverDash.boolValue == true)
        {
            // Angle
        }

        if (hasABackTeleportDash.boolValue == true)
        {
            // Angle
        }

        #endregion

        #region Jumps
        EditorGUIUtility.labelWidth = Screen.width / 3;
        numberOfJumps.intValue = EditorGUILayout.IntField("Number of jumps", numberOfJumps.intValue);
        #endregion

        Rect tempRect = GUILayoutUtility.GetRect(Screen.width - 40.0f, characterBarHeight);

        //Handles.DrawLine(new Vector3(tempRect.x + 40.0f, tempRect.y + 10.0f), new Vector3(tempRect.x + tempRect.width - 40.0f, tempRect.y + 10.0f));

        Vector3[] squareverts = {new Vector3(tempRect.x + 30.0f, tempRect.y + 10.0f),
                            new Vector3(tempRect.x + tempRect.width - 30.0f, tempRect.y + 10.0f),
                            new Vector3(tempRect.x + tempRect.width - 30.0f, tempRect.y + 14.0f),
                            new Vector3(tempRect.x + 30.0f, tempRect.y + 14.0f)};

        Handles.DrawSolidRectangleWithOutline(squareverts, Color.white, Color.black);

        //Apply the changes to our list
        GetTarget.ApplyModifiedProperties();

        Handles.EndGUI();
    }
}