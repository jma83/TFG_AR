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



    public void Start()
    {
        inv = Inventory.Instance;
        inv.onItemChangedCallback += UpdateUI;
        slots = transform.GetComponentsInChildren<InventorySlot>();
        
               
    }

    public void Update()
    {
        if (inv.modified == true) UpdateUI();
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

}
