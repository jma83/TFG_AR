using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPointCollider : MonoBehaviour {

    bool check;
	// Use this for initialization
	void Start () {
        check = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !check)
        {
            GetComponentInChildren<MeshRenderer>().enabled = false;
            PuzzleManager.Instance.Winner();
            check = true;
        }
    }
}
