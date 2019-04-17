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
        description = "Item that allows to restore a small amount of durability for your equipment in use. \n\n" +
            "The durability represents the energy of the weapon. If it reaches 0%, it'll be destroyed. This item can prevent it. You have to equip that weapon in order to repair it.";
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
