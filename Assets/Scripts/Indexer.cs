using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indexer : MonoBehaviour {

    public int ind;



    public void SetPz()
    {
        PuzzlePlacerCategory.Instance.SetPuzzle(ind);        
        UIManagerScript._use.ShowGameCategory();
    }
}
