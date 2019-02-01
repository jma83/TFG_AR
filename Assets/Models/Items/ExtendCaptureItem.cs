using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/ExtendCaptureItem")]

public class ExtendCaptureItem : Item {

    private float defaultTime = 10f;
    private float targetTime;

	// Use this for initialization
	void Start () {
        name = "Extend Capture Range";
        targetTime = 0;
    }

    // Update is called once per frame
    public override void Update() {
        if (active)
        {
            if (targetTime > 0)
            {
                targetTime -= Time.deltaTime;
            }
            else
            {
                GameManager.Instance.CurrentPlayer.SetMaxCaptureRange(0f);
                active = false;

            }
        }
        Debug.Log("targetTime: " + targetTime + " extendRange: "+ active);


    }

    public override void SetRand()
    {
        rand = 0;
    }

    public override void Use()
    {
        GameManager.Instance.CurrentPlayer.SetMaxCaptureRange(1.0f);
        targetTime = defaultTime;
        active = true;
        ItemsManager.Instance.items.Add(this);
    }


}
