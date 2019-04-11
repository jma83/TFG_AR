using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAdvancedBehaviour : EnemyBehaviour
{
    private int abilityType;
    private bool abilityEnabled = false;
    private bool protect  =false;
    private float abilityTimer;
    BulletCollisionManager[] bullets;
    // Use this for initialization
    public void Awake() { 
        abilityType = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update () {
        if (abilityTimer > 0)
        {
            abilityTimer = abilityTimer - Time.deltaTime;
        }

    }

    [Task]
    public void UseAbility()
    {
        if (abilityEnabled) {
            switch (abilityType)
            {
                case 0:
                    ef.Heal(5); // (this ability should be limited -> op: up some health)
                    break;
                case 1:
                    ef.HideEnemy(true); //hide (call function to reduce opacity: alpha factor)
                    abilityTimer = 4f;
                    break;
                case 2:
                    ef.HeavyAttack(); //(heavyAttack call)
                    break;
                case 3:
                    GameManager.Instance.CurrentPlayer.gameObject.GetComponent<PlayerFight>().SetBadVisibility(true); //limitVisibility (affect player ui -> texture)
                    abilityTimer = 5f;
                    break;
            }
            abilityEnabled = false;
            Task.current.Succeed();

        }
        else
        {
            Task.current.Fail();

        }
    }

    [Task]
    public void Protect()   //detect if a bullet from the player is near to protect himself in time (boost or shield)
    {
        if (!protect)
        {
            bullets = FindObjectsOfType<BulletCollisionManager>();
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].GetOwner() == "Player" &&  Vector3.Distance(bullets[i].transform.position, gameObject.transform.position) <= 4)
                {
                    ef.SetStateAI(StateAI.Defend);

                    //decide between ActivateShield and ActivateBoost (random)
                    int j = Random.Range(0, 2);

                    if (j == 0)
                        activeBoost = true;     // ActivateBoost
                    else
                        ef.Defend();    // ActivateShield

                    protect = true;
                    Task.current.Succeed();

                }
            }
        }
        else
        {
            Task.current.Fail();

        }
    }
}