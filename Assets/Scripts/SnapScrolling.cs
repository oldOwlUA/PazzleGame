using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SnapScrolling : MonoBehaviour
{
    [Range(1, 50)]
    [Header("Controllers")]
    public int panCount = 6;
    [Range(0, 500)]
    public int panOffset;
    [Range(0f, 20f)]
    public float snapSpeed;
    [Range(0f, 10f)]
    public float scaleOffset;
    [Range(1f, 20f)]
    public float scaleSpeed;
    [Header("Other Objects")]
    public GameObject panPrefab;
    public ScrollRect scrollRect;

    private GameObject[] instPans;
    private Vector2[] pansPos;
    private Vector2[] pansScale;

    private RectTransform contentRect;
    private Vector2 contentVector;

    private int selectedPanID;
    private bool isScrolling;

    private void Start()
    {
        //contentRect = GetComponent<RectTransform>();
        //instPans = new GameObject[panCount];
        //pansPos = new Vector2[panCount];
        //pansScale = new Vector2[panCount];
        //for (int i = 0; i < panCount; i++)
        //{
        //    instPans[i] = Instantiate(panPrefab, transform, false);
        //    if (i == 0) continue;
        //    instPans[i].transform.localPosition = new Vector2(instPans[i - 1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset,
        //    instPans[i].transform.localPosition.y);
        //    instPans[i].GetComponent<Indexer>().ind = i;
        //    pansPos[i] = -instPans[i].transform.localPosition;
        //}
    }
    [SerializeField]
    List<GameObject> puzzles;

    public void CreateGalery(int ind )
    {
        puzzles = GetCollection(ind);
        contentRect = GetComponent<RectTransform>();
        instPans = new GameObject[panCount];
        pansPos = new Vector2[panCount];
        pansScale = new Vector2[panCount];
        PuzzlePlacerCategory.Instance.Category = ind;
        for (int i = 0; i < panCount; i++)
        {
            instPans[i] = Instantiate(panPrefab, transform, false);
            instPans[i].GetComponent<Image>().sprite = puzzles[i].GetComponent<PuzzleElements>().EndVar.GetComponent<SpriteRenderer>().sprite;
            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i - 1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset,
            instPans[i].transform.localPosition.y);
            instPans[i].GetComponent<Indexer>().ind = i;            
            pansPos[i] = -instPans[i].transform.localPosition;
        }
    }

    List<GameObject> GetCollection(int ind)
    {
        List<GameObject> temp = new List<GameObject>();
        List<CategoriesElement> cat;
        switch (ind)
        {
           
            case 0:
                {
                    cat = PuzzlePlacerCategory.Instance.Safary;
                }
                break;
            case 1:
                {
                    cat = PuzzlePlacerCategory.Instance.Home;
                }
                break;
            case 2:
                {
                    cat = PuzzlePlacerCategory.Instance.Forest;
                }
                break;
            case 3:
                {
                    cat = PuzzlePlacerCategory.Instance.Cars;
                }
                break;
            case 4:
                {
                    cat = PuzzlePlacerCategory.Instance.CAt1;
                }
                break;
            case 5:
                {
                    cat = PuzzlePlacerCategory.Instance.CAt2;
                }
                break;
            default:
                cat = PuzzlePlacerCategory.Instance.Safary;
                break;
        }

        foreach (var item in cat)
        {
            if (item != null)
                temp.Add(item.gm);          
        }
        return temp;
    }


    public float minScale  =.5f;
    public float maxScale  = 1.5f;



    private void FixedUpdate()
    {
        if (contentRect.anchoredPosition.x >= pansPos[0].x && !isScrolling || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x && !isScrolling)
            scrollRect.inertia = false;
        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
            }
            float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, minScale, maxScale);
            pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            instPans[i].transform.localScale = pansScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }

    public void Clear()
    {
        for (int i = 0; i < instPans.Length; i++)
        {
            Destroy(instPans[i]);            
        }
        //instPans[0]
    }
}