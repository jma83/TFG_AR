using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton <Inventory> {

    private List<Item> items = new List<Item>();
    private int space = 26;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public bool modified = false;

    public bool AddItem(Item item)
    {
        if (space > items.Count)
        {
            items.Add(item);
            modified = true;

            if (onItemChangedCallback != null)
            {
                Debug.Log("Inventory = onItemChangedCallback");
                onItemChangedCallback.Invoke();
            }
            return true;
        }
        else
        {
            print("Your inventory is full!");
        }
        return false;
    }

    public void RemoveItem(Item item)
    {
        modified = true;
        items.Remove(item);
    }

    public List<Item> getItems()
    {
        return items;
    }

}
