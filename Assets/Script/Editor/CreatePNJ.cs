using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using LitJson;

public class CreatePNJ : EditorWindow
{
    public TextAsset jsonFile;
    GameObject go;
    string localPath = "Assets/Prefab/PNJEditor/";
    string goName;
    JsonData datas;

    [MenuItem("Window/PNJ Editor/Create PNJ")]
    static public void ShowWindow()
    {
        CreatePNJ window = CreateInstance<CreatePNJ>();
        window.titleContent = new GUIContent("Create PNJ");

        window.Show();
    }

    private void OnGUI()
    {
        //SerializedObject serializedObject = new SerializedObject(this);
        //SerializedProperty serialized = serializedObject.FindProperty(".json");

        GUILayout.BeginVertical();
        //GUILayoutOption[] option = new GUILayoutOption[]
        //    {
        //        GUILayout.Height(10),
        //        GUILayout.Width(20),
        //    };
        
        EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.ObjectField(serialized, option);

        jsonFile = (TextAsset)EditorGUILayout.ObjectField(jsonFile, typeof(TextAsset), false);

        EditorGUILayout.EndHorizontal();

        //EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField(localPath);
        //if (GUILayout.Button("select path"))
        //{
        //    string tempPath = EditorUtility.OpenFolderPanel("PNJFolderPath", localPath, "");

        //    if (tempPath != "")
        //    {
        //        localPath = tempPath;
        //    }
        //}
        //EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        goName = EditorGUILayout.TextField(goName);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        if (jsonFile != null)
        {
            string[] splitusing = jsonFile.text.Split(' ');
            
            if (jsonFile.text.Contains("using"))
            {
                jsonFile = null;
                Debug.Log("Donne moi un .JSON :(");
            }
        }        

        if (GUILayout.Button("Debug"))
        {
            //GameObject[] secondGO = Selection.gameObjects;
            //Debug.Log(localPath);
            //Debug.Log(goName);
            //foreach (GameObject go1 in secondGO)
            //{
            //    Debug.Log(go1.name);
            //}
            datas = JsonMapper.ToObject(jsonFile.text);
            Debug.Log(datas["strMin"]);
        }

        if (GUILayout.Button("Create"))
        {
            localPath = "Assets/Prefab/PNJEditor/";
            string tempName = localPath + goName +".prefab";
            

            GameObject tempGo;            
            GameObject emptyGo = PrefabUtility.CreatePrefab("Assets/Prefab/PNJEditor/EmptyGO.prefab", PNJGenerator.instance.vide);
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                tempName = localPath + goName + i + ".prefab";
                tempGo = PrefabUtility.CreatePrefab(tempName, Selection.gameObjects[i]);
            }
            //tempGo.transform.SetParent(tempEmptygo.transform);
            //tempGo.name = goName + ".prefab";
        }

        GUILayout.EndVertical();
    }
}
