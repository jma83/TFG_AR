using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/ExtendCaptureItem")]

public class ExtendCaptureItem : Item {

    [SerializeField] private float defaultTime;
    private float targetTime;
    private bool active = false;

    // Use this for initialization
    public  void Start () {
        name = "Extend Capture Range";
        targetTime = 0;
        defaultTime = 15;
    }
     
    public override void Update()
    {
        if (active)
        {
            if (targetTime > 0)
            {
                targetTime -= Time.deltaTime;
            }
            else
            {
                RestoreAction();
                active = false;

            }
            //Debug.Log("Capture Range. Target: " + targetTime + "/"+ defaultTime);
        }
    }

    public override void SetRand()
    {
        rand = 0;
    }

    public override void RestoreAction()
    {
        GameManager.Instance.CurrentPlayer.SetMaxCaptureRange(-1);
    }

    public override void Use()
    {
        GameManager.Instance.CurrentPlayer.SetMaxCaptureRange(1.0f);
        targetTime = defaultTime;
        active = true;
        ItemsManager.Instance.AddItem(this);
    }

    public override bool GetActive()
    {
        return active;
    }
}
