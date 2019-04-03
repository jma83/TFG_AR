using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightManager : Singleton<EnemyFightManager> {

    private GameObject[] enemies;
    private List<EnemyFight> enemyOrderList;
    private float timer;
    private int cont;
    private EnemyFight id_mutex;
    // Use this for initialization
    void Start () {

        
        enemyOrderList = new List<EnemyFight>();
        cont = 1;
        UpdateOrderList();

    }
	
	// Update is called once per frame
	void Update () {
		if (timer > 0)
        {
            timer = timer - Time.deltaTime;
        }
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
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (enemies != null)
            if (enemies.Length <= 0)
            {
                //WindowAlert.Instance.SetActiveAlert(); ENEMIGOS DERROTADOS, pulsa OK para volver al mapa
                WindowAlert window = WindowAlert.Instance;
                window.CreateConfirmWindow("BIEN HECHO! ENEMIGOS DERROTADOS, pulsa OK para volver al mapa", false, FightSceneManager.Instance.ChangeScene); //HAS SIDO DERROTADO, pulsa OK para volver al mapa
                window.SetActiveAlert();
                return true;
            }
        return false;
    }

    public void GameOver()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        WindowAlert window = WindowAlert.Instance;
        window.CreateConfirmWindow("HAS SIDO DERROTADO, pulsa OK para volver al mapa", false, FightSceneManager.Instance.ChangeScene); //HAS SIDO DERROTADO, pulsa OK para volver al mapa
        window.SetActiveAlert();
    }

    public int GetNumEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        return enemies.Length;
    }
}
