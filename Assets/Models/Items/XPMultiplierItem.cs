using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/XPMultiplierItem")]

public class XPMultiplierItem : Item {

    int multiplier;

    // Use this for initialization
    public void Start () {
        name = "XPMultiplierItem";
        //icon = Resources.Load<Sprite>("Items/double_xp2");

        defaultTime = 7;
        multiplier = 2;
    }
    

    public override void RestoreAction()
    {
        GameManager.Instance.CurrentPlayer.SetXpMultiplier(1);
    }

    public override void Use()
    {
        GameManager.Instance.CurrentPlayer.SetXpMultiplier(multiplier);
        targetTime = defaultTime;
        active = true;
        ItemsManager.Instance.AddItem(this);
    }

}
