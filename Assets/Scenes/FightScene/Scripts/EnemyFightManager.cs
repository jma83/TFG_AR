using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightManager : Singleton<EnemyFightManager> {

    private GameObject[] enemies;
    private List<EnemyFight> enemyOrderList;
    private float timer;
    private int cont;
    private EnemyFight id_mutex;
    private int total_xp;
    private bool gameOver;
    private bool gameWin;
    private AudioSource audioSource;
    private AudioClip winSound;
    private AudioClip loseSound;
    private Equipment eq;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        loseSound = Resources.Load<AudioClip>("Audio/NewAudio/lose");
        winSound = Resources.Load<AudioClip>("Audio/NewAudio/win");

        enemyOrderList = new List<EnemyFight>();
        cont = 1;
        UpdateOrderList();
        gameOver = false;
        gameWin = false;

        eq = gameObject.GetComponent<Equipment>();
        eq.SetRandomStats();
        eq.SetDurability(50);
    }

    public void InstantiateEnemies(int type)
    {
        EnemyFight[] ef = FindObjectsOfType<EnemyFight>();
        Debug.Log("InstantiateEnemies: " + type);
        if (type == 0)
        {

            for (int j=0;j< ef.Length; j++)
            {
                if (ef[j].gameObject.GetComponent<EnemyAdvancedBehaviour>() != null)
                {
                    Destroy(ef[j].gameObject);
                }

                if (ef[j].gameObject.GetComponent<EnemyBossBehaviour>() != null)
                {
                    Destroy(ef[j].gameObject);
                }
            }
        }
        else if (type == 1)
        {
            for (int j = 0; j < ef.Length; j++)
            {
                if (ef[j].gameObject.GetComponent<EnemyAdvancedBehaviour>() == null)
                {
                    Destroy(ef[j].gameObject);
                }

                if (ef[j].gameObject.GetComponent<EnemyBossBehaviour>() != null)
                {
                    Destroy(ef[j].gameObject);
                }
            }
        }
        else if (type == 2)
        {
            for (int j = 0; j < ef.Length; j++)
            {
                if (ef[j].gameObject.GetComponent<EnemyBossBehaviour>() == null)
                {
                    Destroy(ef[j].gameObject);
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		if (timer > 0)
        {
            timer = timer - Time.deltaTime;
        }
	}

    public void AddTotalXP(int xp)
    {
        total_xp = xp;
    }

    public void UpdateOrderList()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        enemyOrderList.Clear();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyOrderList.Add(enemies[i].GetComponent<EnemyFight>());
            if (i == enemies.Length-cont)
                id_mutex = enemies[i].GetComponent<EnemyFight>();
        }
    }

    public bool CheckEnemyAttack_Mutex(EnemyFight ef)
    {
        UpdateOrderList();
        //Debug.Log("ef.GetInstanceID(): " + ef + " id_mutex:" + id_mutex);
        if (timer <= 0 && (ef == id_mutex && enemies.Length>1) || enemies.Length == 1 && timer <= 0)
        {
            cont++;
            if (enemies.Length < cont)
                cont = 1;

            timer = 3.8f;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckWin()
    {
        Debug.Log("CheckWin");
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (enemies != null)
        {
            if (enemies.Length <= 1)
            {
                Winner();
                return true;
            }
        }
        else
        {
            Winner();
            return true;
        }
        return false;
    }

    private void Winner()
    {
        gameWin = true;
        audioSource.PlayOneShot(winSound);
        int lvl = GameManager.Instance.CurrentPlayer.Lvl;
        GameManager.Instance.CurrentPlayer.AddXp(total_xp);
        if (lvl != GameManager.Instance.CurrentPlayer.Lvl)
        {
            lvl = GameManager.Instance.CurrentPlayer.Lvl;
            GameManager.Instance.AddNewEquipment(eq);
            WindowAlert.Instance.SetNextWindow();
            WindowAlert.Instance.SetWeaponInfo(Resources.Load<Sprite>(eq.GetEquipmentQualityRoute()), eq.GetTotalPower());
            WindowAlert.Instance.CreateConfirmWindow("LEVEL UP! LEVEL: " + lvl + "\n You earn a new equipment!", true, EnemyFightManager.Instance.Winner2);

        }
        else
        {
            Winner2();
        }

    }

    public void Winner2()
    {
        GameManager.Instance.CurrentPlayer.gameObject.GetComponent<Weapon>().DecreaseWeaponDurability(7);
        //WindowAlert.Instance.SetActiveAlert(); ENEMIGOS DERROTADOS, pulsa OK para volver al mapa
        WindowAlert window = WindowAlert.Instance;
        window.CreateConfirmWindow("GOOD JOB! ENEMIES DEFEATED!, press OK to return the map" + System.Environment.NewLine + "EARNED XP: " + total_xp* GameManager.Instance.CurrentPlayer.Xp_Multiplier + System.Environment.NewLine + "Weapon durability: " +
            GameManager.Instance.CurrentPlayer.gameObject.GetComponent<Weapon>().GetWeaponDurability(), false, null, FightSceneManager.Instance.ChangeScene); //HAS SIDO DERROTADO, pulsa OK para volver al mapa
        window.SetActiveAlert();
    }

    public void GameOver()
    {
        if (gameOver == false)
        {
            gameOver = true;
            audioSource.PlayOneShot(loseSound);

            enemies = GameObject.FindGameObjectsWithTag("enemy");
            GameManager.Instance.CurrentPlayer.gameObject.GetComponent<Weapon>().DecreaseWeaponDurability(7);
            WindowAlert window = WindowAlert.Instance;
            window.CreateConfirmWindow("HAS SIDO DERROTADO, pulsa OK para volver al mapa " + System.Environment.NewLine + "Weapon durability: " +
                GameManager.Instance.CurrentPlayer.gameObject.GetComponent<Weapon>().GetWeaponDurability(), false, null,FightSceneManager.Instance.ChangeScene); //HAS SIDO DERROTADO, pulsa OK para volver al mapa
            window.SetActiveAlert();
        }
    }

    public int GetTotalXP()
    {
        return total_xp;
    }

    public int GetNumEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        return enemies.Length;
    }

    public bool GetWin()
    {
        return gameWin;
    }
}
