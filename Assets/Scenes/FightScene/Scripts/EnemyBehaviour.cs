using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    protected EnemyFight ef;
    protected EnemyFightManager efm;
    public float timer;
    protected float defaultTime;
    protected bool activeBoost;
    protected float boostFactor;
    protected bool activeBodyAttack;
    protected bool checkLimit;
    protected bool checkCounter = false;
    protected float speed;
    

    public void Start()
    {
        efm = EnemyFightManager.Instance;
        ef = gameObject.GetComponent<EnemyFight>();
       
        SetTimerOK();
        StartStats();
    }

    public virtual void StartStats()
    {
        defaultTime = 4f;
        speed = 3f;
        boostFactor = 2.5f;
        timer = 0;
        activeBoost = false;
        checkLimit = false;

    }

    public void Update()
    {
        if (timer > 0) timer = timer - Time.deltaTime;

        if (activeBodyAttack)
            activeBodyAttack = ef.UpdateBodyAttack();

        UpdateStats();
    }

    public virtual void UpdateStats()
    {
    }

    void SetTimerOK()
    {        
        defaultTime = Random.Range(3f, 6f);
        timer = defaultTime;        
    }

    

    [Task]
    public void CheckAttacked()
    {
        ef.SetStateAI(StateAI.Check);
        if (ef.GetAttacked() && !ef.GetDefend())   // check if recently has been attacked
        {
            ef.SetAttacked(false);  // reset attacked status to false
            checkCounter = true;    // counter attack active -> It allows to attack even if the timer is not over
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    public void InitMove()
    {
        ef.SetStateAI(StateAI.Check);
        if (timer <= 0)
        {
            SetTimerOK();
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    public void Move()
    {
        if (timer > 0)
        {
            ef.SetStateAI(StateAI.Move);
            if (!activeBoost)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);  //Move at a certain distance from the objective.
            }
            else
            {
                transform.Translate(Vector3.forward * speed * boostFactor * Time.deltaTime);    //Move with a boost
                activeBoost = false;
            }
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

    }
    public void Idle()
    {
        ef.SetStateAI(StateAI.Idle);
        StartCoroutine("Wait");
    }

    [Task]
    public void Attack()
    {
        if (timer <= 0 || timer == 3)
            checkLimit = false;
        if (efm.CheckEnemyAttack_Mutex(ef) || checkCounter)
        {
            checkCounter = false;
            ef.SetStateAI(StateAI.Attack);

            //decide between DistanceAttack and BodyAttack (random)
            int i = Random.Range(0, 2);
            transform.LookAt(Vector3.zero);
            if (i == 0)
                DistanceAttack();
            else
                BodyAttack();

            Idle();
            Task.current.Succeed();
        }
        else
        {
            if (timer<=0)
            SetTimerOK();
            Task.current.Fail();
        }
    }

    public void DistanceAttack()
    {
        ef.Attack();
    }

    public void BodyAttack()
    {
        activeBodyAttack = true;
        activeBoost = true;
    }

    [Task]
    public void Defend()
    {
        checkLimit = false;
        ef.SetStateAI(StateAI.Defend);

        //decide between ActivateShield and ActivateBoost (random)
        int i = Random.Range(0, 2);

        if (i == 0)
            activeBoost = true;     // ActivateBoost
        else
            ef.Defend();    // ActivateShield

        Task.current.Succeed();
    }

    [Task]
    public void CheckMovementActive()
    {
        //check timer (Return false if is finished)
        if (timer > 0)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    public void CheckLimit()
    {
        // Check if patrol movement is inside the limits (distance, colision and timer of movement). There is also a flag: checkLimit to avoid entering several times
        if ((Vector3.Distance(gameObject.transform.position, Vector3.zero) > 12.0f || ef.GetReturnCollision()) && timer > 0 && checkLimit == false) 
        {
            transform.eulerAngles += new Vector3(0, 180f, 0);   //turn around
            checkLimit = true;  // active flag to avoid repeat 

            ef.SetReturnCollision(false);   //set collision to false
            Task.current.Succeed();

        }
        Task.current.Fail();
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.2f);

        int i = Random.Range(0, 3); //Stop and random to change direction

        if (gameObject.transform.rotation.x > 30 || gameObject.transform.rotation.x < -30 || gameObject.transform.rotation.z > 30 || gameObject.transform.rotation.z < -30)
        gameObject.transform.rotation = Quaternion.Euler(0, (gameObject.transform.rotation.eulerAngles.y), 0);
        if (i == 0)
            transform.eulerAngles += new Vector3(0, 180f, 0);
        else if (i == 1)
            transform.eulerAngles += new Vector3(0, 90f, 0);
        else if (i == 2)
            transform.eulerAngles += new Vector3(0, -90f, 0);

    }
}
