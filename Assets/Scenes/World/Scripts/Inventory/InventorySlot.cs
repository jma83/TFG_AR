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
    [SerializeField] Image infoIcon;
    [SerializeField] GameObject hp;
    [SerializeField] Image hp_image;
    [SerializeField] Text hp_text;
    private Vector3 v1;
    private Vector3 v2;

    private void Start()
    {
        inv = Inventory.Instance;
        
    }

    private void Awake()
    {
        v1 = new Vector3(-11f, hp.gameObject.transform.localPosition.y, hp.gameObject.transform.localPosition.z);
        v2 = new Vector3(-25.6f, hp.gameObject.transform.localPosition.y, hp.gameObject.transform.localPosition.z);
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
        updateHP();
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
        hp.gameObject.transform.localPosition = v2;
        deleteIcon.enabled = true;
        deleteIcon.gameObject.SetActive(deleteIcon.enabled);
    }

    public void HideDeleteButton()
    {
        hp.gameObject.transform.localPosition = v1;
        deleteIcon.enabled = false;
        deleteIcon.gameObject.SetActive(deleteIcon.enabled);
    }

    public void ShowInfoButton()
    {
        hp.gameObject.transform.localPosition = v2;
        infoIcon.enabled = true;
        infoIcon.gameObject.SetActive(infoIcon.enabled);
    }

    public void HideInfoButton()
    {
        hp.gameObject.transform.localPosition = v1;
        infoIcon.enabled = false;
        infoIcon.gameObject.SetActive(infoIcon.enabled);
    }

    public void UseItem()
    {
        if (item != null && deleteIcon.enabled==false && infoIcon.enabled == false)
        {
            item.Use();
            inv.RemoveItem(item);
        }else if (equipment != null && deleteIcon.enabled == false && infoIcon.enabled == false)
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
        txt2.text = txt.text = "";
        txt2.enabled = txt.enabled = false;
        txt.gameObject.SetActive(txt.enabled);
        txt2.gameObject.SetActive(txt2.enabled);
        EquipmentSelectedColor(false);

    }

    public void updateHP()
    {
        Debug.Log(equipment.GetDurability());
        float f = equipment.GetDurability() / 100f;
        hp_text.text = equipment.GetDurability().ToString() + "%";

        hp_image.transform.localScale = new Vector3(f, 1, 1);
    }

    public void OnRemoveButton()
    {
        HideDeleteButton();
        //ClearSlot();       
        if (item!=null)
            inv.RemoveItem(item);

        if (equipment != null)
        {
            inv.RemoveEquip(equipment);
            equipment.DeleteObject();
        }
    }
}
