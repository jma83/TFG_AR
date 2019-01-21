using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class bulletScript : MonoBehaviour {

    private string path;
    private GameObject bullet;
    private Rigidbody rb;

    // Use this for initialization
    void Awake () {
        //bullet = new GameObject();


    }

    // Update is called once per frame
    void Update () {
		
	}

    public GameObject CreateBullet()
    {
        bullet = Instantiate(Resources.Load("FightScene/bullet", typeof(GameObject))) as GameObject;
        rb = bullet.GetComponent<Rigidbody>();
        if (GameObject.FindGameObjectsWithTag("camera").Length <= 0)
        {
            print("Error. No cameras found");
            return null;
        }
        
        bullet.transform.rotation = GameObject.FindGameObjectsWithTag("camera")[0].transform.rotation;
        bullet.transform.position = GameObject.FindGameObjectsWithTag("camera")[0].transform.position;


        return bullet;

    }
    public void Shoot()
    {
        rb.AddForce(GameObject.FindGameObjectsWithTag("camera")[0].transform.forward * 500f);

        Destroy(bullet, 3);
    }
}
