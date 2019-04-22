using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : Singleton<PuzzleManager> {

    private int maxTime;
    private float time;
    private int mapNumber;
    private bool flag;
    private int xp;
    private int random_equip;


    // Use this for initialization
    void Start () {
        maxTime = 4;
        time = maxTime;
        xp = Random.Range(20,50);
        mapNumber = Random.Range(0, 2);
        random_equip = Random.Range(0,10);
        flag = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (time > 0)
        {
            time = time - Time.deltaTime;
        }
        else
        {
            if (WindowAlert.Instance != null) {
                if (PuzzleSceneManager.Instance && flag == false)
                {
                    GameOver();
                    flag = true;
                }
            }
            else {
                Debug.Log("window alert null");
            }
        }
    }

    public void Winner()
    {

        if (random_equip == 5)
        {
            //add equipment 
        }

        GameManager.Instance.CurrentPlayer.AddXp(xp);
        GameManager.Instance.CurrentPlayer.substractHp(10);
        if (random_equip == 5)
            WindowAlert.Instance.CreateConfirmWindow("YOU WIN! YOU EARNED:"+ xp +" XP AND A NEW EQUIPMENT! BUT, YOU LOST 10% OF YOUR ENERGY!, press OK to return the map.", true, null, PuzzleSceneManager.Instance.ChangeScene);
        else
            WindowAlert.Instance.CreateConfirmWindow("YOU WIN! YOU EARNED:" + xp + " XP. BUT, YOU LOST 10% OF YOUR ENERGY!, press OK to return the map.", true, null, PuzzleSceneManager.Instance.ChangeScene);
    }

    public void GameOver()
    {
        GameManager.Instance.CurrentPlayer.substractHp(10);
        WindowAlert.Instance.CreateConfirmWindow("YOU LOSE! TIME IS OVER! YOU LOST 10% OF YOUR ENERGY, press OK to return the map.", true, null, PuzzleSceneManager.Instance.ChangeScene);

    }

    public int GetTime(){

        return (int)time;
    }
}
