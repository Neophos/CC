using UnityEngine;
using UnityEditor;

public class YourClassAsset
{
    [MenuItem("Assets/Create/Data")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Characterdata>();
    }
}