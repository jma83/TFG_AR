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
    protected float targetTime;
    private AudioSource audioSource;


    private void Start()
    {
        targetTime = 0.0f;

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
        if (targetTime >= 0)
            targetTime -= Time.deltaTime;
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

    public GameObject CreateBullet()
    {
        bullet = null;
        if (targetTime < 1)
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
                return null;
            }

            if (owner == "Player")
            {
                bullet.transform.rotation = GameObject.FindGameObjectsWithTag("camera")[0].transform.rotation;
                bullet.transform.position = GameObject.FindGameObjectsWithTag("camera")[0].transform.position;
            }

            targetTime = 1.5f;

        }
        return bullet;

    }
    public void Shoot(Vector3 force)
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.Play();
        rb.AddForce(force * (100f*speed));

        Destroy(bullet, 3);
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
}
