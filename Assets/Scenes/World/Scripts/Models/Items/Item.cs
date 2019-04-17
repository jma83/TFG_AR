using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : InventoryEntity
{


    protected ARGameConstants.TypeObj type = (ARGameConstants.TypeObj)5;    // 0 == health ; 1 == bighealth ; 2 == extend ; 3 == xpmultiplier ; 4 == durabilityUp
    protected int rand = 0;
    public float timeStampAux;
    protected ItemsManager itemManager;

    public virtual void Start()
    {
        itemManager = ItemsManager.Instance;
        Time.timeScale = 1.0f;
        if (id == 0 || id == -1)
            id = itemManager.GetNewItemID();

    }

    public void Update()
    {
        if (active)
        {
            if (targetTime > 0)
            {
                targetTime -= Time.deltaTime;
            }
            else
            {
                if (timeStampAux <= 0)
                {
                    Debug.Log("restore action y activo=false " + targetTime + " id: " + id + " type: " + type);
                    RestoreAction();
                    active = false;
                }
                else
                {
                    targetTime = timeStampAux;
                    timeStampAux = 0;
                }

            }
            //Debug.Log(targetTime);
        }
    }

    public virtual void Use()
    {
        //Use item
    }
    public void SetTargetTime(float f)
    {
        targetTime = f;
    }
    public virtual void RestoreAction()
    {
        //action after update
    }

    public void SetRandNum(int r)
    {
        rand = r;
    }

    public void SetType(int t)
    {
        type = (ARGameConstants.TypeObj)t;
    }

    public virtual void SetRand()
    {
        //set rand depending the obj
    }

    public int GetRand()
    {
        return rand;

    }

    public int GetItemType()
    {
        return (int)type;
    }

    public string GetItemTypeName()
    {
        //health ; 1 == bighealth ; 2 == extend ; 3 == xpmultiplier ; 4 == durabilityUp
        if (type == ARGameConstants.TypeObj.Health)
        {
            return "Player health (small)";
        }
        else if (type == ARGameConstants.TypeObj.BigHealth)
        {
            return "Player health (big)";
        }
        else if (type == ARGameConstants.TypeObj.ExtendCapture)
        {
            return "Extend Capture Range";
        }
        else if (type == ARGameConstants.TypeObj.XPMultiplier)
        {
            return "XP Multiplier";
        }
        else if (type == ARGameConstants.TypeObj.DurabilityUP)
        {
            return "Equipment Durability";
        }
        return null;
    }
}