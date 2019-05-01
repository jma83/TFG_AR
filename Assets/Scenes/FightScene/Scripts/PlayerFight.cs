using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerFight : FightEntity
{
    //[SerializeField] private Button fireButton;
    //[SerializeField] private Button defendButton;
    private int bonusAtack;
    private int bonusDefense;
    private int bonusSpeed;
    private Player ply;
    private bool hit;
    private bool badVisibility;

    private AudioSource audioSource;
    private AudioSource audioSource2;
    private AudioClip damageSound;
    private AudioClip shieldSound;
    private AudioClip shieldHitSound;
    private AudioClip visibleSound;
    private AudioClip shieldActiveSound;

    // Use this for initialization
    public void Start () {
        //fireButton.onClick.AddListener(Attack);
        //defendButton.onClick.AddListener(Defend);
        weapon = gameObject.GetComponent<Weapon>();

        ply = GameManager.Instance.CurrentPlayer;
        hit = false;
        AI = false;
        audioSource = GetComponents<AudioSource>()[1];
        audioSource2 = GetComponents<AudioSource>()[2];
        damageSound = Resources.Load<AudioClip>("Audio/NewAudio/explosion_02");
        visibleSound = Resources.Load<AudioClip>("Audio/NewAudio/enemy-ability");
        shieldSound = Resources.Load<AudioClip>("Audio/NewAudio/activate-shield");
        shieldActiveSound = Resources.Load<AudioClip>("Audio/NewAudio/loop_ambient_01");
        shieldHitSound = Resources.Load<AudioClip>("Audio/NewAudio/shield-hit");
    }

    // Update is called once per frame

    public override void DealDamage(int d)
    {
        //decrease health calling player
        if (!defend) { 
            ply.substractHp(d);
            hp = ply.Hp;
            audioSource2.PlayOneShot(damageSound);
        }
        else
        {
            audioSource.PlayOneShot(shieldHitSound);
        }
        
        //hud transition
        SetHit(true);

    }

    public override void SetDefend(bool b)
    {
        if (b)
        {
            audioSource.PlayOneShot(shieldSound);
            audioSource2.clip = shieldActiveSound;
            audioSource2.Play();
        }
        else
        {
            audioSource2.Stop();
        }
        defend = b;
    }

    public void SetHit(bool b)
    {
        hit = b;
    }

    public void SetBadVisibility(bool b)
    {
        if (!defend)
        {
            badVisibility = b;
            audioSource.PlayOneShot(visibleSound);

        }
    }

    public bool GetBadVisibility()
    {
        return badVisibility;
    }

    public bool GetHit()
    {
        return hit;
    }

}
