using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : FightEntity
{

    private int numEnemies;
    private int maxHp;
    private bool checkAttacked;
    private Rigidbody rb;
    private Collider collider;
    private Vector3 lastPosition;
    private Vector3 lastVelocity;
    private Vector3 lastAngularVelocity;
    // Use this for initialization
    void Start()
    {
        weapon = gameObject.GetComponent<Weapon>();
        collider = gameObject.GetComponent<Collider>();
        rb = gameObject.GetComponent<Rigidbody>();
        //rb.detectCollisions = false;
        numEnemies = GameObject.FindGameObjectsWithTag("enemy").Length;
        maxHp = 20;
        hp = maxHp;
        //StartCoroutine("Move");
        checkAttacked = false;
        
        Debug.Log("forceVector: " + forceVector);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * 3f * Time.deltaTime);
    }


    void FixedUpdate()
    {
        lastPosition = transform.position;
        lastVelocity = rb.velocity;
        lastAngularVelocity = rb.angularVelocity;
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

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            transform.position = lastPosition;
            rb.velocity = lastVelocity;
            rb.angularVelocity = lastAngularVelocity;
            Physics.IgnoreCollision(collider, col);
            col.gameObject.GetComponent<PlayerFight>().DealDamage(3);
        }

    }
}