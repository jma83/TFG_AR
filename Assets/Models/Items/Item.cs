using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : InventoryEntity{


    protected int type = -1;    // 0 == health ; 1 == bighealth ; 2 == extend ; 3 == xpmultiplier
    protected int rand=0;
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
                RestoreAction();
                active = false;

            }
            Debug.Log(targetTime);
        }
    }

    public virtual void Use()
    {
        //Use item
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
        type = t;
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
        return type;
    }

    
}
