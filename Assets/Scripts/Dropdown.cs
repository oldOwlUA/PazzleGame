using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class Dropdown : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler {

    public RectTransform conteiner;
    public bool isOpen;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOpen = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOpen = false;
    }

    // Use this for initialization
    void Start () {
        conteiner = transform.Find("Conteiner").GetComponent<RectTransform>();
        isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
        
            Vector3 scale = conteiner.localScale;
            scale.y = Mathf.Lerp(scale.y, isOpen ? 1: 0, Time.deltaTime*12);
            conteiner.localScale = scale;
        
	}
}
