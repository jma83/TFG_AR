using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Droid : MapEntity {

    [SerializeField] private float spawnRate = 0.10f;
    //[SerializeField] private float catchRate = 0.10f;
    [SerializeField] private int hp = 10;
    [SerializeField] private AudioClip enemySound;
    private int type;

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

    public float Hp
    {
        get { return hp; }
    }

    private void OnMouseDown()
    {        
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (captureRange)
            {
                if (GameManager.Instance.CurrentPlayer.Hp > 0)
                {
                    ARGameSceneManager[] managers = FindObjectsOfType<ARGameSceneManager>();

                    foreach (ARGameSceneManager scenemanager1 in managers)
                    {
                        if (scenemanager1.gameObject.activeSelf)
                        {
                            DroidFactory.Instance.SelectDroid(this);
                            audioSource.PlayOneShot(enemySound);
                            scenemanager1.ChangeScene(this.gameObject, 0);

                            //SceneManager.LoadScene("FightScene");
                        }
                    }
                }
                else
                {
                    WindowAlert.Instance.CreateInfoWindow("Necesitas energia para luchar contra este Enemigo", true);
                }
            }
        }
    }

    public void SetDroidType(int i)
    {
        type = i;
        if (type == 0)
        {
            
            gameObject.GetComponentInChildren<MeshRenderer>().material = Resources.Load("WorldScene/Maps/Materials/door_mtl1_diffcol", typeof(Material)) as Material;
        }
        else if (type == 1)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material = Resources.Load("WorldScene/Maps/Materials/bmq1", typeof(Material)) as Material;
        }
    }

    public int GetDroidType()
    {
        return type;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
