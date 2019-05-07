using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class ItemPickup : MapEntity {

    private Item item;
    private Equipment equip;

    [SerializeField] ARGameConstants.TypeObj type; 
    [SerializeField] bool insertInventory; 

    // Use this for initialization
    void Start () {


        item = gameObject.GetComponent<Item>();
        /*switch (type)
        {
            case ARGameConstants.TypeObj.Health:
                item = gameObject.GetComponent<HealthItem>();
                break;
            case ARGameConstants.TypeObj.BigHealth:
                item = gameObject.GetComponent<BigHealthItem>();
                break;
            case ARGameConstants.TypeObj.ExtendCapture:
                item = gameObject.GetComponent<ExtendCaptureItem>();
                break;
            case ARGameConstants.TypeObj.XPMultiplier:
                item = gameObject.GetComponent<XPMultiplierItem>();
                break;
        }*/
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

    public override void Update2()
    {
        CalculatePlayerDistance();

    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {

            //Debug.Log("OnMouseDown");

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
        //Debug.Log("PickUp");

        if (item != null)
        {
            item.SetRand();
            return Inventory.Instance.AddItem(item,true);
        }
        else if (equip != null)
        {
            equip.SetRandomStats();
            return Inventory.Instance.AddEquipment(equip,true);
        }
        

        return false;
    }
    protected virtual void PickUPAction()
    {
        //Implemented in the children
    }

}
