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
    private string owner;
    protected float targetTime;


    private void Start()
    {
        targetTime = 0.0f;
        //default stats for weapon:
        attack = 5;
        defense = 5;
        speed = 5;
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

    public void SetWeaponStats(int attack,int defense,int speed, string owner = null)
    {
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
        this.owner = owner;
    }

    public GameObject CreateBullet()
    {
        bullet = null;
        if (targetTime < 1)
        {
            bullet = Instantiate(Resources.Load("FightScene/bullet", typeof(GameObject))) as GameObject;

            bt = bullet.gameObject.GetComponent<BulletCollisionManager>();
            bt.SetDamage(attack);
            bt.SetOwner(owner);
            rb = bullet.GetComponent<Rigidbody>();
            if (GameObject.FindGameObjectsWithTag("camera").Length <= 0)
            {
                print("Error. No cameras found");
                return null;
            }

            bullet.transform.rotation = GameObject.FindGameObjectsWithTag("camera")[0].transform.rotation;
            bullet.transform.position = GameObject.FindGameObjectsWithTag("camera")[0].transform.position;

            targetTime = 1.5f;

        }
        return bullet;

    }
    public void Shoot()
    {
        rb.AddForce(GameObject.FindGameObjectsWithTag("camera")[0].transform.forward * 500f);

        Destroy(bullet, 3);
    }

}
