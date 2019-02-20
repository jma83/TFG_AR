using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    private bool inventoyEnabled;

    private int allSlots;
    private int enabledSlots;
    private InventorySlot[] slots;
    private Inventory inv;
    [SerializeField] Toggle deleteImage;
    [SerializeField] Toggle infoImage;
    private int checkSwitch;
    private bool checkSwitchDelete;
    private bool checkSwitchInfo;

    public void Start()
    {
        inv = Inventory.Instance;
        inv.onItemChangedCallback += UpdateUI;
        checkSwitch = 0;
        checkSwitchInfo=checkSwitchDelete = false;
        slots = transform.GetComponentsInChildren<InventorySlot>();
    }

    public void SwitchUI(int b)    //b == 0 || b== 1 -> equipment ;; b == 2 -> item ;; 
    {

        turnOffDeleteImage();
        turnOffInfoImage();
        if (checkSwitch != b || b == 0)
        {
            checkSwitch = b;

            UpdateUI();


        }
    }
    public void UpdateUI()
    {
        ClearSlots();
        UpdateDeleteButtons();
        UpdateInfoButtons();


        int size = 0;
        if (checkSwitch == 2) {
            size = inv.getItems().Count;
            slots[0].EquipmentSelectedColor(false);
        }
        else
        {
            size = inv.getEquipments().Count;
        }
        Debug.Log("checkSwitch: " + checkSwitch + " size: " + size);

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < size)
            {
                if (checkSwitch == 2)
                {
                    slots[i].AddItem(inv.getItems()[i]);
                    

                }
                else
                {
                    slots[i].AddEquipment(inv.getEquipments()[i]);
                    if (i==0)
                    if (inv.getEquipments()[i] == inv.GetCurrentEquipment())
                    {
                        slots[i].EquipmentSelectedColor(true);
                    }
                    else
                    {
                        slots[i].EquipmentSelectedColor(false);
                    }
                }
            }
        }
        inv.modified = false;

    }

    public void ClearSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
        }
    }

    public void SwitchButtons(int i)
    {

        if (i == 0)
        {
            checkSwitchDelete = !checkSwitchDelete;

            UpdateDeleteButtons();
        }
        else
        {
            checkSwitchInfo = !checkSwitchInfo;

            UpdateInfoButtons();
        }

    }

    public void UpdateDeleteButtons()
    {
        if (checkSwitch == 2)
        {
            for (int i = 0; i < slots.Length; i++)
            {

                if (checkSwitchDelete && i < inv.getItems().Count)
                    slots[i].ShowDeleteButton();
                else
                    slots[i].HideDeleteButton();

                
            }
        }
        else
        {
            for (int i = 0; i < slots.Length; i++)
            {
                
                if (checkSwitchDelete && i < inv.getEquipments().Count)
                    slots[i].ShowDeleteButton();
                else
                    slots[i].HideDeleteButton();
                
            }
        }
    }

    public void turnOffDeleteImage()
    {
        deleteImage.isOn = false;
    }

    public void UpdateInfoButtons()
    {
        if (checkSwitch == 2)
        {
            for (int i = 0; i < slots.Length; i++)
            {

                if (checkSwitchInfo && i < inv.getItems().Count)
                    slots[i].ShowInfoButton();
                else
                    slots[i].HideInfoButton();


            }
        }
        else
        {
            for (int i = 0; i < slots.Length; i++)
            {

                if (checkSwitchInfo && i < inv.getEquipments().Count)
                    slots[i].ShowInfoButton();
                else
                    slots[i].HideInfoButton();

            }
        }
    }

    public void turnOffInfoImage()
    {
        infoImage.isOn = false;
    }
    public int getCheckSwitch()
    {
        return checkSwitch;
    }
}
