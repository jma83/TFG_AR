using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : MonoBehaviour {

    protected int id;
    public Sprite icon = null;
    protected bool isDefaultItem = false;
    protected int rand=0;
    protected float defaultTime;
    protected float targetTime;
    protected bool active = false;
    private ItemsManager itemManager;

    private void Start()
    {
        itemManager = ItemsManager.Instance;
        id = itemManager.GetNewID();

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

    public virtual void SetRand()
    {
        //set rand depending the obj
    }

    public int GetRand()
    {
        return rand;

    }

    public bool GetActive()
    {
        return active;
    }

    public int GetID()
    {
        return id;
    }
}
