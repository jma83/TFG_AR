using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {


    Item item;
    [SerializeField] Image icon;


    public void AddItem(Item i)
    {
        item = i;
        icon.sprite = item.icon;
        icon.enabled = true;
        icon.gameObject.SetActive(icon.enabled);
    }
	
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        icon.gameObject.SetActive(icon.enabled);

    }

    public void EnableDelete()
    {
        //object selected to be deleted, show delete icon before it
    }

    public void OnRemoveButton()
    {
        Inventory.Instance.RemoveItem(item);
    }
}
