using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Badges : Singleton<Badges> {

    GameObject slotBox1;
    [SerializeField] GameObject BadgesUI;
    [SerializeField] Text noBadgesText;
    string[] text_array;
    int size= 0;
    //[SerializeField] GameObject Slot;

    // Use this for initialization
    void Start () {
        text_array = new string[50];
    }


    public void AddSlot(string str)
    {
        
        if (size < BadgesUI.GetComponentsInChildren<Canvas>().Length && CheckString(str))
        {
            slotBox1 = BadgesUI.GetComponentsInChildren<Canvas>()[size].gameObject;
            slotBox1.GetComponentInChildren<Image>().enabled = true;
            slotBox1.GetComponentInChildren<Text>().text = str;
            text_array[size] = str;
            size++;

            noBadgesText.gameObject.SetActive(false);
            if (size > 8)
                BadgesUI.GetComponent<RectTransform>().sizeDelta = new Vector2(BadgesUI.GetComponent<RectTransform>().sizeDelta.x, BadgesUI.GetComponent<RectTransform>().sizeDelta.y + 80);
            else
                BadgesUI.GetComponent<RectTransform>().sizeDelta = new Vector2(BadgesUI.GetComponent<RectTransform>().sizeDelta.x, BadgesUI.GetComponent<RectTransform>().sizeDelta.y + 20);
        }
        else
        {
            Debug.Log("adios: " + size);

        }

    }

    private bool CheckString(string str)
    {
        for (int i = 0; i < size; i++)
        {
            if (text_array[i] == str)
                return false;
        }
        return true;
    }

    public string[] GetTextArray()
    {
        return text_array;
    }

    public int GetSize()
    {
        return size;
    }
}
