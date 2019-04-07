using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/XPMultiplierItem")]

public class XPMultiplierItem : Item {

    int multiplier;

    // Use this for initialization
    public override void Start () {
        name = "XPMultiplierItem";
        icon = Resources.Load<Sprite>("Items/double_xp2");

        defaultTime = 7;
        multiplier = 2;
        type = (ARGameConstants.TypeObj)3;
    }
    

    public override void RestoreAction()
    {
        GameManager.Instance.CurrentPlayer.SetXpMultiplier(1);
    }

    public override void Use()
    {
        if (ItemsManager.Instance.AddItem(this))
        {

            GameManager.Instance.CurrentPlayer.SetXpMultiplier(multiplier);

            targetTime = defaultTime;
            active = true;
        }
    }

}
