using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : FightEntity
{

    private int numEnemies;
    private int maxHp;

    // Use this for initialization
    void Start()
    {
        numEnemies = GameObject.FindGameObjectsWithTag("Player").Length;
        maxHp = 20;
        hp = maxHp;
        StartCoroutine("Move");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 3f * Time.deltaTime);
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

    IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.5f);
            transform.eulerAngles += new Vector3(0, 180f, 0);
        }
    }
}