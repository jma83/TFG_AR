using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class webCamScript : MonoBehaviour
{

    public GameObject webCameraPlane;
    //public Button fireButton;
    //private float targetTime = 0.0f;

    // Use this for initialization
    void Start()
    {
        
        if (Application.isMobilePlatform)
        {
            GameObject cameraParent = new GameObject("camParent");
            cameraParent.transform.position = this.transform.position;
            this.transform.parent = cameraParent.transform;
            cameraParent.transform.Rotate(Vector3.right, 90);
        }

        Input.gyro.enabled = true;
        
        //fireButton.onClick.AddListener(OnButtonDown);
            

        WebCamTexture webCameraTexture = new WebCamTexture();
        webCameraPlane.GetComponent<MeshRenderer>().material.mainTexture = webCameraTexture;
        webCameraTexture.Play();

    }


    /*void OnButtonDown()
    {        
        if (targetTime < 1)
        {
            GameObject bullet = Instantiate(Resources.Load("FightScene/bullet", typeof(GameObject))) as GameObject;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            bullet.transform.rotation = this.transform.rotation;
            bullet.transform.position = this.transform.position;
            rb.AddForce(this.transform.forward * 500f);
            Destroy(bullet, 3);

            GetComponent<AudioSource>().Play();
            targetTime = 2.0f;
        }

    }*/

    // Update is called once per frame
    void Update()
    {
        //if (targetTime >= 0)
        //targetTime -= Time.deltaTime;
        //print(targetTime);

        Quaternion cameraRotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);
        this.transform.localRotation = cameraRotation;

    }
}