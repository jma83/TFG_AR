using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/HealthItem")]

public class HealthItem : Item {

	// Use this for initialization
	public override void Start () {
        name = "HealthItem";
        type = (ARGameConstants.TypeObj)0;
        //rand = Random.Range(5, 30);
        icon = Resources.Load<Sprite>("Items/small-potion");
        description = "Item that can be used to restore a small amount of energy for your Character. \n\n" +
            "The energy is a main element during the game that allows you to fight and resolve puzzles, in order to level up";
    }

    public override void SetRand()
    {
        rand = Random.Range(5, 30);

    }

    public override void Use()
    {       
        GameManager.Instance.CurrentPlayer.addHp(rand);
        active = true;
        this.DeleteObject();
    }
}
