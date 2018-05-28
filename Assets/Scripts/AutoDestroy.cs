using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {


    public float TimeToDestroy = 5;
	// Use this for initialization
	void Start () {
        Invoke("destroyFX", TimeToDestroy);
	}

    void destroyFX()
    {
        Destroy(gameObject);
    }

}
