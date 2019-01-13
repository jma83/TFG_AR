using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEntity : MonoBehaviour {

    protected bool captureRange;
    protected float targetTime;

    // Use this for initialization
    void Start () {
        captureRange = false;
        targetTime = 0;
    }

    // Update is called once per frame
    void Update () {
        if (targetTime > 0)
            targetTime -= Time.deltaTime;
        if (targetTime <= 0 && captureRange)
        {
            targetTime = 0;
            captureRange = false;
        }
    }

    public void setCaptureRange(bool value)
    {
        captureRange = value;
        targetTime = 2.0f;
    }
}
