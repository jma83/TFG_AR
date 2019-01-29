using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/ExtendCaptureItem")]

public class ExtendCaptureItem : Item {

    private float defaultTime = 10f;
    private float targetTime;
    private bool extendRange;
	// Use this for initialization
	void Start () {
        name = "Extend Capture Range";
        targetTime = 0;
        extendRange = false;
    }

    // Update is called once per frame
    void Update() {
        if (extendRange)
        {
            if (targetTime > 0)
            {
                targetTime -= Time.deltaTime;
            }
            else
            {
                extendRange = false;
                GameManager.Instance.CurrentPlayer.SetMaxCaptureRange(0f);
            }
        }
        Debug.Log("targetTime: " + targetTime + " extendRange: "+ extendRange);


    }
    public override void Use()
    {
        GameManager.Instance.CurrentPlayer.SetMaxCaptureRange(1.0f);
        targetTime = defaultTime;
        extendRange = true;
    }


}
