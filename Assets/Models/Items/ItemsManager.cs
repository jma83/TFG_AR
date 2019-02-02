using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : Singleton<ItemsManager>
{

    public List<Item> items;
    [SerializeField] GameObject activeObjects;
    private List<Image> imgs;
    private int maxSizeActive;

    // Use this for initialization
    void Start () {
        items = new List<Item>();
        
        imgs = new List<Image>();
        for (int i = 0; i < maxSizeActive; i++)
        {
            imgs.Add(activeObjects.GetComponentsInChildren<Image>()[i]);

        }
        maxSizeActive = 3;
    }
	
	// Update is called once per frame
	void Update () {
        if (items.Count > 0)
        {
            activeObjects.SetActive(true);
            

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetActive())
                {
                    items[i].Update();

                }
                else
                {
                    deleteImages(items[i].icon);
                    items.Remove(items[i]);
                }

            }
        }
        else
        {
            activeObjects.SetActive(false);
        }
    }
    
    
    public void AddItem(Item item)
    {
        
        for (int i = 0; i < maxSizeActive; i++)
        {

            if (activeObjects.GetComponentsInChildren<Image>()[i].sprite != null)
            {
                if (activeObjects.GetComponentsInChildren<Image>()[i].sprite == item.icon)
                {
                    items.Remove(item);
                    items.Add(item);
                    break;
                }
            }
            else
            {
                activeObjects.GetComponentsInChildren<Image>()[i].sprite = item.icon;
                activeObjects.GetComponentsInChildren<Image>()[i].enabled = true;
                items.Add(item);
                break;
            }
        }
    }
    private void deleteImages(Sprite spr)
    {

        for (int i = 0; i < maxSizeActive; i++)
        {
            if (activeObjects.GetComponentsInChildren<Image>().Length>i)
            if (activeObjects.GetComponentsInChildren<Image>()[i] == spr)
            {
                activeObjects.GetComponentsInChildren<Image>()[i].sprite = null;
                activeObjects.GetComponentsInChildren<Image>()[i].enabled = false;
                break;
            }
        }
    }
}


