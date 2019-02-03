using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
abstract public class Item : ScriptableObject {

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    protected int rand=0;


    public virtual void Use()
    {
        //Use item
    }

    public virtual void Update()
    {

    }

    public virtual void RestoreAction()
    {
        //action after update
    }

    public virtual void SetRand()
    {

    }

    public virtual int GetRand()
    {
        return rand;

    }

    public virtual bool GetActive()
    {
        return false;
    }
}
