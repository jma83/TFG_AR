using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/HealthItem")]

public class HealthItem : Item {

	// Use this for initialization
	public override void Start () {
        name = "HealthItem";
        //rand = Random.Range(5, 30);
        icon = Resources.Load<Sprite>("Items/small-potion");

    }

    public override void SetRand()
    {
        rand = Random.Range(5, 30);

    }

    public override void Use()
    {       
        GameManager.Instance.CurrentPlayer.addHp(rand);
        this.DeleteObject();
    }
}
