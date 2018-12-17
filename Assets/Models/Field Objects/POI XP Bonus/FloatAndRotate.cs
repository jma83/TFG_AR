using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAndRotate : MonoBehaviour {

    [SerializeField] private float rotateSpeed = 50f;
    [SerializeField] private float floatAmplitude = 1f;
    [SerializeField] private float floatFrequency = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (gameObject !=null && transform != null)
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
            Vector3 tempPos = startPos;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;
            transform.position = tempPos;
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
