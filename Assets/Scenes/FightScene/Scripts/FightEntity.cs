using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEntity : MonoBehaviour {
    protected Weapon weapon;
    protected AudioSource audioSource;
    protected int hp; 
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        
    }

    protected void Attack()
    {
        
        if (weapon.CreateBullet() != null)
        {
            Debug.Log("tag del creador:" + gameObject.tag);

            weapon.Shoot();
            audioSource = this.GetComponent<AudioSource>();
            audioSource.Play();
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
