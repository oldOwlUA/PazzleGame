using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;



public class MovePuzzle : MonoBehaviour
{
  GameObject BangPref;

    public Transform moveParent;
   
    public Transform dontMoveParent;
    [HideInInspector]
    public Vector3 sp;
    [HideInInspector]
    public Vector3 mousPos;
    [HideInInspector]
    public Vector3 puzzPos;
    [HideInInspector]
    public Vector2 startPos;
    public float moveSpeed = float.MaxValue;

    public int id;

    public float minScale;
    public float maxScale;
    public float minScaleOnGame;

    

    GameObject DestObj;

    public bool collide = false;
    public bool end = false;
    public bool drag = false;
    public bool parrent = true;

    public bool Randoms = false;

    private void FixedUpdate()
    {
        if (!drag && !parrent && !end)
        {
            print((Vector3.Distance(transform.position, sp)));
            if (Vector3.Distance(transform.position, sp) > 1.1f)
            {
                transform.localPosition = new Vector3(
                    Mathf.Lerp(transform.localPosition.x, sp.x, 0.05f),
                    Mathf.Lerp(transform.localPosition.y, sp.y, 0.05f),
                    transform.localPosition.z);
                transform.localScale = new Vector3(
                    Mathf.Lerp(transform.localScale.x, minScaleOnGame, Time.deltaTime * 2),
                    Mathf.Lerp(transform.localScale.y, minScaleOnGame, Time.deltaTime * 2),
                   0);
            }
            else
            {
                transform.SetParent(dontMoveParent);
                transform.localScale = new Vector3(minScale, minScale, minScale);
                parrent = true;
            }

        }
        if (end)
        {
            minScale = minScaleOnGame;
            gameObject.GetComponent<Collider2D>().enabled = false;
            if (!Randoms)
                DestObj.GetComponent<SpriteRenderer>().enabled = true;
            else
            {
                DestObj.GetComponent<SpriteRenderer>().color = Color.white;
                DestObj.GetComponent<Animator>().Play("Anim",0);
            }
            GameControll.Instance.Check();
            end = false;
            enabled = false;
            Destroy(gameObject);
            Instantiate(BangPref, transform.position, Quaternion.identity);

        }

        if (parrent && transform.localScale.x < minScale)
            transform.localScale = new Vector3(
                   Mathf.Lerp(transform.localScale.x, minScale, Time.deltaTime * 2),
                   Mathf.Lerp(transform.localScale.y, minScale, Time.deltaTime * 2),
                   Mathf.Lerp(transform.localScale.z, minScale, Time.deltaTime * 2));


    }
    private void OnMouseUp()
    {

        if (collide)
        {
            drag = false;
            //transform.position = newStartPos;
            maxScale = 0.7f;
            end = true;
        }
        else
        {

            drag = false;
            return;
        }


    }

    public void OnMouseDrag()
    {
        drag = true;

        transform.SetParent(moveParent);
        parrent = false;
        mousPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -2);

        var TMP = Camera.main.ScreenToWorldPoint(mousPos);

        transform.localPosition = new Vector3(
                    Mathf.Lerp(transform.localPosition.x, TMP.x, Time.deltaTime * moveSpeed),
                    Mathf.Lerp(transform.localPosition.y, TMP.y, Time.deltaTime * moveSpeed),
                   transform.localPosition.z);

        transform.localScale = new Vector3(
               Mathf.Lerp(transform.localScale.x, maxScale, Time.deltaTime * 5),
               Mathf.Lerp(transform.localScale.y, maxScale, Time.deltaTime * 5),
              0);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (drag)
        {
            if (id == col.GetComponent<PointsEnd>().id)
            {
                DestObj = col.gameObject;
                //  newStartPos = col.transform.localPosition;
                collide = true;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collide = false;
    }

    private void Start()
    {
        // startPos = transform.position;
        BangPref = GameControll.Instance.Bang;
        parrent = true;
    }

}



