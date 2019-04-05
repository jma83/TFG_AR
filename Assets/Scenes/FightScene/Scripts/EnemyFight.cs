using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StateAI : int { Idle = 0 , Check = 1 , Move = 2 , Attack = 3 , Defend = 4 };

public class EnemyFight : FightEntity
{

    private int numEnemies;
    private int maxHp;
    private bool checkAttacked;
    private Rigidbody rb;
    private Collider collide;
    private Vector3 lastPosition;
    private Vector3 lastVelocity;
    private Vector3 lastAngularVelocity;
    private StateAI state;
    private int xp;
    private float return_timer;


    // Use this for initialization
    void Start()
    {
        weapon = gameObject.GetComponent<Weapon>();
        collide = gameObject.GetComponent<Collider>();
        rb = gameObject.GetComponent<Rigidbody>();
        //rb.detectCollisions = false;
        numEnemies = GameObject.FindGameObjectsWithTag("enemy").Length;
        maxHp = 20;
        hp = maxHp;
        //StartCoroutine("Move");
        checkAttacked = false;
        xp = Random.Range(20, 40);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * 3f * Time.deltaTime);
    }


    /*
    void FixedUpdate()
    {
        lastPosition = transform.position;
        lastVelocity = rb.velocity;
        lastAngularVelocity = rb.angularVelocity;
    }
    */


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
        //decrease health
        substractHP(d);
        checkAttacked = true ;
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

    public StateAI GetStateAI()
    {
        return state;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "FightBox" && return_timer == 0)
        {
            Debug.Log("return_timer establecido!");
            return_timer = 1f;
        }

        if (col.gameObject.tag == "Player")
        {
            /*transform.position = lastPosition;
            rb.velocity = lastVelocity;
            rb.angularVelocity = lastAngularVelocity;
            Physics.IgnoreCollision(collide, col);*/
            col.gameObject.GetComponent<PlayerFight>().DealDamage(3);

            if (col.gameObject.GetComponent<PlayerFight>().GetHP() <= 0)
            {
                EnemyFightManager.Instance.GameOver();
            }
            
        }
    }

    public void SetReturnTimer(float f)
    {
        return_timer = f;
        Debug.Log("return_timer: " + return_timer);
    }

    public float GetReturnTimer()
    {
        return return_timer;
    }

    public int GetXP()
    {
        return xp;
    }
}