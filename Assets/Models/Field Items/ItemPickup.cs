using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MapEntity {

    [SerializeField] Item item;

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
        return Inventory.Instance.AddItem(item);
    }
    protected virtual void PickUpObj()
    {
        //Implemented in the children
    }

}
