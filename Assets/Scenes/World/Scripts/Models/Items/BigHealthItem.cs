using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/BigHealthItem")]

public class BigHealthItem : HealthItem {

    public override void Start()
    {
        icon = Resources.Load<Sprite>("Items/big-potion");
        //rand = Random.Range(40, 60);
        type = (ARGameConstants.TypeObj)1;
        description = "Item that can be used to restore a great amount of energy for your Character. \n\n" +
            "The energy is a main factor during the game that allows you to fight and resolve puzzles, in order to level up";
    }
    public override void RestoreAction()
    {

    }

    public override void SetRand()
    {
        rand = Random.Range(40, 60);

    }
}
