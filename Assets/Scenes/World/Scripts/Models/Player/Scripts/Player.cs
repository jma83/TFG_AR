using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private int xp = 0;
    private int requiredXp = 100;
    private int levelBase = 100;
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
    private Vector3 aux_RespawnPos;
    private string user_name;
    private float soundLevel = -1;



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
    public int Total_xp
    {
        get { return total_xp; }
        set { total_xp = value; }
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

    public int Lvl
    {
        get{ return lvl; }
        set { lvl = value; }
    }
    private void Start () {
        walk = this.GetComponent(typeof(Animator)) as Animator;
        //temp_pos = Vector3.zero;
        InitLevelData(0,false);
        Debug.Log("Start RespawnPos: " + aux_RespawnPos.x + " " + aux_RespawnPos.y + " " + aux_RespawnPos.z);
        aux_RespawnPos = transform.position;

    }

    public void SetMaxCaptureRange(float f)
    {
        if (f <= 2.0f && f > 0f)
            captureRange = f;
        else
            captureRange = 0.5f;
    }

    public void SetUserName(string s)
    {
        user_name = s;
    }

    public void SetSoundLevel(float f,bool b=false)
    {
        soundLevel = f;
        if (b)
        {
            AudioSource[] audio = FindObjectsOfType<AudioSource>();

            for (int i = 0; i < audio.Length; i++)
            {
                audio[i].volume = f / 100;
            }
        }
    }

    public void AddXp(int xp)
    {
        int value= Mathf.Max(0, xp); 
        int diff = 0;
        this.xp += value* xp_multiplier;
        Debug.Log("AddXp XP: " + this.xp);
        total_xp += value* xp_multiplier;
        if (this.xp >= requiredXp)
        {
            diff = this.xp - requiredXp;
            InitLevelData(diff,true);
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

    private void InitLevelData(int diff,bool add)
    {
        if (!add && lvl == 0 || add)
        {
            lvl++;
            xp = diff;
        }
        requiredXp = levelBase * lvl;
        
        Debug.Log("InitLevelData XP: " + xp);
        if (lvl != 1 && add)
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
        if (walk!=null)
        Walk();

        playerTimer();
        if (Vector3.Distance(transform.position, aux_RespawnPos) > 80)
            Debug.Log(Vector3.Distance(transform.position, aux_RespawnPos));

    }

    public void playerTimer()
    {
        if (targetTime > 0)
            targetTime -= Time.deltaTime;
        if (targetTime <= 0)
        {
            if (Vector3.Distance(transform.position,aux_RespawnPos) > 80)
            {
                aux_RespawnPos = transform.position;
                Debug.Log("aux_RespawnPos: " + aux_RespawnPos);
                DroidFactory.Instance.ResetDroids();
            }
            //temp_pos = transform.localPosition;
            targetTime = 10;
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

    public void SetAuxRespawnPos(Vector3 v)
    {
        Debug.Log("Seteamos SetAuxRespawnPos: " + v.x + " " + v.y + " " + v.z);
        aux_RespawnPos = v;
    }

    public Vector3 GetAuxRespawnPos()
    {
        return aux_RespawnPos;
    }

    public string GetUserName()
    {
        return user_name;
    }

    public float GetSoundLevel()
    {
        return soundLevel;
    }
}
