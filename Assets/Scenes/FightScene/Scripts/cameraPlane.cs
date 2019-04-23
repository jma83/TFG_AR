using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPlane : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (Screen.width>1920)
        {
            gameObject.transform.localScale = new Vector3(-1.94f, 1, -1);
        }

    }
	
}
