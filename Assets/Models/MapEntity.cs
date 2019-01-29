using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEntity : MonoBehaviour {

    protected bool captureRange;
    protected float targetTime;
    protected bool alwaysActiveCapture;

    // Use this for initialization
    void Start () {
        captureRange = false;
        alwaysActiveCapture = false;
        targetTime = 0;
    }

    // Update is called once per frame
    void Update () {
        if (captureRange)
        {
            if (targetTime > 0)
                targetTime -= Time.deltaTime;
            if (targetTime <= 0)
            {
                targetTime = 0;
                if (alwaysActiveCapture == false)
                    captureRange = false;
            }
        }
        
    }

    public void SetCaptureRange(bool value)
    {
        captureRange = value;
        targetTime = 4.0f;
    }
    public void SetFixedCaptureRange(bool value)
    {
        alwaysActiveCapture = value;
        SetCaptureRange(value);
    }
}
