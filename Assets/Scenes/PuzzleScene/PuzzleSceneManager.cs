﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleSceneManager : ARGameSceneManager {
    

    // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        GameManager.Instance.InitializeScene();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void ChangeScene(GameObject droid,int i)
    {
        GameManager.Instance.Save();
        List<GameObject> list = new List<GameObject>();
        list.Add(droid);
        list = null;
        SceneChangeManager.Instance.GoToScene(ARGameConstants.SCENE_WORLD, list);
    }

    public override void playerTapped(GameObject player)
    {
        ChangeScene(null, 0);
    }
}
