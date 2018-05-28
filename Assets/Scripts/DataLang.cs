using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataLang
{
    public List<DataObject> Texts = new List<DataObject>();
}

[System.Serializable]
public class DataObject
{
    public string Name;// имя обьекта

    public int id; // идентификатор для использования

    /* переводы ( при необходимости добавить своее)*/
    public string en;
    public string ru;
    public string ua;
    public string SP;

    public DataObject()
    {
        Name = "Add name";// имя обьекта

        id = 0; // идентификатор для использования

        /* переводы ( при необходимости добавить своее)*/
        en = "Add english";
        ru = "Добавьте русский";
        ua = "Додайте українську";
        SP = "sPAIN";
    }
}