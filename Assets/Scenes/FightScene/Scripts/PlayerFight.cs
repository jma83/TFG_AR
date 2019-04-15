using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerFight : FightEntity
{
    //[SerializeField] private Button fireButton;
    //[SerializeField] private Button defendButton;
    private int bonusAtack;
    private int bonusDefense;
    private int bonusSpeed;
    private Player ply;
    private bool hit;
    private bool badVisibility;


    // Use this for initialization
    public void Start () {
        //fireButton.onClick.AddListener(Attack);
        //defendButton.onClick.AddListener(Defend);
        weapon = gameObject.GetComponent<Weapon>();

        ply = GameManager.Instance.CurrentPlayer;
        hit = false;
        AI = false;
    }
	
	// Update is called once per frame

    public override void DealDamage(int d)
    {
        //decrease health calling player
        if (!defend) { 
            ply.substractHp(d);
            hp = ply.Hp;
        }
        //hud transition
        SetHit(true);

    }

    public override void SetDefend(bool b)
    {
        defend = b;
    }

    public void SetHit(bool b)
    {
        hit = b;
    }

    public void SetBadVisibility(bool b)
    {
        if (!defend)
        badVisibility = b;
    }

    public bool GetBadVisibility()
    {
        return badVisibility;
    }

    public bool GetHit()
    {
        return hit;
    }

}
