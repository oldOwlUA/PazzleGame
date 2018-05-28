using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using CHe;

public class UIManagerLang : SingletonObj<UIManagerLang>
{

    private string dataLangPath = "Resources/Lang.json";

    public DataLang DataObj;

    public List<TranslateObj> translateList;


    private bool LoadData()
    {

        string filePath = dataLangPath;

        TextAsset aset = Resources.Load("Lang") as TextAsset;
        DataObj = JsonUtility.FromJson<DataLang>(aset.text);
        return true;
    }

    private void SaveData()
    {
        string dataAsJson = JsonUtility.ToJson(DataObj);

        Debug.Log(dataAsJson);
        Debug.Log(DataObj);
        string filePath = dataLangPath;
        File.WriteAllText(filePath, dataAsJson);
        Debug.Log(filePath);
    }


    private void Awake()
    {
        LoadData();
    }
    public void ent()
    {

        if (LoadData())
            translateList[0].textObj.text = DataObj.Texts[0].en;
        else
            translateList[0].textObj.text = "Не удалось загрузить";
    }


}
[System.Serializable]
public class TranslateObj
{
    public int ind;
    public Text textObj;
}