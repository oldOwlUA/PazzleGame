using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CHe;
public class UIManagerScript : MonoBehaviour
{
    public GameObject GameSceneRangom;
    public GameObject GameSceneCategory;
    public GameObject ChoiceLevel;
    public GameObject ChoiceCategory;
    public GameObject MainMenu;
    public GameObject pusles;
    public GameObject SettingParrent;
    public GameObject BackParrent;
    public GameObject BackParrentCan;
    public GameObject ChoiceAnimal;
    public GameObject GameCanvas;
    public GameObject PazzlePanel;
    public GameObject UpBtn;


    public int puslePlace;

    public static UIManagerScript _use;
    private void Start()
    {
        _use = this;
    }

    //IEnumerator Restart()
    //{            
    //    yield return new WaitForSeconds(3);
    //    print("roundEnd");
    //    pusles.GetComponent<PuzzlePlacer>().ResetPuzzle(0);
    //}

    //public void ChangeScene()
    //{
    //    StartCoroutine(Restart());
    //}

    public void RateClick()
    {
        Application.OpenURL("www.google.com");
    }

    public void NoADSClick()
    {
        Application.OpenURL("www.google.com");
    }

    public void ShowGameCategory()
    {
        //  GameSceneCategory.SetActive(true);
        ChoiceAnimal.SetActive(false);
        GameCanvas.SetActive(true);
        ShowLeftPan(1);
    }

    public void BtnStart(float time)
    {
        StartCoroutine(start(time));
    }

    IEnumerator start(float t)
    {
        yield return new WaitForSeconds(t);
        MainMenu.SetActive(false);
        ChoiceLevel.SetActive(true);
    }


    public void BtnBackToMain(float time)
    {
        StartCoroutine(back(time));
    }

    IEnumerator back(float t)
    {
        yield return new WaitForSeconds(t);
        MainMenu.SetActive(true);
        ChoiceLevel.SetActive(false);
    }

    public void BtnEndRound()
    {
        pusles.GetComponent<PuzzlePlacer>().DestPuzzles();
        MainMenu.SetActive(true);
        //GameScene.SetActive(false);
    }

    public void Close()
    {
        Application.Quit();
    }

    //public void Check()
    //{
    //    if (puslePlace > 5.1)
    //    {
    //        StartCoroutine(Restart());
    //        puslePlace = 0;
    //    }
    //}

    public void ShowSetting()
    {
        SettingParrent.GetComponent<Animator>().SetInteger("Show", 1);
    }

    public void HideSetting()
    {
        SettingParrent.GetComponent<Animator>().SetInteger("Show", 2);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ShowBack()
    {
        Invoke("ShowB", 1);
    }
    void ShowB()
    {
        BackParrent.GetComponent<Animator>().SetInteger("Show", 1);
    }


    public void HideBack()
    {
        BackParrent.GetComponent<Animator>().SetInteger("Show", 2);
        StartCoroutine("HidePan");
    }

    IEnumerator HidePan()
    {
        ShowLeftPan(0.5f);
        yield return new WaitForSeconds(1f);
        BackParrentCan.SetActive(false);
    }

    public void ShowLeftPan(float time)
    {
        Invoke("ShowLP", time);
    }

    void ShowLP()
    {
        PazzlePanel.GetComponent<Animator>().SetInteger("IsShow", 1);
        UpBtn.GetComponent<Animator>().SetInteger("IsShow", 1);
    }

    public void HideLeftPan(float time)
    {
        Invoke("HideLP", time);
    }
    void HideLP()
    {
        PazzlePanel.GetComponent<Animator>().SetInteger("IsShow", 0);
        UpBtn.GetComponent<Animator>().SetInteger("IsShow", 0);
    }


    public void BtnBackOk()
    {
        if (GameControll.Instance.isRandom)
        {
            GameSceneRangom.SetActive(false);
            ChoiceLevel.SetActive(true);
        }
        else
        {
            PuzzlePlacerCategory.Instance.End(0);
        }
        
        BackParrent.GetComponent<Animator>().SetInteger("Show", 2);

    }

    #region

    public GameObject EasyBtn;
    public GameObject HardBtn;

    public void BtnСomplexity()
    {
        HardBtn.SetActive(true);
        EasyBtn.SetActive(false);
        GameControll.Instance.SetEasy();

    }

    #endregion

}
