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

        if (start.transform.position.x != v1.x)
        {
            

        }
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
                v1 = new Vector3(-0.271f, 0.073f, 0.368f);
                v2 = new Vector3(-0.094f, 0.08f, 0.409f);
                break;
            case 2:
                v1 = new Vector3(0.132f, 0.073f, 0.22f);
                v2 = new Vector3(-0.036f, 0.08f, 0.409f);
                break;
            case 3:
                v1 = new Vector3(0.43f, 0.073f, 0.1194f);
                v2 = new Vector3(-0.42f, 0.08f, -0.448f);
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
