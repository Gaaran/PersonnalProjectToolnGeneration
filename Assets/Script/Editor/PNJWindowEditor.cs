using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class PNJWindowEditor : EditorWindow {

	[MenuItem("Window/PNJ Editor/PNJ Window Editor")]
    static public void ShowWindow()
    {
        PNJWindowEditor window = CreateInstance<PNJWindowEditor>();
        window.titleContent = new GUIContent("PNJ window");

        window.Show();
    }

    PNJGenerator.StatsMinMax statsMin;
    PNJGenerator.Archetype type;

    public PNJWindowEditor()
    {
        statsMin = default(PNJGenerator.StatsMinMax);//zeroing the struct
        type = PNJGenerator.Archetype.paysant;
    }

    private void OnGUI()
    {
        
        GUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Dex :");
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Min : ");
        statsMin.dexMin = EditorGUILayout.IntField(statsMin.dexMin);
        GUILayout.Label("Max :");
        statsMin.dexMax = EditorGUILayout.IntField(statsMin.dexMax);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Intel :");
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Min :");
        statsMin.intelMin = EditorGUILayout.IntField(statsMin.intelMin);
        GUILayout.Label("Max : ");
        statsMin.intelMax = EditorGUILayout.IntField(statsMin.intelMax);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Str :");
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Min :");
        statsMin.strMin = EditorGUILayout.IntField(statsMin.strMin);
        GUILayout.Label("Max : ");
        statsMin.strMax = EditorGUILayout.IntField(statsMin.strMax);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Archetype : ");
        EditorGUILayout.EndHorizontal();

        type = (PNJGenerator.Archetype)EditorGUILayout.EnumPopup(type);

        if (GUILayout.Button("Save into Jason"))
        {
            SaveWindowedStats();
        }

        GUILayout.EndVertical();
    }

    public void SaveWindowedStats()
    {
        string jsonStats = JsonUtility.ToJson(statsMin);

        StreamWriter writer = new StreamWriter("Assets/Resources/PNJson/" + type + "WindowedStatsJson.JSON");
        writer.Write(jsonStats);
        writer.Close();

        Debug.Log(jsonStats);
    }
}
