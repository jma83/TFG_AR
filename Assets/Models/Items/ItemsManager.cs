using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : Singleton<ItemsManager>
{

    public List<Item> items;

    // Use this for initialization
    void Start () {
        items = new List<Item>();
    }
	
	// Update is called once per frame
	void Update () {
        if (items.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetActive())
                {
                    items[i].Update();
                }
                else
                {
                    items.Remove(items[i]);
                }
            }
        }
    }

}
