using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Weapon : MonoBehaviour
{

    private string path;
    private GameObject bullet;
    private Rigidbody rb;
    BulletCollisionManager bt;
    private int attack;
    private int defense;
    private int speed;
    private int type;
    private int quality;
    private int durability;
    private string owner;
    protected float targetAttackTime = 0f;
    protected float reloadDefendTime = 0f;
    protected float targetDefendTime = 0f;
    private float defaultTargetDefendTime;
    private float defaultTargetAttackTime;
    private AudioSource audioSource;


    private void Start()
    {
        reloadDefendTime = targetDefendTime = targetAttackTime = 0.0f;
         
        defaultTargetDefendTime = 5f;
        //default stats for weapon:
        if (attack==0)
            attack = 5;
        if (defense == 0)
            defense = 5;
        if (speed == 0)
            speed = 5;
        if (durability == 0)
            durability = -1;


        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = Resources.Load("FightScene/laser") as AudioClip;
        owner = gameObject.tag;
    }

    // Use this for initialization
    void Awake()
    {
        //bullet = new GameObject();


    }

    // Update is called once per frame
    void Update()
    {
        if (targetAttackTime >= 0)
            targetAttackTime -= Time.deltaTime;
        if (targetDefendTime >= 0)
        {
            targetDefendTime -= Time.deltaTime;
        }
        else
        {
            if (reloadDefendTime >= 0)
                reloadDefendTime -= Time.deltaTime;
        }

    }

    public void SetWeaponStats(int durability,int type,int quality,int attack,int defense,int speed, string owner = null)
    {
        if (durability > 0)
        {
            Debug.Log("SetWeaponStats: " + attack + ", " + defense + ", " + speed);
            this.attack = attack;
            this.defense = defense;
            this.speed = speed;
            this.durability = durability;
            this.type = type;
            this.quality = quality;
        }
        this.owner = owner;
        
    }

    public bool CreateBullet()
    {
        bullet = null;
        if (targetAttackTime < 1)
        {
            //Debug.Log("CreateBullet: " + attack + ", " + defense + ", " + speed);
            bullet = Instantiate(Resources.Load("FightScene/bullet", typeof(GameObject)), gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            bt = bullet.gameObject.GetComponent<BulletCollisionManager>();
            //bt.gameObject.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
            bt.SetDamage(attack);
            bt.SetOwner(owner);
            rb = bullet.GetComponent<Rigidbody>();
            if (GameObject.FindGameObjectsWithTag("camera").Length <= 0)
            {
                print("Error. No cameras found");
                return false;
            }

            if (owner == "Player")
            {
                bullet.transform.rotation = GameObject.FindGameObjectsWithTag("camera")[0].transform.rotation;
                bullet.transform.position = GameObject.FindGameObjectsWithTag("camera")[0].transform.position;
            }

            targetAttackTime = 2.5f;

            if (speed != 5)
            {
                if (speed - 5 > 50) targetAttackTime = 1f;
                else if (speed - 5 > 40) targetAttackTime = 1.2f;
                else if (speed - 5 > 30) targetAttackTime = 1.4f;
                else if (speed - 5 > 20) targetAttackTime = 1.6f;
                else if (speed - 5 > 10) targetAttackTime = 1.8f;
                else if (speed - 5 > 5) targetAttackTime = 2f;
                else targetAttackTime = 2.5f;
            }
            return true;

        }
        return false;

    }
    public void Shoot(Vector3 force)
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.Play();
        rb.AddForce(force * (speed * 60));

        Destroy(bullet, 3);
    }

    public bool ActivateShield(bool priorityAI)
    {
        Debug.Log("holii");
        if (targetDefendTime <= 0 && reloadDefendTime <=0 || priorityAI)
        {
            targetDefendTime = defaultTargetDefendTime;
            reloadDefendTime = defaultTargetDefendTime;
            return true;
        }
        return false;
    }

    public void SetHeavyStrike(bool b)
    {
        if (b)
        {
            attack = attack * 2;
        }
        else
        {
            attack = attack / 2;
        }

    }

    public void DecreaseWeaponDurability(int value)
    {
        durability = durability - value;

        if (durability < 0) durability = 0;
    }

    public string GetWeaponType()
    {
        if (type == 3)
        {
            return "Fast";
        }
        else if (type == 2)
        {
            return "Defensive";
        }
        else if (type == 1)
        {
            return "Ofensive";
        }
        else if (type == 0)
        {
            return "Balanced";
        }

        return null;
    }

    public string GetWeaponQuality()
    {

        if (quality == 3)
        {
            return "GUI/legendQuality";
        }
        else if (quality == 2)
        {
            return "GUI/epicQuality";
        }
        else if (quality == 1)
        {
            return "GUI/rareQuality";
        }
        else if (quality == 0)
        {
            return "GUI/basicQuality";
        }
        return null;
        
    }

    public int GetWeaponStats()
    {
        return attack+defense+speed;
    }

    public int GetWeaponDurability()
    {
        return durability;
    }

    public float GetTargetAttackTime()
    {
        return targetAttackTime;
    }
    public float GetTargetDefendTime()
    {
        return targetDefendTime;
    }
    public float GetReloadDefendTime()
    {
        return reloadDefendTime;
    }
}
