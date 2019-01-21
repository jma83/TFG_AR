using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : ScriptableObject
{

    new public string name = "New Equipment";
    public Sprite icon = null;
    public bool isDefaultEquipment = false;
    public int type = 0;
}
