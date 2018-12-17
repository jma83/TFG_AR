using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private int xp = 0;
    [SerializeField] private int requiredXp = 100;
    [SerializeField] private int levelBase = 100;
    [SerializeField] private List<GameObject> droids = new List<GameObject>();
    private Animator walk;
    private Vector3 temp_pos;
    private int lvl = 0;
    private int total_xp = 0;
    private int hp = 100;
    private int maxHp = 100;
    private int minHp = 0;
    private float captureRange = 0.5f;
    private GameObject[] near_entities;


    public GameObject[] NearEntities
    {
        get { return near_entities;  }
        set { near_entities=value; }
    }
    public int Hp
    {
        get { return hp; }
    }
    public float CaptureRange
    {
        get { return captureRange; }
    }
    public int Xp
    {
        get { return xp; }
    }
    public int RequiredXp
    {
        get { return requiredXp; }
    }
    public int LevelBase
    {
        get { return levelBase; }
    }
    public List<GameObject> Droids
    {
        get { return droids; }
    }

    public int Lvl
    {
        get{ return lvl; }
    }
    private void Start () {
        walk = this.GetComponent(typeof(Animator)) as Animator;
        temp_pos = this.transform.position;
        InitLevelData(0);
	}
    public void AddXp(int xp)
    {
        int value= Mathf.Max(0, xp); 
        int diff = 0;
        this.xp += value;
        total_xp += value;
        if (this.xp >= requiredXp)
        {
            diff = this.xp - requiredXp;
            InitLevelData(diff);
        }
    }
    public void Adddroid(GameObject droid)
    {
        droids.Add(droid);
    }
    private void InitLevelData(int diff)
    {
        lvl++;
        requiredXp = levelBase * lvl;
        xp = diff;
    }
    public void addHp(int diff)
    {
        if (hp < maxHp)
            hp += diff;
        if (hp > maxHp)
            hp = maxHp;
    }
    public void substractHp(int diff)
    {
        if (hp > minHp)
            hp -= diff;
        if (hp < minHp)
            hp = minHp;
    }
    private void Update()
    {
        Walk();
    }
    public void Walk()
    {

        if (transform.hasChanged)
        {
            walk.SetBool("walk", true);
            StartCoroutine(Wait());
        }
        else
        {
            walk.SetBool("walk", false);
        }
        temp_pos = this.transform.position;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);

        transform.hasChanged = false;
    }
}
