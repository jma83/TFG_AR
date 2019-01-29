using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/HealthItem")]

public class HealthItem : Item {

	// Use this for initialization
	void Start () {
        name = "HealthItem";
        rand = Random.Range(5, 30);
    }
	
	public override void Use()
    {       
        GameManager.Instance.CurrentPlayer.addHp(rand);
            
    }
}
