﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StateAI : int { Idle = 0 , Check = 1 , Move = 2 , Attack = 3 , Defend = 4 };

public class EnemyFight : FightEntity
{

    [SerializeField] private GameObject capsule;
    [SerializeField] private int type;
    private int numEnemies;
    private bool checkAttacked;
    private Rigidbody rb;
    //private Collider collide;
    //private Vector3 lastPosition;
    private Vector3 lastVelocity;
    private Vector3 lastAngularVelocity;
    private StateAI state;
    private int xp;
    private bool return_flag;
    Vector3 v;



    // Use this for initialization
    void Start()
    {
        //collide = gameObject.GetComponent<Collider>();
        //rb = gameObject.GetComponent<Rigidbody>();
        //rb.detectCollisions = false;

        weapon = gameObject.GetComponent<Weapon>();
        numEnemies = GameObject.FindGameObjectsWithTag("enemy").Length;
        switch (type)
        {
            case 0:
                SetHP(20);
                xp = Random.Range(20, 60);

                break;
            case 1:
                SetHP(40);
                xp = Random.Range(50, 100);
                weapon.SetWeaponStats(100, 3, 1, 6, 16, 16, gameObject.tag);
                gameObject.GetComponent<MeshRenderer>().material = Resources.Load("FightScene/door_mtl1_diffcol", typeof(Material)) as Material;

                break;
            case 2:
                SetHP(60);
                xp = Random.Range(80, 200);
                weapon.SetWeaponStats(100, 3, 1, 8, 26, 26, gameObject.tag);

                break;
        }
        
        checkAttacked = false;
        state = new StateAI();
        AI = true;
    }


    public bool UpdateBodyAttack()
    {
        v = new Vector3(0, Random.Range(-1f, 5f), 0);
        transform.position = Vector3.Lerp(this.gameObject.transform.position, v, Time.deltaTime);
        if (Vector3.Distance(gameObject.transform.position, Vector3.zero) < 1.7)
        {
            // activeBodyAttack = false;
            gameObject.transform.rotation = Quaternion.Euler(0, (gameObject.transform.rotation.eulerAngles.y), 0);
            return false;
        }

        return true;
    }


    public void HeavyAttack()
    {
        weapon.SetHeavyStrike(true);
        Attack();
        weapon.SetHeavyStrike(false);
    }

    public void Heal(int h)
    {
        hp += h;
    }

    public int getNumEnemies()
    {
        return numEnemies; 
    }

    public void setNumEnemies(int i)
    {
        numEnemies = i;
    }
    public override void DealDamage(int d)
    {
        if (!GetDefend())
        {
            //decrease health
            substractHP(d);
            SetAttacked(true);
        }
    }
    public void addHP(int i)
    {
        if (i > maxHp)
        {
            hp = maxHp;
        }
        else
        {
            if (i > 0)
                hp += i;
        }
    }

    public void substractHP(int i)
    {
        if (i > hp)
        {
            hp = 0;
        }
        else
        {
            if (i > 0)
                hp -= i;
        }
    }

    /*IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.5f);
            transform.eulerAngles += new Vector3(0, 180f, 0);
        }
    }*/

    public void SetAttacked(bool b)
    {
        checkAttacked = b;
    }

    public bool GetAttacked()
    {
        return checkAttacked;
    }

    public void SetStateAI(StateAI s)
    {
        state = s;
    }

    public override void SetDefend(bool b)
    {
        defend = b;
        capsule.SetActive(b);
    }

    public void HideEnemy(bool b)
    {
        if (type == 1)
        {
            if (b)
                gameObject.GetComponent<MeshRenderer>().material = Resources.Load("FightScene/door_mtl1_diffcol_opacity", typeof(Material)) as Material;
            else
                gameObject.GetComponent<MeshRenderer>().material = Resources.Load("FightScene/door_mtl1_diffcol", typeof(Material)) as Material;
        }
        else if (type == 2)
        {
            if (b)
                gameObject.GetComponent<MeshRenderer>().material = Resources.Load("FightScene/BossMaterialOpacity", typeof(Material)) as Material;
            else
                gameObject.GetComponent<MeshRenderer>().material = Resources.Load("FightScene/BossMaterial", typeof(Material)) as Material;
        }
    }

    public StateAI GetStateAI()
    {
        return state;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "FightBox" && !return_flag)
        {
            Debug.Log("return_timer establecido!");
            SetReturnCollision(true);
        }

        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerFight>().DealDamage(3);

            if (col.gameObject.GetComponent<PlayerFight>().GetHP() <= 0)
            {
                EnemyFightManager.Instance.GameOver();
            }
            
        }
    }

    public void SetReturnCollision(bool f) //tiempo para volver (rotacion 180)
    {
        return_flag = f;
    }

    public void SetHP(int h)
    {
        maxHp = h;
        hp = maxHp;
    }

    public bool GetReturnCollision()
    {
        return return_flag;
    }

    public int GetXP()
    {
        return xp;
    }
}