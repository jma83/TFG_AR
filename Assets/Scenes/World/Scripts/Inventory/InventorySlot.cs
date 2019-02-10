using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {


    Item item;
    Equipment equipment;
    [SerializeField] Image icon;
    [SerializeField] Text txt;
    [SerializeField] Image deleteIcon;

    public void AddItem(Item i)
    {
        item = i;
        //item.SetRand();


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


        txt.text = "";
        txt.enabled = false;
        txt.gameObject.SetActive(txt.enabled);
        
        icon.sprite = null;
        icon.enabled = false;
        icon.gameObject.SetActive(icon.enabled);

    }

    public void ShowDeleteButton()
    {
        deleteIcon.enabled = true;
        deleteIcon.gameObject.SetActive(deleteIcon.enabled);
    }

    public void HideDeleteButton()
    {
        deleteIcon.enabled = false;
        deleteIcon.gameObject.SetActive(deleteIcon.enabled);
    }

    public void UseItem()
    {
        if (item != null && deleteIcon.enabled==false)
        {
            item.Use();
            Inventory.Instance.RemoveItem(item);
        }
    }

    public void ClearText()
    {
        txt.text = "";
        txt.enabled = false;
        txt.gameObject.SetActive(txt.enabled);
    }

    public void OnRemoveButton()
    {
        HideDeleteButton();
        //ClearSlot();       
        Inventory.Instance.RemoveItem(item);
    }
}
