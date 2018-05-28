using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleElements : MonoBehaviour
{
    public List<GameObject> list;
    public List<float> listScale;
    public GameObject EndVar;
    public GameObject Shadow;
    public GameObject rootList;
    int i = 0;
    public void Check()
    {
        i++;
        if (i < 6)
            return;
        else
        {
            rootList.SetActive(false);
            Shadow.SetActive(false);
            EndVar.SetActive(true);
            PuzzlePlacerCategory.Instance.End();           
            UIManagerScript._use.HideLeftPan(0.1f);
        }
    }
}
