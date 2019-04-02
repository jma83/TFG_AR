using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    private EnemyFight ef;
    public float timer;
    private float defaultTime;
    private bool activeBoost;
    private bool activeBodyAttack;
    private float speed;

    private void Start()
    {
        ef = gameObject.GetComponent<EnemyFight>();
        defaultTime = 4f;
        speed = 3f;
        timer = 0;
        activeBoost = false;
        defaultTime = Random.Range(3f, 6f);
        timer = defaultTime;
    }

    private void Update()
    {
        if (timer > 0) timer = timer - Time.deltaTime;
        if (activeBodyAttack == true) {
            transform.position = Vector3.Lerp(this.gameObject.transform.position, Vector3.zero, Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position,Vector3.zero) < 1.5)
            {
                activeBodyAttack = false;
            }
        }
    }


    [Task]
    void CheckAttacked()
    {
        if (ef.GetAttacked())   //check if recently has been attacked
        {
            ef.SetAttacked(false);
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void InitMove()
    {
        if (timer <= 0)
        {
            defaultTime = Random.Range(3f, 6f);
            timer = defaultTime;
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
        

    }

    [Task]
    void Move()
    {
        if (timer > 0)
        {
            if (!activeBoost)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime); //Move at a certain distance from the objective.
            }
            else
            {
                transform.Translate(Vector3.forward * speed * 2 * Time.deltaTime);
                activeBoost = false;
            }
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

    }
    [Task]
    void Idle()
    {   
        StartCoroutine("Wait");       
        Task.current.Succeed();

    }

    [Task]
    void Attack()
    {
        //decide between DistanceAttack and BodyAttack (random)
        int i = Random.Range(0, 2);
        transform.LookAt(Vector3.zero);
        if (i == 0)
        {
            DistanceAttack();
        }
        else
        {
            BodyAttack();
        }
        Task.current.Succeed();
    }

    void DistanceAttack()
    {
        ef.Attack();
    }

    void BodyAttack()
    {
        activeBodyAttack = true;
        ActivateBoost();



    }

    [Task]
    void Defend()
    {
        //decide between ActivateShield and ActivateBoost (random)

        ActivateBoost();
        // ActivateShield

        Task.current.Succeed();
    }

    void ActivateShield()
    {

    }

    void ActivateBoost()
    {
        activeBoost = true;
    }

    [Task]
    void CheckMovementActive()
    {
        //check timer (Return false if is finished)
        if (timer > 0) Task.current.Succeed();
        else Task.current.Fail();

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.2f);
        int i = Random.Range(0, 2); //Stop and random to change direction

        if (i == 0)
        {
            transform.eulerAngles += new Vector3(0, 180f, 0);
        }
        else
        {
            transform.eulerAngles += new Vector3(0, 90f, 0);
        }
    }
}
