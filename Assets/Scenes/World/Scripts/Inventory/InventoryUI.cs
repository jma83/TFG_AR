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
    private int checkSwitch;
    private bool checkSwitchDelete;

    public void Start()
    {
        inv = Inventory.Instance;
        inv.onItemChangedCallback += UpdateUI;
        checkSwitch = 0;
        checkSwitchDelete = false;
        slots = transform.GetComponentsInChildren<InventorySlot>();
    }

    public void SwitchUI(int b)    //b == 0 || b== 1 -> equipment ;; b == 2 -> item ;; 
    {

        turnOffDeleteImage();
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

    public void SwitchDeleteButtons()
    {
        checkSwitchDelete = !checkSwitchDelete;

        UpdateDeleteButtons();


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
    public int getCheckSwitch()
    {
        return checkSwitch;
    }
}
