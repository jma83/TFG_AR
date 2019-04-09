using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    private EnemyFight ef;
    private EnemyFightManager efm;
    public float timer;
    private float defaultTime;
    private bool activeBoost;
    private bool activeBodyAttack;
    private bool checkLimit;
    private float speed;
    private void Start()
    {
        efm = EnemyFightManager.Instance;
        ef = gameObject.GetComponent<EnemyFight>();
        defaultTime = 4f;
        speed = 3f;
        timer = 0;
        activeBoost = false;
        checkLimit = false;
        SetTimerOK();
    }

    private void Update()
    {
        if (timer > 0) timer = timer - Time.deltaTime;

        if (activeBodyAttack == true) {
            Vector3 v = new Vector3(0, Random.Range(-1f, 5f), 0);
            transform.position = Vector3.Lerp(this.gameObject.transform.position, v, Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position,Vector3.zero) < 1.5)
            {
                activeBodyAttack = false;
                
                gameObject.transform.rotation = Quaternion.Euler(0, (gameObject.transform.rotation.eulerAngles.y), 0);
            }
        }
    }


    void SetTimerOK()
    {
        
        defaultTime = Random.Range(3f, 6f);
        timer = defaultTime;
        
    }

    [Task]
    void CheckAttacked()
    {
        ef.SetStateAI(StateAI.Check);
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
    void Move()
    {
        if (timer > 0)
        {
            ef.SetStateAI(StateAI.Move);
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
    void Idle()
    {
            ef.SetStateAI(StateAI.Idle);
            StartCoroutine("Wait");
        //Task.current.Succeed();

    }

    [Task]
    void Attack()
    {
        if (timer <= 0 || timer == 3)
            checkLimit = false;
        if (efm.CheckEnemyAttack_Mutex(ef))
        {
            ef.SetStateAI(StateAI.Attack);

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
        checkLimit = false;
        ef.SetStateAI(StateAI.Defend);
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
        if (timer > 0)
            Task.current.Succeed();
        else
            Task.current.Fail();

    }
    /*[Task]
    void CheckLimit()
    {
        if (Vector3.Distance(gameObject.transform.position, Vector3.zero) > 12.0f && timer > 0 && checkLimit == false)  //        if (checkLimit == false && ef.GetReturnTimer() > 0)
        {
            transform.eulerAngles += new Vector3(0, 180f, 0);
            timer = 0f;
            checkLimit = true;
        }
        Task.current.Fail();
    }*/

    [Task]
    void CheckLimit()
    {
        if ((Vector3.Distance(gameObject.transform.position, Vector3.zero) > 12.0f || ef.GetReturnTimer() == 1f) && timer > 0 && checkLimit == false)  //        if (checkLimit == false && ef.GetReturnTimer() > 0)
        {
            transform.eulerAngles += new Vector3(0, 180f, 0);
            //timer = 0f;
            checkLimit = true;

            ef.SetReturnTimer(0);
            Task.current.Succeed();

        }
        Task.current.Fail();
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
