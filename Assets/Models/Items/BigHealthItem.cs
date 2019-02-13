using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/BigHealthItem")]

public class BigHealthItem : HealthItem {

    public override void Start()
    {
       // icon = Resources.Load<Sprite>("Items/big-potion");
        //rand = Random.Range(40, 60);

    }
    public override void RestoreAction()
    {

    }

    public override void SetRand()
    {
        rand = 60; //rand = Random.Range(40, 60);

    }
}
