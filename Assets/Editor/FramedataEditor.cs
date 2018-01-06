using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Framedata))]
public class FramedataEditor : Editor
{
    Framedata t;
    SerializedObject GetTarget;
    SerializedProperty moveList;
    SerializedProperty hitActive;
    SerializedProperty hitRecovery;
    SerializedProperty movesRef;
    SerializedProperty animation;
    SerializedProperty moveName;
    SerializedProperty movelistName;
    SerializedProperty input;
    SerializedProperty framesOfStartup;
    SerializedProperty numberOfHits;
    SerializedProperty activeAndRecovery;
    SerializedProperty framesOfRecovery;
    SerializedProperty hitData;
    SerializedProperty absoluteTrajectory;
    SerializedProperty customTrajectory;
    SerializedProperty customStun;
    SerializedProperty damage;
    SerializedProperty level;
    SerializedProperty typeOfAttack;
    SerializedProperty trajectoryPath;
    SerializedProperty blockDamageFactor;
    SerializedProperty initialStun;
    SerializedProperty hitstun;
    SerializedProperty blockstun;
    SerializedProperty sound;
    SerializedProperty condensedView;

    Globals globals;

    int ListSize;
    int totalFrames;

    float framedataBarHeight;
    float pixelsPerFrame;
    float widthOfSegment;
    float widthOfPreviousSegment;
    string labelText;
    GUIStyle text;
    int sd;

    void OnEnable()
    {
        t = (Framedata)target;
        GetTarget = new SerializedObject(t);
        moveList = GetTarget.FindProperty("moves"); // Find the List in our script and create a refrence of it

        text = new GUIStyle();
        text.normal.textColor = Color.black;
        text.fontSize = 14;
        text.fontStyle = FontStyle.Bold;

        globals = Resources.Load<Globals>("Globals");
    }

    public override void OnInspectorGUI()
    {

        Handles.BeginGUI();

        framedataBarHeight = 20.0f;

        //Update our list
        GetTarget.Update();

        if (GUILayout.Button("Add new move"))
        {
            t.moves.Add(new Framedata.Move());
        }


        EditorGUILayout.Space();
        //Display our list to the inspector window
        for (int i = 0; i < moveList.arraySize; i++)
        {
            #region Get Properties
            movesRef = moveList.GetArrayElementAtIndex(i);
            condensedView = movesRef.FindPropertyRelative("condensedView");
            animation = movesRef.FindPropertyRelative("animation");
            moveName = movesRef.FindPropertyRelative("name");
            movelistName = movesRef.FindPropertyRelative("movelistName");
            input = movesRef.FindPropertyRelative("input");
            framesOfStartup = movesRef.FindPropertyRelative("framesOfStartup");
            numberOfHits = movesRef.FindPropertyRelative("numberOfHits");
            activeAndRecovery = movesRef.FindPropertyRelative("activeAndRecovery");
            framesOfRecovery = movesRef.FindPropertyRelative("framesOfRecovery");
            hitData = movesRef.FindPropertyRelative("hitData");
            sound = movesRef.FindPropertyRelative("sound");
            #endregion

            if (condensedView.boolValue == true)
            {
                // Condensed view
                #region Name and toggle
                EditorGUILayout.BeginHorizontal();

                EditorGUIUtility.labelWidth = 50.0f;
                movelistName.stringValue = EditorGUILayout.TextField("Move", movelistName.stringValue);

                condensedView.boolValue = EditorGUILayout.ToggleLeft("Condensed view", condensedView.boolValue);

                EditorGUILayout.EndHorizontal();
                #endregion
            }
            else
            {
                // Complete information view
                #region Graphical bars
                EditorGUIUtility.labelWidth = Screen.width / 5;

                Rect rect = GUILayoutUtility.GetRect(Screen.width - 40.0f, framedataBarHeight);

                totalFrames = framesOfStartup.intValue + framesOfRecovery.intValue;

                for (int m = 0; m < numberOfHits.intValue; m++)
                {
                    if (m == numberOfHits.intValue - 1)
                    {
                        hitActive = activeAndRecovery.GetArrayElementAtIndex(m).FindPropertyRelative("first");

                        totalFrames += hitActive.intValue;
                    }
                    else
                    {
                        hitActive = activeAndRecovery.GetArrayElementAtIndex(m).FindPropertyRelative("first");
                        hitRecovery = activeAndRecovery.GetArrayElementAtIndex(m).FindPropertyRelative("second");

                        totalFrames += hitActive.intValue + hitRecovery.intValue;
                    }
                }

                pixelsPerFrame = (rect.width) / (totalFrames);

                for (int k = 0; k < 1 + 1 + (numberOfHits.intValue); k++)
                {
                    if (k == 0)
                    {// Startup
                        widthOfSegment = pixelsPerFrame * framesOfStartup.intValue;

                        if (framesOfStartup.intValue > 0)
                        {
                            Handles.DrawSolidRectangleWithOutline(new Vector3[4]{
                            new Vector3(rect.x, rect.y), 
                            new Vector3(rect.x + widthOfSegment, rect.y), 
                            new Vector3(rect.x + widthOfSegment, rect.y + rect.height), 
                            new Vector3(rect.x, rect.y + rect.height)},
                            new Color(230.0f / 255.0f, 70.0f / 255.0f, 70.0f / 255.0f), Color.black);

                            labelText = framesOfStartup.intValue.ToString();

                            Handles.Label(new Vector3(rect.x + widthOfSegment / 2 - 4, rect.y), labelText, text);
                        }

                        widthOfPreviousSegment = widthOfSegment;
                    }
                    else if (k == 1 + 1 + (numberOfHits.intValue) - 1)
                    {// Recovery
                        //widthOfSegment = pixelsPerFrame * framesOfRecovery.intValue;

                        if (framesOfRecovery.intValue > 0)
                        {
                            Handles.DrawSolidRectangleWithOutline(new Vector3[4]{
                            new Vector3(rect.x + widthOfPreviousSegment, rect.y), 
                            new Vector3(rect.x + rect.width, rect.y), 
                            new Vector3(rect.x + rect.width, rect.y + rect.height), 
                            new Vector3(rect.x + widthOfPreviousSegment, rect.y + rect.height)},
                            new Color(20.0f / 255.0f, 128.0f / 255.0f, 20.0f / 255.0f), Color.black);

                            labelText = framesOfRecovery.intValue.ToString();

                            Handles.Label(new Vector3(rect.x + widthOfPreviousSegment + ((rect.width - widthOfPreviousSegment) / 2) - 4, rect.y), labelText, text);
                        }
                        //widthOfPreviousSegment = widthOfSegment;
                    }
                    else if (k == 1 + 1 + (numberOfHits.intValue) - 2)
                    {// Last hit
                        hitActive = activeAndRecovery.GetArrayElementAtIndex(k - 1).FindPropertyRelative("first");

                        widthOfSegment = pixelsPerFrame * hitActive.intValue;
                        if (hitActive.intValue > 0)
                        {
                            Handles.DrawSolidRectangleWithOutline(new Vector3[4]{
                            new Vector3(rect.x + widthOfPreviousSegment, rect.y), 
                            new Vector3(rect.x + widthOfPreviousSegment + widthOfSegment, rect.y), 
                            new Vector3(rect.x + widthOfPreviousSegment + widthOfSegment, rect.y + rect.height), 
                            new Vector3(rect.x + widthOfPreviousSegment, rect.y + rect.height)},
                            new Color(255.0f / 255.0f, 220.0f / 255.0f, 50.0f / 255.0f), Color.black);

                            labelText = hitActive.intValue.ToString();

                            Handles.Label(new Vector3(rect.x + widthOfPreviousSegment + (widthOfSegment / 2) - 4, rect.y), labelText, text);
                        }
                        widthOfPreviousSegment += widthOfSegment;
                    }
                    else
                    {// Hits
                        hitActive = activeAndRecovery.GetArrayElementAtIndex(k - 1).FindPropertyRelative("first");
                        hitRecovery = activeAndRecovery.GetArrayElementAtIndex(k - 1).FindPropertyRelative("second");

                        widthOfSegment = pixelsPerFrame * hitActive.intValue;
                        if (hitActive.intValue > 0)
                        {
                            Handles.DrawSolidRectangleWithOutline(new Vector3[4]{
                            new Vector3(rect.x + widthOfPreviousSegment, rect.y), 
                            new Vector3(rect.x + widthOfSegment + widthOfPreviousSegment, rect.y), 
                            new Vector3(rect.x + widthOfSegment + widthOfPreviousSegment, rect.y + rect.height), 
                            new Vector3(rect.x + widthOfPreviousSegment, rect.y + rect.height)},
                            new Color(255.0f / 255.0f, 220.0f / 255.0f, 50.0f / 255.0f), Color.black);

                            labelText = hitActive.intValue.ToString();

                            Handles.Label(new Vector3(rect.x + widthOfPreviousSegment + (widthOfSegment / 2) - 4, rect.y), labelText, text);
                        }
                        widthOfPreviousSegment += widthOfSegment;

                        if (hitRecovery.intValue > 0)
                        {

                            widthOfSegment = pixelsPerFrame * hitRecovery.intValue;

                            Handles.DrawSolidRectangleWithOutline(new Vector3[4]{
                                new Vector3(rect.x + widthOfPreviousSegment, rect.y), 
                                new Vector3(rect.x + widthOfSegment + widthOfPreviousSegment, rect.y), 
                                new Vector3(rect.x + widthOfSegment + widthOfPreviousSegment, rect.y + rect.height), 
                                new Vector3(rect.x + widthOfPreviousSegment, rect.y + rect.height)},
                                new Color(60.0f / 255.0f, 150.0f / 255.0f, 210.0f / 255.0f), Color.black);
                        
                            labelText = hitRecovery.intValue.ToString();

                            Handles.Label(new Vector3(rect.x + widthOfPreviousSegment + (widthOfSegment / 2) - 4, rect.y), labelText, text);

                            widthOfPreviousSegment += widthOfSegment;
                        }
                    }
                }
                #endregion

                #region Name and input and sound
                EditorGUIUtility.labelWidth = 0.0f;
                movelistName.stringValue = EditorGUILayout.TextField("Name in move list", movelistName.stringValue);
                moveName.stringValue = EditorGUILayout.TextField("Script name", moveName.stringValue);
                EditorGUIUtility.labelWidth = Screen.width / 5;
                input.stringValue = EditorGUILayout.TextField("Input", input.stringValue.ToUpper(), GUILayout.MaxWidth(200));
                #endregion

                #region Hit information
                framesOfStartup.intValue = EditorGUILayout.IntField("Startup", framesOfStartup.intValue, GUILayout.MaxWidth(200));

                if (framesOfStartup.intValue < 0)
                {
                    framesOfStartup.intValue = 0;
                }

                numberOfHits.intValue = EditorGUILayout.IntField("Hits", numberOfHits.intValue, GUILayout.MaxWidth(200));

                if (numberOfHits.intValue < 0)
                {
                    numberOfHits.intValue = 0;
                }

                if (activeAndRecovery.isArray)
                {
                    while (numberOfHits.intValue != activeAndRecovery.arraySize)
                    {
                        if (numberOfHits.intValue > activeAndRecovery.arraySize)
                        {
                            activeAndRecovery.InsertArrayElementAtIndex(activeAndRecovery.arraySize);
                        }
                        if (numberOfHits.intValue < activeAndRecovery.arraySize)
                        {
                            activeAndRecovery.DeleteArrayElementAtIndex(activeAndRecovery.arraySize - 1);
                        }
                    }
                }

                if (hitData.isArray)
                {
                    while (numberOfHits.intValue != hitData.arraySize)
                    {
                        if (numberOfHits.intValue > hitData.arraySize)
                        {
                            hitData.InsertArrayElementAtIndex(hitData.arraySize);
                        }
                        if (numberOfHits.intValue < hitData.arraySize)
                        {
                            hitData.DeleteArrayElementAtIndex(hitData.arraySize - 1);
                        }
                    }
                }

                if (numberOfHits.intValue >= 1)
                {
                    EditorGUI.indentLevel++;

                    for (int m = 0; m < numberOfHits.intValue; m++)
                    {
                        EditorGUIUtility.labelWidth = Screen.width / 3;

                        if (m == numberOfHits.intValue - 1)
                        {
                            hitActive = activeAndRecovery.GetArrayElementAtIndex(m).FindPropertyRelative("first");

                            hitActive.intValue = EditorGUILayout.IntField("Active frames", hitActive.intValue, GUILayout.MaxWidth(200));

                            if (hitActive.intValue < 0)
                            {
                                hitActive.intValue = 0;
                            }
                        }
                        else
                        {
                            hitActive = activeAndRecovery.GetArrayElementAtIndex(m).FindPropertyRelative("first");
                            hitRecovery = activeAndRecovery.GetArrayElementAtIndex(m).FindPropertyRelative("second");

                            hitActive.intValue = EditorGUILayout.IntField("Active frames", hitActive.intValue, GUILayout.MaxWidth(200));
                            hitRecovery.intValue = EditorGUILayout.IntField("Recovery frames", hitRecovery.intValue, GUILayout.MaxWidth(200));

                            if (hitActive.intValue < 0)
                            {
                                hitActive.intValue = 0;
                            }
                            if (hitRecovery.intValue < 0)
                            {
                                hitRecovery.intValue = 0;
                            }
                        }

                            //[System.Serializable]
                            //public class HitPacket
                            //{
                            //    // Settings
                            //    public bool absoluteTrajectory;
                            //    public bool customTrajectory;
                            //    public bool customStun;
                            //    public bool floatsOnHit;
                            //    public bool floatsOnBlock;
        
                            //    // Always set by hand
                            //    public int damage;
                            //    public int level;
                            //    public TypeOfAttack typeOfAttack;
        
                            //    // Custom according to settings, otherwise automatically set
                            //    public Vector2 trajectoryPath;
                            //    public float blockDamageFactor;
                            //    public int initialStun;
                            //    public int hitstun;
                            //    public int blockstun;
                            //}

                        absoluteTrajectory = hitData.GetArrayElementAtIndex(m).FindPropertyRelative("absoluteTrajectory");
                        customTrajectory = hitData.GetArrayElementAtIndex(m).FindPropertyRelative("customTrajectory");
                        customStun = hitData.GetArrayElementAtIndex(m).FindPropertyRelative("customStun");
                        damage = hitData.GetArrayElementAtIndex(m).FindPropertyRelative("damage");
                        level = hitData.GetArrayElementAtIndex(m).FindPropertyRelative("level");
                        typeOfAttack = hitData.GetArrayElementAtIndex(m).FindPropertyRelative("typeOfAttack");
                        trajectoryPath = hitData.GetArrayElementAtIndex(m).FindPropertyRelative("trajectoryPath");
                        blockDamageFactor = hitData.GetArrayElementAtIndex(m).FindPropertyRelative("blockDamageFactor");
                        initialStun = hitData.GetArrayElementAtIndex(m).FindPropertyRelative("initialStun");
                        hitstun = hitData.GetArrayElementAtIndex(m).FindPropertyRelative("hitstun");
                        blockstun = hitData.GetArrayElementAtIndex(m).FindPropertyRelative("blockstun");

                        EditorGUILayout.BeginHorizontal();

                        EditorGUIUtility.labelWidth = Screen.width / 3;

                        //Rect recta = GUILayoutUtility.GetRect(5.0f, framedataBarHeight);

                        //absoluteTrajectory.boolValue = EditorGUI.ToggleLeft(GUILayoutUtility.GetRect(0.0f, 5.0f, 0.0f, framedataBarHeight), "Absolute trajectory", absoluteTrajectory.boolValue);
                        //customTrajectory.boolValue = EditorGUI.Toggle(new Rect(rect.x + Screen.width / 5.0f, rect.y, 1.0f, rect.height), "Custom trajectory", customTrajectory.boolValue);
                        //customStun.boolValue = EditorGUI.ToggleLeft(new Rect(rect.x + Screen.width / 1.5f, rect.y, rect.width, rect.height), "Custom stun", customStun.boolValue);

                        //customTrajectory.boolValue = EditorGUILayout.Toggle("Custom trajectory", customTrajectory.boolValue);

                        absoluteTrajectory.boolValue = EditorGUILayout.Toggle("Absolute trajectory", absoluteTrajectory.boolValue);
                        customTrajectory.boolValue = EditorGUILayout.Toggle("Custom trajectory", customTrajectory.boolValue);

                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();

                        EditorGUIUtility.labelWidth = Screen.width / 4;

                        level.intValue = EditorGUILayout.IntSlider("Level", level.intValue, 1, 5);
                        
                        customStun.boolValue = EditorGUILayout.Toggle("Custom", customStun.boolValue);

                        EditorGUILayout.EndHorizontal();

                        if (customTrajectory.boolValue == true)
                        {
                            // Angle
                        }

                        if (customStun.boolValue == true)
                        {
                            //EditorGUI.indentLevel++;

                            EditorGUILayout.BeginHorizontal();

                            EditorGUIUtility.labelWidth = 70.0f;

                            hitstun.intValue = EditorGUILayout.IntField("Hit stun", hitstun.intValue);

                            blockstun.intValue = EditorGUILayout.IntField("Block stun", blockstun.intValue);

                            initialStun.intValue = EditorGUILayout.IntField("Initial stun", initialStun.intValue);

                            EditorGUIUtility.labelWidth = Screen.width / 4;

                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.Space();

                            //EditorGUI.indentLevel--;
                        }

                        EditorGUILayout.BeginHorizontal();

                        damage.intValue = EditorGUILayout.IntField("Damage", damage.intValue);
                        blockDamageFactor.floatValue = EditorGUILayout.FloatField("Block Multiplier", blockDamageFactor.floatValue);

                        EditorGUILayout.EndHorizontal();

                            //public enum TypeOfAttack
                            //{
                            //    MID = 0,
                            //    LOW,
                            //    OVERHEAD,
                            //    THROW,
                            //    COMMANDTHROW,
                            //    AIRBLOCKABLE,
                            //    UNBLOCKABLE,
                            //    CATCH,
                            //    PARRY
                            //}

                        EditorGUILayout.PropertyField(typeOfAttack);

                        if (m < numberOfHits.intValue - 1)
                        {
                            rect = GUILayoutUtility.GetRect(Screen.width - 40.0f, 10.0f);

                            Handles.DrawLine(new Vector3(rect.x + 20.0f, rect.y + 5.0f), new Vector3(rect.x + rect.width - 20.0f, rect.y + 5.0f));
                        }
                    }

                    EditorGUI.indentLevel--;
                }

                framesOfRecovery.intValue = EditorGUILayout.IntField("Recovery", framesOfRecovery.intValue);

                if (framesOfRecovery.intValue < 0)
                {
                    framesOfRecovery.intValue = 0;
                }

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Total frames", totalFrames.ToString());

                if (numberOfHits.intValue > 0)
                {
                    EditorGUILayout.LabelField("Static Difference", ((GetStaticDifference() > 0) ? "+" : "") + GetStaticDifference().ToString());
                }

                EditorGUILayout.EndHorizontal();

                condensedView.boolValue = EditorGUILayout.ToggleLeft("Condensed view", condensedView.boolValue);

                #endregion

                #region Buttons at bottom
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Delete move"))
                {
                    if (EditorUtility.DisplayDialog("Really delete move?", "", "Yes", "No"))
                    {
                        moveList.DeleteArrayElementAtIndex(i);
                    }
                }

                if (GUILayout.Button("Move attack up"))
                {
                    moveList.MoveArrayElement(i, i - 1);
                }

                if (GUILayout.Button("Move attack down"))
                {
                    moveList.MoveArrayElement(i, i + 1);
                }

                EditorGUILayout.EndHorizontal();

                #endregion
            }

            Rect tempRect = GUILayoutUtility.GetRect(Screen.width - 40.0f, framedataBarHeight);

            //Handles.DrawLine(new Vector3(tempRect.x + 40.0f, tempRect.y + 10.0f), new Vector3(tempRect.x + tempRect.width - 40.0f, tempRect.y + 10.0f));

            Vector3[] squareverts = {new Vector3(tempRect.x + 30.0f, tempRect.y + 10.0f),
                               new Vector3(tempRect.x + tempRect.width - 30.0f, tempRect.y + 10.0f),
                               new Vector3(tempRect.x + tempRect.width - 30.0f, tempRect.y + 14.0f),
                               new Vector3(tempRect.x + 30.0f, tempRect.y + 14.0f)};

            Handles.DrawSolidRectangleWithOutline(squareverts, Color.white, Color.black);
        }

        //Apply the changes to our list
        GetTarget.ApplyModifiedProperties();

        Handles.EndGUI();
    }

    int GetStaticDifference()
    {
        int temp = 0;

        if (customStun.boolValue == true)
        {
            temp = blockstun.intValue;
        }
        else if (level.intValue == 1)
        {
            temp = globals.level1Blockstun;
        }
        else if (level.intValue == 2)
        {
            temp = globals.level2Blockstun;
        }
        else if (level.intValue == 3)
        {
            temp = globals.level3Blockstun;
        }
        else if (level.intValue == 4)
        {
            temp = globals.level4Blockstun;
        }
        else if (level.intValue == 5)
        {
            temp = globals.level5Blockstun;
        }

        sd = temp - (hitActive.intValue + framesOfRecovery.intValue);

        return sd;
    }
}