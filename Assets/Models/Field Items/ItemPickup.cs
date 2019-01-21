using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MapEntity {

    [SerializeField] Item item;
    [SerializeField] Equipment equip;

    // Use this for initialization
    void Start () {
		
	}
	
    private void OnMouseDown()
    {
        if (captureRange)
        {          
            if (PickUp() && gameObject.name != "XpBonus")
                Destroy(gameObject);
        }
    }

    private bool PickUp()
    {
        Debug.Log("PickUp");
        PickUpObj();
        if (item != null)
            return Inventory.Instance.AddItem(item);
        else if (equip != null)
            return Inventory.Instance.AddEquipment(equip);

        return false;
    }
    protected virtual void PickUpObj()
    {
        //Implemented in the children
    }

}
