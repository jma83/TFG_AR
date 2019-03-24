using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerFight : FightEntity
{
    public Button fireButton;
    private int bonusAtack;
    private int bonusDefense;
    private int bonusSpeed;
    private Player ply;
    private bool hit;


    // Use this for initialization
    void Start () {
        weapon = gameObject.AddComponent(typeof(Weapon)) as Weapon;
        fireButton.onClick.AddListener(Attack);
        ply = GameManager.Instance.CurrentPlayer;
        hit = false;
    }
	
	// Update is called once per frame
	void Update () {
        
        
    }
    public override void DealDamage(int d)
    {
        //decrease health calling player
        ply.substractHp(d);
        hp=ply.Hp;
        //hud transition
        SetHit(true);

    }

    public void SetHit(bool b)
    {
        hit = b;
    }

    public bool GetHit()
    {
        return hit;
    }
}
