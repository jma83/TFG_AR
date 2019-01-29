using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MapEntity {

    [SerializeField] Item item;
    [SerializeField] Equipment equip;
    [SerializeField] bool insertInventory; 

    // Use this for initialization
    void Start () {
		
	}
	
    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");

        if (captureRange)
        {
            Debug.Log("Capture range TRUE");


            if (PickUp() && insertInventory)
                Destroy(gameObject);           
            else           
                PickUPAction();

        }
        else
        {
            Debug.Log("Capture range false");

        }
    }

    private bool PickUp()
    {
        Debug.Log("PickUp");

        if (item != null)
            return Inventory.Instance.AddItem(item);
        else if (equip != null)
            return Inventory.Instance.AddEquipment(equip);
        

        return false;
    }
    protected virtual void PickUPAction()
    {
        //Implemented in the children
    }

}
