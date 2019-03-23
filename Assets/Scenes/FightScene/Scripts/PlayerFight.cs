using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerFight : MonoBehaviour {
    public Button fireButton;
    private Weapon bullet;
    private float targetTime;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        bullet = gameObject.AddComponent(typeof(Weapon)) as Weapon;
        fireButton.onClick.AddListener(Attack);
        targetTime = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (targetTime >= 0)
            targetTime -= Time.deltaTime;
        
    }

    void Attack()
    {
        if (targetTime < 1)
        {
            bullet.CreateBullet();
            bullet.Shoot();
            audioSource = this.GetComponent<AudioSource>();
            audioSource.Play();
            targetTime = 1.5f;
        }
    }

    void Defend()
    {

    }
}
