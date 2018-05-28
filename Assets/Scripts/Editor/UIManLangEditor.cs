using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class UIManLangEditor : EditorWindow
{
    private string dataLangPath = "/Resources/Lang.json";

    public  DataLang DataObj;

    private void LoadData()
    {
        string filePath = Application.dataPath + dataLangPath;

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

            DataObj = JsonUtility.FromJson<DataLang>(dataAsJson);
            Debug.Log(filePath + " is not Exist");
        }
        

    }

    private void SaveData()
    {
        string dataAsJson = JsonUtility.ToJson(DataObj);

        Debug.Log(dataAsJson);
        Debug.Log(DataObj);
        string filePath = Application.dataPath + dataLangPath;
        File.WriteAllText(filePath, dataAsJson);
        Debug.Log(filePath);


    }

    [MenuItem("Window/GameLangData")]
    private static void Init()
    {
        UIManLangEditor window = (UIManLangEditor)GetWindow(typeof(UIManLangEditor));
        window.Show();
    }

    private void OnGUI()
    {
        if (DataObj != null)
        {
            SerializedObject so = new SerializedObject(this);
            SerializedProperty sp = so.FindProperty("DataObj");
            EditorGUILayout.PropertyField(sp, true);
            so.ApplyModifiedProperties();

            if (GUILayout.Button("SaveDate"))
            {
                SaveData();
            }
        }
        if (GUILayout.Button("LoadData"))
        {
            LoadData();
        }

    }
}

