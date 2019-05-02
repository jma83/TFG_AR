using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : Singleton<ItemsManager>
{
    private String equipmentPrefab = "Equipment/Sword";
    private String healthPrefab = "Items/ManaPot";
    //private String bigHealtPrefab = "Items/LifePot";
    private String extendCapturePrefab = "Items/Shield";
    private String xpMultiplierPrefab = "Items/Key";

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
    public String GetHealthPrefab()
    {
        return healthPrefab;
    }
    public String GetBigHealtPrefab()
    {
        return xpMultiplierPrefab;
    }
    public String GetXpMultiplierPrefab()
    {
        return extendCapturePrefab;
    }
    public String GetExtendCapturePrefab()
    {
        return extendCapturePrefab;
    }

}

