using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/HealthItem")]

public class HealthItem : Item {

    [SerializeField] private int randValue;
	// Use this for initialization
	public  void Start () {
        name = "HealthItem";
    }

    public override void SetRand()
    {
        rand = randValue;//Random.Range(5, 30);
    }

    public override void Use()
    {       
        GameManager.Instance.CurrentPlayer.addHp(rand);
            
    }
}
