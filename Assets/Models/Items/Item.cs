using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    protected int rand=0;
    protected bool active=false;

    public virtual void Use()
    {
        //Use item
    }

    public virtual void Update()
    {
        //Update item
    }

    public virtual void SetRand()
    {

    }

    public int GetRand()
    {
        return rand;
    }

    public bool GetActive()
    {
        return active;
    }
}
