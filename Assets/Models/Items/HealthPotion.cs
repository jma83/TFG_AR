using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item {

    private int rand;

	// Use this for initialization
	void Start () {
        rand = Random.Range(5,30);

    }
	
	public override void Use()
    {
        GameManager.Instance.CurrentPlayer.addHp(rand);
            
    }
}
