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
        icon = Resources.Load<Sprite>("Items/Icons/iconfinder_Spell_Scroll");
        type = (ARGameConstants.TypeObj)2;
        description = "Item that allows to extend the range of the player, to reach certain items or objectives in the map. \n\n" +
            "The capture range is represented by the blue circle below the player. It will become bigger during a certain time (it will appear in the list of active items).";
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
