using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHe;

public class GameControll : SingletonObj<GameControll> {

    public GameObject Bang;
    public bool isRandom;

    public void Check()
    {
        if (isRandom)
             PuzzlePlacer.Instance.Check();
        else
            PuzzlePlacerCategory.Instance.InstPref[0].GetComponent<PuzzleElements>().Check();

    }

    public void setType(bool type)
    {
        isRandom = type;
    }

    public void SetEasy()
    {
        if (isRandom)
            PuzzlePlacer.Instance.SetEasy();
        else
            PuzzlePlacerCategory.Instance.SetEasy();
    }

    public void SetHard()
    {
        if (isRandom)
            PuzzlePlacer.Instance.SetHard();
        else
            PuzzlePlacerCategory.Instance.SetHard();
    }
	

}
