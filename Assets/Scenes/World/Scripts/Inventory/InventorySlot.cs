﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {


    Item item;
    Equipment equipment;
    [SerializeField] Image icon;
    [SerializeField] Text txt;

    public void AddItem(Item i)
    {
        item = i;
        icon.sprite = item.icon;
        icon.enabled = true;       
        icon.gameObject.SetActive(icon.enabled);
        if (txt != null)
        {
            if (item.GetRand() != 0)
            {
                txt.enabled = true;
                txt.text = item.GetRand().ToString() + " HP";
                txt.gameObject.SetActive(txt.enabled);
            }
        }
    }

    public void AddEquipment(Equipment i)
    {
        equipment = i;
        icon.sprite = equipment.icon;
        icon.enabled = true;
        icon.gameObject.SetActive(icon.enabled);
    }

    public void ClearSlot()
    {
        item = null;
        if (txt != null)
        {
            txt.text = null;
            txt.enabled = false;
            txt.gameObject.SetActive(txt.enabled);
        }
        icon.sprite = null;
        icon.enabled = false;
        icon.gameObject.SetActive(icon.enabled);

    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            Inventory.Instance.RemoveItem(item);
        }
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