﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapboxItemPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
