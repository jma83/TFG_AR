using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private int xp = 0;
    [SerializeField] private int requiredXp = 100;
    [SerializeField] private int levelBase = 100;
    [SerializeField] private List<GameObject> droids = new List<GameObject>();
    private Animator walk;
    //private Vector3 temp_pos;
    private int lvl = 0;
    private int total_xp = 0;
    private int hp = 100;
    private int maxHp = 100;
    private int minHp = 0;
    private float captureRange = 0.5f;
    protected float targetTime =0;
    public CaptureRange captureRangeObj;
    private int xp_multiplier = 1;



    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }
    
    public int Xp_Multiplier
    {
        get { return xp_multiplier; }
        set { xp_multiplier = value; }
    }
    public float CaptureRange
    {
        get { return captureRange; }
        set { captureRange = value; }
    }
    public CaptureRange CaptureRangeObj
    {
        get { return captureRangeObj; }
        set { captureRangeObj = value; }
    }
    public int Xp
    {
        get { return xp; }
        set { xp = value; }
    }
    public int RequiredXp
    {
        get { return requiredXp; }
        set { requiredXp = value; }
    }
    public int LevelBase
    {
        get { return levelBase; }
        set { levelBase = value; }
    }
    public List<GameObject> Droids
    {
        get { return droids; }
        set { droids = value; }
    }

    public int Lvl
    {
        get{ return lvl; }
        set { lvl = value; }
    }
    private void Start () {
        walk = this.GetComponent(typeof(Animator)) as Animator;
        //temp_pos = Vector3.zero;
        InitLevelData(0);
	}

    public void SetMaxCaptureRange(float f)
    {
        if (f <= 2.0f && f > 0f)
            captureRange = f;
        else
            captureRange = 0.5f;
    }

    public void AddXp(int xp)
    {
        int value= Mathf.Max(0, xp); 
        int diff = 0;
        this.xp += value* xp_multiplier;
        total_xp += value* xp_multiplier;
        if (this.xp >= requiredXp)
        {
            diff = this.xp - requiredXp;
            InitLevelData(diff);
        }
    }

    public void SetXpMultiplier(int x)
    {
        xp_multiplier = x;
    }
    public int GetXpMultiplier()
    {
        return xp_multiplier;
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
        if (lvl != 1)
            WindowAlert.Instance.CreateInfoWindow("SUBES DE NIVEL!\n Nivel " + lvl,true);
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

    public void playerTimer()
    {
        if (targetTime > 0)
            targetTime -= Time.deltaTime;
        if (targetTime <= 0)
        {
            //temp_pos = transform.localPosition;
            targetTime = 1;
        }
    }
    public void Walk()
    {
        
        if (transform.hasChanged) //if (temp_pos != transform.localPosition)
        {
            walk.SetBool("walk", true);
            StartCoroutine(Wait());
        }
        else
        {
            walk.SetBool("walk", false);
        }
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);

        transform.hasChanged = false;
    }
}
