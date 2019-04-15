using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAdvancedBehaviour : EnemyBehaviour
{
    protected int abilityType;
    protected bool abilityEnabled = false;
    protected float protectTimer = 0;
    protected float abilityTimer = 0;
    protected float abilityRestartTimer = 0;
    protected bool checker = false;
    protected bool checkIdle = true;
    BulletCollisionManager[] bullets;
    // Use this for initialization
    public override void StartStats() { 
        abilityType = Random.Range(0, 3);
        speed = 5f;
    }

    // Update is called once per frame
    public override void UpdateStats () {
        
        if (abilityTimer > 0)
        {
            abilityTimer = abilityTimer - Time.deltaTime;
        }
        else
        {
            if (abilityRestartTimer <= 0 && !abilityEnabled && checker)
            {
                checker = false;
                abilityRestartTimer = 5f;
                ef.HideEnemy(false);
                GameManager.Instance.CurrentPlayer.gameObject.GetComponent<PlayerFight>().SetBadVisibility(false);
            }
            if (abilityRestartTimer > 0)
            {
                abilityRestartTimer = abilityRestartTimer - Time.deltaTime;
            }
            else
            {
                abilityEnabled = true;
            }            
        }

        

        if (protectTimer > 0)
            protectTimer = protectTimer - Time.deltaTime;

    }

    public void SetTimerOK()
    {
        defaultTime = Random.Range(2.5f, 5f);
        timer = defaultTime;
    }


    [Task]
    public void UseAbility()
    {
        if (abilityEnabled) {
            checker = true;
            Debug.Log("uso mi habilidad, tipo: " + abilityType);
            abilityTimer = 4f;

            switch (abilityType)
            {
                case 0:
                    GameManager.Instance.CurrentPlayer.gameObject.GetComponent<PlayerFight>().SetBadVisibility(true); //limitVisibility (affect player ui -> texture)
                    abilityTimer = 5f;
                    break;
                case 1:
                    ef.HideEnemy(true); //hide (call function to reduce opacity: alpha factor)
                    break;
                case 2:
                    StartCoroutine(WaitAndAttack());
                    break;
                case 3:
                    if (ef.GetHP() < ef.GetMaxHP())
                        ef.Heal(3); // (this ability should be limited -> op: up some health)
                    break;
                    
            }

            abilityEnabled = false;
            if (checkIdle)
            Idle();
            Task.current.Succeed();

        }
        else
        {
            Task.current.Fail();

        }
    }

    [Task]
    public void CheckProtect()
    {
        bool check = false;
        bullets = FindObjectsOfType<BulletCollisionManager>();
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i].GetOwner() == "Player" && Vector3.Distance(bullets[i].transform.position, gameObject.transform.position) <= 4)
            {
                Task.current.Succeed();
                check = true;
                break;
            }
        }
        if (!check)
        Task.current.Fail();

    }

    [Task]
    public void Protect()   //detect if a bullet from the player is near to protect himself in time (boost or shield)
    {
        if (protectTimer <= 0)
        {
            Debug.Log("proteccion anticipada!!!");
            ef.SetStateAI(StateAI.Defend);

            //decide between ActivateShield and ActivateBoost (random)
            int j = Random.Range(0, 2);

            if (j == 0)
                activeBoost = true;     // ActivateBoost
            else
                ef.Defend();    // ActivateShield

            protectTimer = 15f;
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    public IEnumerator WaitAndAttack()
    {
        yield return new WaitForSeconds(3.2f);
        transform.LookAt(Vector3.zero);
        ef.HeavyAttack(); //(heavyAttack call)
        yield return new WaitForSeconds(1.0f);

    }
}