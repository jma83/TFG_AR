using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidingSphere : MonoBehaviour {
    public GameObject plane;
    public Text text;
    public GameObject spawnPoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //text.text = "X:" + this.transform.position.x + "Y:" + this.transform.position.y + "Z:" + this.transform.position.z;
        if (this.transform.position.y<plane.transform.position.y-10)
        {
            ResetPos();
        }
    }
    public void ResetPos()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.transform.position = spawnPoint.transform.position;
    }
}
