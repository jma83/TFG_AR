using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    private bool inventoyEnabled;

    private int allSlots;
    private int enabledSlots;
    private InventorySlot[] slots;
    private Inventory inv;
    private int checkSwitch;



    public void Start()
    {
        inv = Inventory.Instance;
        inv.onItemChangedCallback += UpdateUI;
        checkSwitch = 0;
        slots = transform.GetComponentsInChildren<InventorySlot>();
    }

    public void SwitchUI(int b)    //b == 0 || b== 1 -> equipment ;; b == 2 -> item ;; 
    {


        if (checkSwitch != b || b==0)
        {
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
            if (i< inv.getItems().Count) { 
                slots[i].AddItem(inv.getItems()[i]);
            }else{
                slots[i].ClearSlot();
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
    public int getCheckSwitch()
    {
        return checkSwitch;
    }
}
