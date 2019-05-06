using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBonus : ItemPickup {

    private int bonus = 40;
    Renderer rend;
    MoveAndRotate m;

    void Start()
    {
        rend = GetComponent<Renderer>();
        m = GetComponent<MoveAndRotate>();
        gameObject.name = "XpBonus";
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject != null && this.transform != null && this.enabled)
        {
            //m.StartMovement();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    protected override void PickUPAction()
    {
        if (captureRange && this.enabled==true && rend.enabled==true)
        {
            GameManager.Instance.CurrentPlayer.AddXp(bonus);
            this.enabled = false;
            rend.enabled = false;
        }
    }
}
