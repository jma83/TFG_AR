using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {


    Item item;
    Equipment equipment;
    Inventory inv;
    [SerializeField] Image icon;
    [SerializeField] Image powerIcon;
    [SerializeField] Text txt;
    [SerializeField] Text txt2;
    [SerializeField] Image deleteIcon;
    [SerializeField] GameObject hp;

    private void Start()
    {
        inv = Inventory.Instance;
    }

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
        powerIcon.sprite = Resources.Load<Sprite>(equipment.GetEquipmentQuality());
        powerIcon.enabled = true;
        powerIcon.gameObject.SetActive(powerIcon.enabled);

        txt2.text = equipment.GetTotalPower().ToString();
        txt2.enabled = true;
        txt2.gameObject.SetActive(txt2.enabled);
        hp.SetActive(true);
    }

    public void ClearSlot()
    {
        item = null;
        equipment = null;

        hp.SetActive(false);

        txt2.text = txt.text = "";
        txt2.enabled = txt.enabled = false;
        txt.gameObject.SetActive(txt.enabled);
        txt2.gameObject.SetActive(txt2.enabled);

        powerIcon.sprite=icon.sprite = null;
        powerIcon.enabled = icon.enabled = false;
        icon.gameObject.SetActive(icon.enabled);
        powerIcon.gameObject.SetActive(powerIcon.enabled);

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
            inv.RemoveItem(item);
        }else if (equipment != null && deleteIcon.enabled == false)
        {
            //equipment.Activate();
            Debug.Log("trying to equip: "+equipment.name);
            inv.SelectEquipment(equipment);

        }
    }

    public void EquipmentSelectedColor(bool b)
    {
        if (b)
            gameObject.GetComponentInChildren<Image>().color = new Color32(105, 194, 225, 200);   //selected
        else
            gameObject.GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 200);   //default


    }
    public void ClearText()
    {
        txt.text = "";
        txt.enabled = false;
        txt.gameObject.SetActive(txt.enabled);
        EquipmentSelectedColor(false);

    }

    public void OnRemoveButton()
    {
        HideDeleteButton();
        //ClearSlot();       
        inv.RemoveItem(item);
    }
}
