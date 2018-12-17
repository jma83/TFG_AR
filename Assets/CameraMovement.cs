using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] private GameObject gm_obj;
    private Player player1;
    private Transform t;
    private float distance = 15f;
    private bool parentPlayer =true;
    private Vector3 offset;

    // Use this for initialization
    void Start () {
        player1 = GameManager.Instance.CurrentPlayer;
        t = player1.gameObject.transform;
        offset = transform.position - player1.transform.position;

    }

    // Update is called once per frame
    void LateUpdate () {
        
        if (parentPlayer==false)
            transform.position = player1.transform.position + offset;

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
            transform.rotation = player1.gameObject.transform.rotation;
            parentPlayer = true;
        }
        transform.LookAt(player1.gameObject.transform);
    }
}
