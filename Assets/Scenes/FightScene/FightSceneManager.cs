using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSceneManager : ARGameSceneManager {
    

    // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void droidTapped(GameObject droid)
    {
        List<GameObject> list = new List<GameObject>();
        list.Add(droid);
        SceneChangeManager.Instance.GoToScene(ARGameConstants.SCENE_WORLD, list);
    }

    public override void playerTapped(GameObject player)
    {
        
    }
}
