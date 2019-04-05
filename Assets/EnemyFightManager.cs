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

    // Use this for initialization
    void Start () {        
        enemyOrderList = new List<EnemyFight>();
        cont = 1;
        UpdateOrderList();
        gameOver = false;
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
        if (timer <= 0 && (ef == id_mutex && enemies.Length>1) || enemies.Length == 1)
        {
            cont++;
            if (enemies.Length < cont)
                cont = 1;
             
            timer = 2.0f;
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
        GameManager.Instance.CurrentPlayer.AddXp(total_xp);
        GameManager.Instance.CurrentPlayer.gameObject.GetComponent<Weapon>().DecreaseWeaponDurability(7);
        //WindowAlert.Instance.SetActiveAlert(); ENEMIGOS DERROTADOS, pulsa OK para volver al mapa
        WindowAlert window = WindowAlert.Instance;
        window.CreateConfirmWindow("BIEN HECHO! ENEMIGOS DERROTADOS, pulsa OK para volver al mapa" + System.Environment.NewLine + "XP: " + total_xp + " Weapon durability: " +
            GameManager.Instance.CurrentPlayer.gameObject.GetComponent<Weapon>().GetWeaponDurability(), false, FightSceneManager.Instance.ChangeScene); //HAS SIDO DERROTADO, pulsa OK para volver al mapa
        window.SetActiveAlert();
    }

    public void GameOver()
    {
        if (gameOver == false)
        {
            gameOver = true;
            enemies = GameObject.FindGameObjectsWithTag("enemy");
            GameManager.Instance.CurrentPlayer.gameObject.GetComponent<Weapon>().DecreaseWeaponDurability(7);
            WindowAlert window = WindowAlert.Instance;
            window.CreateConfirmWindow("HAS SIDO DERROTADO, pulsa OK para volver al mapa " + System.Environment.NewLine + "Weapon durability: " +
                GameManager.Instance.CurrentPlayer.gameObject.GetComponent<Weapon>().GetWeaponDurability(), false, FightSceneManager.Instance.ChangeScene); //HAS SIDO DERROTADO, pulsa OK para volver al mapa
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
}
