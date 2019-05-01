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
    private bool AR;
    Equipment eq;
    private AudioSource audioSource;
    private AudioClip loseSound;
    private AudioClip winSound;


    // Use this for initialization
    void Start () {
        maxTime = 40;
        time = maxTime;
        xp = Random.Range(20,50);
        mapNumber = Random.Range(0, 2);
        random_equip = Random.Range(0,10);
        flag = false;

        eq = gameObject.GetComponent<Equipment>();
        audioSource = GetComponent<AudioSource>();
        loseSound = Resources.Load<AudioClip>("Audio/NewAudio/lose");
        winSound = Resources.Load<AudioClip>("Audio/NewAudio/win");

        eq.SetRandomStats();
        AR = true;
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

    public void SetAR(bool b)
    {
        AR = b;
    }

    public void Winner()
    {
        if (!flag)
        {
            flag = true;
            int lvl = GameManager.Instance.CurrentPlayer.Lvl;
            
            if (AR)
            {
                if (random_equip <= 2)
                    GameManager.Instance.AddNewEquipment(eq);
                xp *= 2;
            }
            GameManager.Instance.CurrentPlayer.AddXp(xp);
            if (lvl != GameManager.Instance.CurrentPlayer.Lvl)
            {
                lvl = GameManager.Instance.CurrentPlayer.Lvl;
                WindowAlert.Instance.CreateConfirmWindow("LEVEEEEEL UP!\n Nivel " + lvl, true, PuzzleManager.Instance.Winner2);
            }
            else
            {
                Winner2();
            }
        }
    }

    public void Winner2()
    {
        audioSource.PlayOneShot(winSound);
        GameManager.Instance.CurrentPlayer.substractHp(5);
        if (random_equip <= 2)
            WindowAlert.Instance.CreateConfirmWindow("YOU WIN! YOU EARNED:" + xp + " XP AND A NEW EQUIPMENT! BUT, YOU LOST 5% OF YOUR ENERGY!, press OK to return the map.", false, null, PuzzleSceneManager.Instance.ChangeScene);
        else
            WindowAlert.Instance.CreateConfirmWindow("YOU WIN! YOU EARNED:" + xp + " XP. BUT, YOU LOST 5% OF YOUR ENERGY!, press OK to return the map.", false, null, PuzzleSceneManager.Instance.ChangeScene);
        WindowAlert.Instance.SetActiveAlert();

    }

    public void GameOver()
    {
        flag = true;
        audioSource.PlayOneShot(loseSound);
        GameManager.Instance.CurrentPlayer.substractHp(10);
        WindowAlert.Instance.CreateConfirmWindow("YOU LOSE! TIME IS OVER! YOU LOST 10% OF YOUR ENERGY, press OK to return the map.", true, null, PuzzleSceneManager.Instance.ChangeScene);

    }

    public void SetTime(int t)
    {

       time = t;
    }

    public int GetTime(){

        return (int)time;
    }
}
