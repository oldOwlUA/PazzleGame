using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHe;
public class PuzzlePlacer : SingletonObj<PuzzlePlacer>
{
   [HideInInspector]
    public GameObject[] Points;

    public Color easyColor;
    public Color hardColor;
  
    public List<Sprite> Stickers;

    public int[] numbers;


    private void OnEnable()
    {
        ResetPuzzle(0);
    }
    private void OnDisable()
    {
        DestPuzzles();
        UIManagerScript._use.HideLeftPan(0);        
    }


    public GameObject Prefab;
    public List<GameObject> PointsEnd;

    public Transform move;
    public Transform dontMove;
    public Transform PointBack;

    public List<GameObject> InstPref;
    
    List<CategoriesElement> cat;



    public void SetEasy()
    {
        for (int i = 0; i < InstPref.Count; i++)
        {
            InstPref[i].GetComponent<SpriteRenderer>().color = easyColor;
        }
    }

    public void SetHard()
    {
        for (int i = 0; i < InstPref.Count; i++)
        {
            InstPref[i].GetComponent<SpriteRenderer>().color = hardColor;
        }
    }


    private void Intant()
    {
        if (Stickers.Count == 0)
            return;
        for (int j = 0; j < Points.Length; j++)
        {
            InstPref.Add(Instantiate(Prefab, transform.position, Quaternion.identity, dontMove));
            InstPref[j].GetComponent<MovePuzzle>().sp = PointBack.position;        
            InstPref[j].GetComponent<MovePuzzle>().moveParent = move;
            InstPref[j].GetComponent<MovePuzzle>().dontMoveParent = dontMove;
            InstPref[j].GetComponent<MovePuzzle>().id = j;
            InstPref[j].GetComponent<SpriteRenderer>().sprite = Stickers[numbers[j]];
            InstPref[j].AddComponent<BoxCollider2D>();
            InstPref[j].GetComponent<BoxCollider2D>().isTrigger = true;
            InstPref[j].GetComponent<MovePuzzle>().minScale = 20;
            InstPref[j].GetComponent<MovePuzzle>().maxScale = 0.7f;
            InstPref[j].GetComponent<MovePuzzle>().Randoms = true;
            PointsEnd[j].GetComponent<SpriteRenderer>().sprite = Stickers[numbers[j]];
            PointsEnd[j].transform.position = new Vector3
                    (PointsEnd[j].transform.position.x + Random.Range(-1, 1),
                    PointsEnd[j].transform.position.y + Random.Range(-1.5f, 1.5f),
                    40);
            PointsEnd[j].GetComponent<PointsEnd>().id = j;           
        }
    }
    
    public List<GameObject> BangPreff;
    

    public Transform sp;





    void Start()
    {    
        Calc2();
        RecInd();
        Intant();
      
    }

    public void Bang(Vector3 v)
    {
        int tempId = Random.Range(0, (BangPreff.Count - 1));
        if (BangPreff.Count > 0)
            Instantiate(BangPreff[tempId], v, Quaternion.identity);
    }

    public void ResetPuzzle(float time)
    {
        
        StartCoroutine("Reset",time);
    }
    IEnumerator Reset(float f)
    {
        yield return new WaitForSeconds(f);
        for (int i = 0; i < InstPref.Count; i++)
        {
            Destroy(InstPref[i]);
        }
        InstPref.RemoveRange(0, InstPref.Count);
        Calc2();
        RecInd();
        Intant();
        UIManagerScript._use.BtnСomplexity();
        UIManagerScript._use.ShowLeftPan(0);
    }



    public void DestPuzzles()
    {
        for (int i = 0; i < InstPref.Count; i++)
        {
            Destroy(InstPref[i]);
        }
    }
    //калькуляция позиций пазлов
    void calc()
    {
        float h = ((Screen.height-(Screen.height *0.1f)) / Points.Length) / 2;

        float v = ((Screen.width - (Screen.width * 0.15f)) / 3) / 2;
        print(v);
        float f = 0;        

        #region
        for (int i = 0; i < Points.Length; i++)
        {

            if (i == 0)
            {
                f = h;
            }
            else
            {
                f += (Screen.height / Points.Length);
            }

            var hig = new Vector2(Screen.width / 20, f);
            var TMP = Camera.main.ScreenToWorldPoint(hig);
            Points[i].transform.localPosition = new Vector3
                (
                TMP.x,
                TMP.y,
                transform.position.z);
            print(Points[i].transform.localPosition);
            print(Points[i].transform.position);
            #endregion            
        }
    }

    //калькуляция позиций теней пазлов
    void Calc2()
    {
       // float h = (Screen.height / Points.Length) / 2;

        float v = ((Screen.width - (Screen.width * 0.15f)) / 3) / 2;
        print(v);

        float а = 0;

        for (int i = 0; i < Points.Length; i++)
        {


            Vector2 wig;

            if (i == 0)
            {
                а = v + (Screen.width * 0.15f);
                print(а);
            }
            else
            {
                if (i == 3)
                    а = v + (Screen.width * 0.15f);
                else
                    а += (Screen.width - (Screen.width * 0.15f)) / 3;
            }
            if (i < 3)
            {
                wig = new Vector2(а, (Screen.height / 2) / 2);
            }
            else
            {
                wig = new Vector2(а, ((Screen.height / 2) / 2) * 3);
            }
            print(wig);
            var TMP2 = Camera.main.ScreenToWorldPoint(wig);
            PointsEnd[i].SetActive(true);
          
            PointsEnd[i].transform.localPosition = new Vector3
            (
                TMP2.x,
                TMP2.y,
               0
            );

            PointsEnd[i].SetActive(false);
            PointsEnd[i].GetComponent<SpriteRenderer>().color = Color.black;
            PointsEnd[i].SetActive(true);
        }
    }


    void RecInd()
    {
        if (Stickers.Count == 0)
            return;
        numbers = new int[Points.Length];
        for (int i = 0; i < Points.Length; i++)
        {
            numbers[i] = Random.Range(0, Stickers.Count - 1);
        }
        for (int i = 0; i < Points.Length - 1; i++)
        {

            for (int y = Points.Length - 1; y > i; y--)
            {


                if (numbers[i] == numbers[y])
                {
                    numbers[i] = Random.Range(0, Stickers.Count - 1);
                }
            }
        }

    }

    int i = 0;
    public void Check()
    {
        i++;
        if (i < 6)
            return;
        else
        {           
            UIManagerScript._use.HideLeftPan(0);
            i = 0;
            ResetPuzzle(5);
        }
    }
}
