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
        //goName = EditorGUILayout.TextField(goName);
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
            //foreach (GameObject go1 in secondGO)
            //{
            //    Debug.Log(go1.name);
            //}
            datas = JsonMapper.ToObject(jsonFile.text);
            //Debug.Log(datas["strMin"]);
            Debug.Log(datas["stats"]["str"]);
        }

        if (GUILayout.Button("Create"))
        {
            //You should never, NEVER, judge this code okey ? Unity forced my hand
            if (EditorApplication.isPlaying)
            {
                localPath = "Assets/Prefab/PNJEditor/";
                string tempName = localPath/* + goName +".prefab"*/;


                //GameObject tempGo;
                //GameObject emptyGo = PrefabUtility.CreatePrefab("Assets/Prefab/PNJEditor/EmptyGO.prefab", PNJGenerator.instance.vide);

                //il se passe des choses par ici
                //tempName = localPath + "name0" + ".prefab";
                ////tempGo = PrefabUtility.CreatePrefab(tempName, Selection.gameObjects[i]);
                //GameObject tempgovide = Instantiate(PNJGenerator.instance.vide);
                //GameObject testerinorino = Instantiate(PNJGenerator.instance.chest1);
                //testerinorino.transform.SetParent(tempgovide.transform);
                //tempgovide.AddComponent<PNJ>();
                //tempGo = PrefabUtility.CreatePrefab(tempName, tempgovide);
                //Destroy(tempgovide);


                //tempGo.transform.SetParent(tempEmptygo.transform);
                //tempGo.name = goName + ".prefab";
                datas = JsonMapper.ToObject(jsonFile.text);

                if (jsonFile.name.Contains("Static"))
                {
                    CreateStaticPrefab(datas, localPath);
                }
                else if (jsonFile.name.Contains("Windowed"))
                {
                    CreateWindowedPrefab(datas, localPath);
                }
            }
            else
            {
                Debug.LogError("You must be in play mode to do that.");
            }            
        }

        GUILayout.EndVertical();
    }

    void CreateStaticPrefab(JsonData data, string filePath)
    {        
        GameObject hat;
        GameObject chest;
        GameObject arms;
        bool offSetOK = false;
        
        PNJ.Stats stats = new PNJ.Stats();

        stats.str = (int)data["stats"]["str"];
        stats.dex = (int)data["stats"]["dex"];
        stats.intel = (int)data["stats"]["intel"];
        stats.name = (string)data["stats"]["name"];

        //stats.name = PNJ.RandomizeName(5);

        //GameObject emptyGo = PrefabUtility.CreatePrefab("Assets/Prefab/PNJEditor/EmptyGO.prefab", PNJGenerator.instance.vide);
        GameObject emptyGo = Instantiate(PNJGenerator.instance.vide);
        emptyGo.transform.name = stats.name;
        emptyGo.AddComponent<PNJ>();
        emptyGo.GetComponent<PNJ>().AccStats = stats;
        //str
        if (stats.str > (int)((PNJGenerator.instance.statsGiver.strMax * 66) / 100))
        {
            //chest = PrefabUtility.CreatePrefab(filePath + "")

            chest = Instantiate(PNJGenerator.instance.chest1);
            offSetOK = true;
        }
        else if (stats.str > (int)((PNJGenerator.instance.statsGiver.strMax * 33) / 100))
        {
            chest = Instantiate(PNJGenerator.instance.chest2);
        }
        else
        {
            chest = Instantiate(PNJGenerator.instance.chest3);
        }

        //intel
        if (stats.intel > (int)((PNJGenerator.instance.statsGiver.intelMax * 66) / 100))
        {
            hat = Instantiate(PNJGenerator.instance.head1);
        }
        else if (stats.intel > (int)((PNJGenerator.instance.statsGiver.intelMax * 33) / 100))
        {

            hat = Instantiate(PNJGenerator.instance.head2);

            if (offSetOK)
            {
                hat.transform.position += new Vector3(0.0f, 0.25f, 0.0f);
            }
        }
        else
        {
            hat = new GameObject();
            Debug.LogError("Y'a un blem ici, clique mwa dessus ! Jsuis pas censé rentré là dedans, jle fais pour intellicense");
        }

        //dex
        if (stats.dex > (int)((PNJGenerator.instance.statsGiver.dexMax * 66) / 100))
        {
            arms = Instantiate(PNJGenerator.instance.arms1);
        }
        else if (stats.dex > (int)((PNJGenerator.instance.statsGiver.dexMax * 33) / 100))
        {
            arms = Instantiate(PNJGenerator.instance.arms2);
        }
        else
        {
            arms = Instantiate(PNJGenerator.instance.arms3);
        }

        chest.transform.SetParent(emptyGo.transform);
        hat.transform.SetParent(emptyGo.transform);
        arms.transform.SetParent(emptyGo.transform);
        filePath += emptyGo.name + ".prefab";
        PrefabUtility.CreatePrefab(filePath, emptyGo);
        Destroy(emptyGo);
    }
    //Faut finir ca
    void CreateWindowedPrefab(JsonData data, string filePath)
    {
        GameObject hat;
        GameObject chest;
        GameObject arms;
        bool offSetOK = false;

        PNJGenerator.StatsMinMax statsMinMax = new PNJGenerator.StatsMinMax();
        PNJ.Stats stats = new PNJ.Stats();

        statsMinMax.strMax = (int)data["strMax"];
        statsMinMax.strMin = (int)data["strMin"];
        statsMinMax.dexMax = (int)data["dexMax"];
        statsMinMax.dexMin = (int)data["dexMin"];
        statsMinMax.intelMax = (int)data["intelMax"];
        statsMinMax.intelMin = (int)data["intelMin"];
        statsMinMax.name = (string)data["name"];

        stats.str = Random.Range(statsMinMax.strMin +1, statsMinMax.strMax +1);
        stats.dex = Random.Range(statsMinMax.dexMin +1, statsMinMax.dexMax +1);
        stats.intel = Random.Range(statsMinMax.intelMin +1, statsMinMax.intelMax +1);
        stats.name = PNJ.RandomizeName(5);
        
        GameObject emptyGo = Instantiate(PNJGenerator.instance.vide);
        emptyGo.transform.name = stats.name;
        emptyGo.AddComponent<PNJ>();
        emptyGo.GetComponent<PNJ>().AccStats = stats;
        emptyGo.GetComponent<PNJ>().hasBeenCreated = true;
        //str
        if (stats.str > ((statsMinMax.strMax * 66) / 100))
        {
            //chest = PrefabUtility.CreatePrefab(filePath + "")

            chest = Instantiate(PNJGenerator.instance.chest1);
            offSetOK = true;
        }
        else if (stats.str > ((statsMinMax.strMax * 33) / 100))
        {
            chest = Instantiate(PNJGenerator.instance.chest2);
        }
        else
        {
            chest = Instantiate(PNJGenerator.instance.chest3);
        }

        //intel
        if (stats.intel > ((statsMinMax.intelMax * 66) / 100))
        {
            hat = Instantiate(PNJGenerator.instance.head1);
        }
        else if (stats.intel > ((statsMinMax.intelMax * 33) / 100))
        {

            hat = Instantiate(PNJGenerator.instance.head2);

            if (offSetOK)
            {
                hat.transform.position += new Vector3(0.0f, 0.25f, 0.0f);
            }
        }
        else
        {
            hat = new GameObject();
            Debug.LogError("Y'a un blem ici, clique mwa dessus ! Jsuis pas censé rentré là dedans, jle fais pour intellicense");
        }

        //dex
        if (stats.dex > ((statsMinMax.dexMax * 66) / 100))
        {
            arms = Instantiate(PNJGenerator.instance.arms1);
        }
        else if (stats.dex > ((statsMinMax.dexMax * 33) / 100))
        {
            arms = Instantiate(PNJGenerator.instance.arms2);
        }
        else
        {
            arms = Instantiate(PNJGenerator.instance.arms3);
        }

        chest.transform.SetParent(emptyGo.transform);
        hat.transform.SetParent(emptyGo.transform);
        arms.transform.SetParent(emptyGo.transform);
        filePath += emptyGo.name + ".prefab";
        PrefabUtility.CreatePrefab(filePath, emptyGo);
        Destroy(emptyGo);
    }
}
