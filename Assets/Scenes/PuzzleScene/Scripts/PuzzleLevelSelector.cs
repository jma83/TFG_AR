using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLevelSelector : Singleton<PuzzleLevelSelector> {

    private int level;
    [SerializeField] GameObject[] mazes;
    [SerializeField] GameObject sphere;
    [SerializeField] GameObject start;
    [SerializeField] GameObject finish;
    Vector3 v1 = Vector3.zero;
    Vector3 v2 = Vector3.zero;
    bool b = false;

    // Use this for initialization
    void Start () {
        CreateLevel();

    }
	
	// Update is called once per frame
	void Update () {
        if (level > 0 && !b)
            SetObjPosition();

    }

    public void CreateLevel()
    {
        level = Random.Range(0, 4);

        switch (level)
        {
            case 0:
                Destroy(mazes[1]);
                Destroy(mazes[2]);
                Destroy(mazes[3]);
                break;
            case 1:
                Destroy(mazes[0]);
                Destroy(mazes[2]);
                Destroy(mazes[3]);
                break;
            case 2:
                Destroy(mazes[0]);
                Destroy(mazes[1]);
                Destroy(mazes[3]);
                break;
            case 3:
                Destroy(mazes[0]);
                Destroy(mazes[1]);
                Destroy(mazes[2]);
                break;
        }
        mazes[level].SetActive(true);

       

    }

    private void SetObjPosition()
    {

        Debug.Log("level: " +level);
        switch (level)
        {
            case 1:
                v1 = new Vector3(-0.2689f, 0.073f, 0.383f);
                v2 = new Vector3(-0.091f, 0.08f, 0.403f);
                break;
            case 2:
                v1 = new Vector3(0.128f, 0.073f, 0.21f);
                v2 = new Vector3(-0.032f, 0.08f, 0.403f);
                break;
            case 3:
                v1 = new Vector3(0.267f, 0.073f, 0.104f);
                v2 = new Vector3(-0.266f, 0.08f, -0.343f);
                break;
        }
        sphere.transform.position = start.transform.position = v1;
        finish.transform.position = v2;
        b = true;
        start.transform.position = start.transform.position * 10;
        sphere.transform.position = sphere.transform.position * 10;
        finish.transform.position = finish.transform.position * 10;


    }

    public GameObject GetPuzzle()
    {
        return mazes[level];
    }
}
