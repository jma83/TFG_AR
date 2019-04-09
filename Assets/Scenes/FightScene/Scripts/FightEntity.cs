using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEntity : MonoBehaviour {
    protected Weapon weapon;
    protected int hp;
    protected Vector3 forceVector;
    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update () {
        
    }

    public void Attack()
    {
        forceVector = (Vector3.zero - gameObject.transform.position).normalized;
        if (gameObject.tag == "Player") forceVector = GameObject.FindGameObjectsWithTag("camera")[0].transform.forward;

        if (weapon.CreateBullet() != null)
        {
            //Debug.Log("tag del creador:" + gameObject.tag);

            weapon.Shoot(forceVector);
            
        }
        
    }

    protected void Defend()
    {

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
    
}
