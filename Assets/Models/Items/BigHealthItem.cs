using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/BigHealthItem")]

public class BigHealthItem : HealthItem {


    public override void RestoreAction()
    {

    }

    public override void SetRand()
    {
        rand = 50;//Random.Range(5, 30);
    }
}
