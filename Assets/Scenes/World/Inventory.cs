using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    [SerializeField] private GameObject slotContainer;

    private bool inventoyEnabled;

    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;

    

    public void Start()
    {
        allSlots = 24;
        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotContainer.transform.GetChild(i).gameObject;
        }
    }

}
