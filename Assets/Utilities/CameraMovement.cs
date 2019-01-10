using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] private GameObject gm_obj;
    private Player player1;
    private Transform t;
    private bool parentPlayer =true;
    private Vector3 offset;
    private Vector3 defaultCameraPos;
    private Quaternion defaultCameraRot;
    
    

    // Use this for initialization
    void Start () {
        player1 = GameManager.Instance.CurrentPlayer;
        t = player1.gameObject.transform;
        offset = transform.position - player1.transform.position;
        gm_obj.transform.position = player1.transform.position;
        defaultCameraPos = transform.localPosition;
        defaultCameraRot = transform.localRotation;
    }

    // Update is called once per frame
    void LateUpdate () {

        if (parentPlayer == false)
        {
            gm_obj.transform.position = player1.transform.position + offset;
            transform.LookAt(player1.gameObject.transform);
        }
    }

    public void switchParent()
    {
        if (parentPlayer)
        {
            transform.SetParent(gm_obj.transform);

            parentPlayer = false;
        }
        else
        { 
            
            transform.SetParent(player1.gameObject.transform);
            transform.localRotation = defaultCameraRot;
            
            parentPlayer = true;
        }
        transform.localPosition = defaultCameraPos;
        transform.LookAt(player1.gameObject.transform);
    }
}
