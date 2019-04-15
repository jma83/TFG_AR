using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEntity : MonoBehaviour {
    protected Weapon weapon;
    protected int maxHp;
    protected int hp;
    protected Vector3 forceVector;
    protected bool defend;
    protected bool AI;

    // Use this for initialization
    void Start () {
        defend = false;
        //weapon = gameObject.GetComponent<Weapon>();

    }

    // Update is called once per frame
    public void Update () {
        if (defend)
        {
            if (weapon.GetTargetDefendTime() <= 0)
                SetDefend(false);
        }
    }

    public void Attack()
    {
        forceVector = (Vector3.zero - gameObject.transform.position).normalized;
        if (gameObject.tag == "Player") forceVector = GameObject.FindGameObjectsWithTag("camera")[0].transform.forward;


        if (weapon != null)
        {
            if (weapon.CreateBullet())
                weapon.Shoot(forceVector);

        }

    }

    public void Defend()
    {
        if (weapon != null)
            if (weapon.ActivateShield(AI))
                SetDefend(true);
        
    }

    public virtual void HealDamage()
    {

    }

    public virtual void DealDamage(int d)
    {
    }

    public int GetHP()
    {
        return hp;
    }

    public int GetMaxHP()
    {
        return maxHp;
    }

    public virtual void SetDefend(bool b)
    {
        defend = b;

    }

    public bool GetDefend()
    {
        return defend;
    }

}
