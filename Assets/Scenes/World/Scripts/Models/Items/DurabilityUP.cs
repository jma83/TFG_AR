using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurabilityUP : Item {

    Equipment eq;

    // Use this for initialization
    public override void Start () {
        name = "DurabilityUP";
        type = (ARGameConstants.TypeObj)4;
        //rand = Random.Range(5, 30);
        icon = Resources.Load<Sprite>("Items/durability-equip");
    }

    public override void SetRand()
    {
        rand = Random.Range(5, 30);
    }

    public override void Use()
    {
        eq = GameManager.Instance.CurrentPlayer.GetComponent<Inventory>().GetCurrentEquipment();
        if (eq != null)
        {
            eq.SetDurability(eq.GetDurability() + rand);
            active = true;
            this.DeleteObject();
        }
        else
        {
            WindowAlert.Instance.CreateInfoWindow("You don't have any equipment in use. Please, select one to apply this item",true);
        }
    }
}
