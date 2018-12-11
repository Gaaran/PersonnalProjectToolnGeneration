using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;


public class CreatePNJStats : EditorWindow {

    [MenuItem("Window/PNJ Editor/Create PNJ Stats")]
    static public void ShowWindow()
    {
        CreatePNJStats window = CreateInstance<CreatePNJStats>();
        window.titleContent = new GUIContent("Create PNJ Stats");

        window.Show();
    }

    PNJ pnjToSave;

    public CreatePNJStats()
    {
        pnjToSave = new PNJ();
        tmpStats = new PNJ.Stats();
        type = PNJGenerator.Archetype.paysant;
    }


    PNJ.Stats tmpStats;
    PNJGenerator.Archetype type;


    private void OnGUI()
    {
        


        GUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("dex :");
        tmpStats.dex = EditorGUILayout.IntField(tmpStats.dex);       
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("intel :");
        tmpStats.intel = EditorGUILayout.IntField(tmpStats.intel);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("str :");
        tmpStats.str = EditorGUILayout.IntField(tmpStats.str);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("name :");
        tmpStats.name = EditorGUILayout.TextField(tmpStats.name);
        EditorGUILayout.EndHorizontal();



        type = (PNJGenerator.Archetype)EditorGUILayout.EnumPopup("archetype", type);

        if (GUILayout.Button("Save into jason"))
        {
            Debug.Log(pnjToSave);
            SaveStatsInPNJ();
        }

        GUILayout.EndVertical();
    }


    public void SaveStatsInPNJ()
    {
        //Debug.Log("AccStats : " + pnjToSave.AccStats); //NULL REF EXCEPTION ???
        //Debug.Log("tmpStats : " + tmpStats);
        pnjToSave.AccStats = tmpStats;

        string jsonPnj = JsonUtility.ToJson(pnjToSave);

        //writingFileJSON
        StreamWriter writer = new StreamWriter("Assets/Resources/PNJson/" + type +"StaticJson.JSON");
       
        writer.Write(jsonPnj);

        writer.Close();
        

        Debug.Log(jsonPnj);
    }
}
