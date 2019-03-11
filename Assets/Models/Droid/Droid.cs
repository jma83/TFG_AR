using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Droid : MapEntity {

    [SerializeField] private float spawnRate = 0.10f;
    //[SerializeField] private float catchRate = 0.10f;
    [SerializeField] private int attack = 0;
    [SerializeField] private int defense = 0;
    [SerializeField] private int hp = 10;
    [SerializeField] private AudioClip enemySound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(audioSource);
        Assert.IsNotNull(enemySound);

    }
    private void Start()
    {
        //DontDestroyOnLoad(this);
    }

    public float SpawnRate
    {
        get { return spawnRate;  }
    }

    /*public float CatchRate
    {
        get { return spawnRate; }
    }*/

    public int Attack
    {
        get { return attack; }
    }

    public float Defense
    {
        get { return defense; }
    }

    public float Hp
    {
        get { return hp; }
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            ARGameSceneManager[] managers = FindObjectsOfType<ARGameSceneManager>();

            foreach (ARGameSceneManager scenemanager1 in managers)
            {
                if (scenemanager1.gameObject.activeSelf)
                {
                    if (captureRange)
                    {
                        audioSource.PlayOneShot(enemySound);
                        scenemanager1.ChangeScene(this.gameObject,0);
                    }
                    //SceneManager.LoadScene("FightScene");
                }
            }
        }
    }
}
