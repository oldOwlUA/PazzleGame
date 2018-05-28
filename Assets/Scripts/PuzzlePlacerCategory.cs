using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using CHe;

[Serializable]
public class CategoriesElement
{
    public int ind;
    public GameObject gm;
}


public class PuzzlePlacerCategory : SingletonObj<PuzzlePlacerCategory>
{
    [HideInInspector]
    public GameObject[] Points;
    public List<Sprite> backgrounds;
    public SpriteRenderer BackRend;

    public Color easyColor;
    public Color hardColor;

    public List<GameObject> Stickers;

    public List<CategoriesElement> Safary;
    public List<CategoriesElement> Home;
    public List<CategoriesElement> Forest;
    public List<CategoriesElement> Cars;
    public List<CategoriesElement> CAt1;//todo
    public List<CategoriesElement> CAt2;//todo

    public GameObject Prefab;
    public List<GameObject> PointsEnd;

    public Transform move;
    public Transform dontMove;
    public Transform PointBack;

    public Transform Center;

    public List<GameObject> InstPref;

    private void OnEnable() { }
    private void OnDisable() { }

    List<CategoriesElement> cat;

    public void SetBackG(int ind)
    {
        if (backgrounds[ind]!= null)
            BackRend.sprite = backgrounds[ind];
        else
            BackRend.sprite = backgrounds[0];
    }


    public void SetEasy()
    {
        for (int i = 1; i < InstPref.Count; i++)
        {
            if (InstPref[i] != null)
                InstPref[i].GetComponent<SpriteRenderer>().color = easyColor;
        }
    }

    public void SetHard()
    {
        for (int i = 1; i < InstPref.Count; i++)
        {
            if (InstPref[i] != null)
                InstPref[i].GetComponent<SpriteRenderer>().color = hardColor;
        }
    }

    public int Category = 0;
    public void SetPuzzle(int ind)
    {
        if (Stickers.Count == 0)
            return;
        else
        {

            switch (Category)
            {
                case 0:
                    {
                        cat = PuzzlePlacerCategory.Instance.Safary;
                    }
                    break;
                case 1:
                    {
                        cat = Home;
                    }
                    break;
                case 2:
                    {
                        cat = Forest;
                    }
                    break;
                case 3:
                    {
                        cat = Cars;
                    }
                    break;
                case 4:
                    {
                        cat = CAt1;
                    }
                    break;
                case 5:
                    {
                        cat = CAt2;
                    }
                    break;
                default:
                    cat = Safary;
                    break;
            }

            InstPref.Add(Instantiate(cat[ind].gm, Center.position, Quaternion.identity, Center));
            IntantObj();
        }

    }

    public void ResetPuzzle()
    {

        for (int i = 0; i < InstPref.Count; i++)
        {
            Destroy(InstPref[i]);
        }
        InstPref.RemoveRange(0, InstPref.Count);
       
        IntantObj();
        UIManagerScript._use.BtnСomplexity();
    }

    public void DestPuzzles()
    {
        for (int i = 0; i < InstPref.Count; i++)
        {
            Destroy(InstPref[i]);
        }
        InstPref.RemoveRange(0, InstPref.Count);
    }

    private void IntantObj()
    {
        int elemCount = Stickers[0].GetComponent<PuzzleElements>().list.Count;
        print(elemCount);
        for (int i = 0; i < elemCount; i++)
        {
            InstPref.Add(Instantiate(Prefab, dontMove.position, Quaternion.identity, dontMove));

            InstPref[i + 1].GetComponent<SpriteRenderer>().sprite = InstPref[0].GetComponent<PuzzleElements>().list[i].GetComponent<SpriteRenderer>().sprite;
            InstPref[i + 1].GetComponent<MovePuzzle>().minScale = InstPref[0].GetComponent<PuzzleElements>().listScale[i];
            InstPref[i + 1].AddComponent<PolygonCollider2D>();
            InstPref[i + 1].GetComponent<PolygonCollider2D>().isTrigger = true;
            InstPref[i + 1].GetComponent<MovePuzzle>().sp = PointBack.position;
            InstPref[i + 1].GetComponent<MovePuzzle>().moveParent = move;
            InstPref[i + 1].GetComponent<MovePuzzle>().dontMoveParent = dontMove;

            InstPref[i + 1].GetComponent<MovePuzzle>().id = i;
        }
    }

    public void End()
    {
        StartCoroutine("EndCoroutine", 5);
    }
    public void End(float f)
    {
        StartCoroutine("EndCoroutine", f);
    }
    IEnumerator EndCoroutine(float f)
    {
        yield return new WaitForSeconds(f);
        UIManagerScript._use.ChoiceAnimal.SetActive(true);
        DestPuzzles();
    }

}

