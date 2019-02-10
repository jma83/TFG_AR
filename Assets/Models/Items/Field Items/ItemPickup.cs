using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TypeObj : int { Health, BigHealth, XPMultiplier, ExtendCapture };


public class ItemPickup : MapEntity {

    [SerializeField] private Item item;
    [SerializeField] TypeObj type;
    [SerializeField] Equipment equip;
    [SerializeField] bool insertInventory; 

    // Use this for initialization
    void Start () {

        /*switch (type)
        {
            case TypeObj.Health:
                item = new HealthItem();
                break;
            case TypeObj.BigHealth:
                item = new BigHealthItem();
                break;
            case TypeObj.ExtendCapture:
                item = new ExtendCaptureItem();
                break;
            case TypeObj.XPMultiplier:
                item = new XPMultiplierItem();
                break;
        }*/

    }
	
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
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
    }

    private bool PickUp()
    {
        Debug.Log("PickUp");

        if (item != null)
        {
            return Inventory.Instance.AddItem(item);
        }
        else if (equip != null)
        {
            return Inventory.Instance.AddEquipment(equip);
        }
        

        return false;
    }
    protected virtual void PickUPAction()
    {
        //Implemented in the children
    }

}
