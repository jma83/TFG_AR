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
        if (checkSwitch != b || b==0)
        {
            //SwitchDeleteButtons();
            UpdateDeleteButtons();
            checkSwitch = b;
            int size = 0;
            ClearSlots();
            if (b==2) size = inv.getItems().Count;
            else size = inv.getEquipment().Count; 

            for (int i = 0; i < slots.Length; i++)
            {
                if (i < size)
                {
                    if (b==2)
                        slots[i].AddItem(inv.getItems()[i]);
                    else
                        slots[i].AddEquipment(inv.getEquipment()[i]);

                }
            }
        }
    }
    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearText();
            if (i< inv.getItems().Count) { 
                slots[i].AddItem(inv.getItems()[i]);
            }else{
                slots[i].ClearSlot();
            }
        }
        inv.modified = false;
        UpdateDeleteButtons();

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
                
                if (checkSwitchDelete && i < inv.getEquipment().Count)
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
