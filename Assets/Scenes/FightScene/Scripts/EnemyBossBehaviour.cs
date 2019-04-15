using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossBehaviour : EnemyAdvancedBehaviour
{
    private bool axisX = false;
    private int axisY = 0;

    [Task]
    public void Move2()
    {
        transform.LookAt(Vector3.zero);
        if (timer > 0)
        {
            ef.SetStateAI(StateAI.Move);
            Vector3 v1 = Vector3.one;
            Vector3 v2 = Vector3.one;

            if (axisY == 1)
                v1 = Vector3.up;  //Move at a certain distance from the objective.
            else if (axisY == 2)
                v1 = Vector3.down;

            if (axisX)
                v2 = Vector3.right;  //Move at a certain distance from the objective.
            else
                v2 = Vector3.left;

            if (!activeBoost)
            {
                if (timer <= 0.5 && axisY != 0)
                    transform.Translate(v1 * speed * Time.deltaTime);
                transform.Translate(v2 * speed * Time.deltaTime);
            }
            else
            {
                if (timer >= 1.5 && timer <= 2 && axisY != 0)
                    transform.Translate(v1 * speed * boostFactor * Time.deltaTime);    //Move with a boost
                transform.Translate(v2 * speed * boostFactor * Time.deltaTime);
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
    public void Attack2()
    {
        if (timer <= 0 || timer == 3)
            checkLimit = false;


        if (timer <= 0)
        {
            axisX = !axisX;   //turn around
            axisY++;
            if (axisY >= 3) axisY = 0;
            SetTimerOK();
        }

        if (efm.CheckEnemyAttack_Mutex(ef) || checkCounter)
        {
            checkCounter = false;
            ef.SetStateAI(StateAI.Attack);

            DistanceAttack();
            //Idle();
            
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

    }
    [Task]
    public void CheckLimit2()
    {
        // Check if patrol movement is inside the limits (distance, colision and timer of movement). There is also a flag: checkLimit to avoid entering several times
        if ((Vector3.Distance(gameObject.transform.position, Vector3.zero) > 12.0f || ef.GetReturnCollision()) && timer > 0 && checkLimit == false)
        {
            
            checkLimit = true;  // active flag to avoid repeat 
            Debug.Log("Translate");
            transform.Translate(Vector3.forward * speed * boostFactor * Time.deltaTime);
            ef.SetReturnCollision(false);   //set collision to false
            Task.current.Succeed();

        }
        Task.current.Fail();
    }
    [Task]
    public void SelectAbility()
    {
        abilityType = Random.Range(0, 4);
        checkIdle = false;
        boostFactor = 4f;
        Task.current.Succeed();
    }
}
