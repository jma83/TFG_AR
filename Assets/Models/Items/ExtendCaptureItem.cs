using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/ExtendCaptureItem")]

public class ExtendCaptureItem : Item {


    // Use this for initialization
    public override void Start () {
        name = "ExtendCaptureItem";
        targetTime = 0;
        defaultTime = 50;
        icon = Resources.Load<Sprite>("Items/iconfinder_Spell_Scroll");
        type = 2;
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
        if (ItemsManager.Instance.AddItem(this))
        {
            GameManager.Instance.CurrentPlayer.SetMaxCaptureRange(1.0f);
            targetTime = defaultTime;
            active = true;
        }
    }

}
