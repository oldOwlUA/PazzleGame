using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LogoStart : MonoBehaviour {



    ScreenFader screenFader; 
    IEnumerator Example()
    {
        screenFader = GetComponent<ScreenFader>();
        print("Befor");
        print("after");
        yield return new WaitForSeconds(2);
        screenFader.fadeState = ScreenFader.FadeState.In; 
        yield return new WaitForSeconds(5);
        print("after cur");
        SceneManager.LoadScene(0);

    }



    void Start()
    {
        StartCoroutine("Example");
    }
}
