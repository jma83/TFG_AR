using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : Singleton<ItemsManager>
{
    private String equipmentPrefab = "Equipment/Weapon";


    public List<Item> items;
    [SerializeField] GameObject activeObjects;
    private List<Image> imgs;
    private int maxSizeActive;
    private float targetTime;
    private int id_item;
    private int id_equipment;

    // Use this for initialization
    void Start () {

        if (maxSizeActive != 3)
        {
            items = new List<Item>();
            id_item = -1;
            maxSizeActive = 3;
            Time.timeScale = 1.0f;
        }
        if (activeObjects == null) activeObjects = GameObject.Find("/GUI/ActiveItems");

        StartCoroutine(Wait(2f));

    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        CheckPickedItems();

    }

    private void CheckPickedItems()
    {
        List<Item> items = Inventory.Instance.getItems(); //FindObjectOfType
        List<Equipment> equips = Inventory.Instance.getEquipments();

        Item[] mapItems= FindObjectsOfType<Item>();
        Item it;
        Debug.Log("items.Count: " + mapItems.Length);
        for (int i = 0; i < items.Count; i++)
        {
            it = items[i];
            for (int j = 0; j < mapItems.Length; j++)
            {
                if (it.GetItemType() == mapItems[j].GetItemType() && mapItems[j]!=it && mapItems[j].transform.localPosition.z == it.transform.localPosition.z && mapItems[j].transform.localPosition.x == it.transform.localPosition.x)
                {
                    if (mapItems[j].GetComponent<MeshRenderer>() != null)
                    {
                        mapItems[j].GetComponent<MeshRenderer>().enabled = false;
                    }
                    else
                    {
                        mapItems[j].GetComponentsInChildren<MeshRenderer>()[0].enabled = false;
                        mapItems[j].GetComponentsInChildren<MeshRenderer>()[1].enabled = false;
                        mapItems[j].GetComponentsInChildren<MeshRenderer>()[2].enabled = false;
                    }
                    mapItems[j].GetComponent<BoxCollider>().enabled = false;
                    mapItems[j].GetComponent<ItemPickup>().SetPicked();
                    if (mapItems[j].GetComponentInChildren<ItemPickup>()!=null)
                        mapItems[j].GetComponentInChildren<ItemPickup>().SetPicked();
                    Debug.Log("FOUND items! j: " + j);
                }

            }
        }

        Equipment[] mapEquip = FindObjectsOfType<Equipment>();
        Equipment eq;
        Debug.Log("equip.Count: " + mapEquip.Length);
        for (int i = 0; i < equips.Count; i++)
        {
            eq = equips[i];
            for (int j = 0; j < mapEquip.Length; j++)
            {
                if (eq.GetEquipmentTypeNum() == mapEquip[j].GetEquipmentTypeNum() && mapEquip[j].GetComponent<ItemPickup>().enabled == true && mapEquip[j].transform.localPosition == eq.transform.localPosition)
                {
                    mapEquip[j].GetComponent<MeshRenderer>().enabled = false;
                    mapEquip[j].GetComponent<BoxCollider>().enabled = false;
                    Debug.Log("FOUND equip! j: " + j);
                }
            }
        }
    }


    // Update is called once per frame
    void Update () {

        if (activeObjects != null)
        {

            if (items.Count > 0)
            {
                activeObjects.SetActive(true);


                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i] != null)
                    {
                        if (items[i].GetActive())
                        {
                            //items[i].Update();
                            //Debug.Log("Items Activos: " + items.Count);

                        }
                        else
                        {
                            deleteImages(items[i].icon);
                            Item item_aux = items[i];
                            items.Remove(items[i]);
                            item_aux.DeleteObject();
                            if (items.Count == 0) targetTime = 1;
                        }
                    }

                }
            }
            else
            {
                if (targetTime > 0)
                {
                    targetTime -= Time.deltaTime;
                }
                else
                {
                    activeObjects.SetActive(false);
                }
            }
        }
    }

    public void SetItem(Item i, int pos)
    {
        if (i != null && pos >= 0 && pos < items.Count)
            items[pos] = i;
        else
            AddItem(i);
    }

    public bool AddItem(Item item)
    {
        bool b = false;
        if (maxSizeActive == 0) Start();
        for (int i = 0; i < maxSizeActive; i++)
        {

            if (activeObjects.GetComponentsInChildren<Image>()[i].sprite != null)
            {
                if (activeObjects.GetComponentsInChildren<Image>()[i].sprite == item.icon)
                {
                    items.Remove(item);
                    //item.DeleteObject();
                    items.Add(item);
                    b = true;
                    break;
                }
            }
            else
            {
                if (item.icon == null)
                    item.Start();
                
                activeObjects.GetComponentsInChildren<Image>()[i].sprite = item.icon;
                activeObjects.GetComponentsInChildren<Image>()[i].enabled = true;
                items.Add(item);
                b = true;
                break;
            }
        }
        Debug.Log("Tam: " + maxSizeActive);
        return b;
    }
    private void deleteImages(Sprite spr)
    {

        for (int i = 0; i < maxSizeActive; i++)
        {
            if (activeObjects.GetComponentsInChildren<Image>().Length > i)
            {
                if (activeObjects.GetComponentsInChildren<Image>()[i].sprite == spr)
                {
                    activeObjects.GetComponentsInChildren<Image>()[i].sprite = null;
                    activeObjects.GetComponentsInChildren<Image>()[i].enabled = false;
                    break;
                }
            }
        }
    }

    public int GetNewItemID()
    {
        id_item++;
        return id_item;
    }

    public int GetNewEquipID()
    {
        id_equipment++;
        return id_equipment;
    }

    public void SetLastItemID(int i)
    {
        id_item=i;
    }

    public void SetLastEquipID(int i)
    {
        id_equipment=i;
    }

    public List<Item> GetItems()
    {
        return items;
    }

    public int GetLastItemID()
    {
        return id_item;
    }

    public int GetLastEquipID()
    {
        return id_equipment;
    }

    public String GetEquipmentPrefab(){
        return equipmentPrefab;
    }
 

}


