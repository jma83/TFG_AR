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
    [SerializeField]  private int type;

    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        enemySound = Resources.Load<AudioClip>("Audio/NewAudio/start_battle2");
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
        if (EventSystem.current.currentSelectedGameObject == null && !EventSystem.current.IsPointerOverGameObject() && !IsPointerOverUIObject())
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
                    WindowAlert.Instance.ClearMessages();
                    WindowAlert.Instance.CreateConfirmWindow("Necesitas energia para luchar contra este Enemigo", true);
                }
            }
        }
    }

    public void SetDroidType(int i)
    {
        type = i;

        if (type == 2)
        
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material = Resources.Load("FightScene/BossMaterial", typeof(Material)) as Material;
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
