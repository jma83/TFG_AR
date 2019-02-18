using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TypeObj : int { Health, BigHealth, XPMultiplier, ExtendCapture };


public class ItemPickup : MapEntity {

    /*[SerializeField] */private Item item;
    [SerializeField] TypeObj type;
    [SerializeField] Equipment equip;
    [SerializeField] bool insertInventory; 

    // Use this for initialization
    void Start () {

        switch (type)
        {
            case TypeObj.Health:
                item = gameObject.GetComponent<HealthItem>();
                break;
            case TypeObj.BigHealth:
                item = gameObject.GetComponent<BigHealthItem>();
                break;
            case TypeObj.ExtendCapture:
                item = gameObject.GetComponent<ExtendCaptureItem>();
                break;
            case TypeObj.XPMultiplier:
                item = gameObject.GetComponent<XPMultiplierItem>();
                break;
        }
        if (item != null)
        {
            item.Start();
        }
        else
        {
            equip = gameObject.GetComponent<Equipment>();
            equip.Start();
        }


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
                    if (item != null)
                        item.DisableComponents();
                    else
                        equip.DisableComponents();
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
        if (item != null)
            item.SetRand();

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
