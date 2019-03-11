using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSceneManager : ARGameSceneManager {

    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ChangeScene(GameObject droid,int i)
    {
        List<GameObject> list = new List<GameObject>();
        list.Add(droid);

        GameManager.Instance.Save();
        if (i==0)
            SceneChangeManager.Instance.GoToScene(ARGameConstants.SCENE_COMBAT, list);
        else
            SceneChangeManager.Instance.GoToScene(ARGameConstants.SCENE_PUZZLE, list);
    }

    public override void playerTapped(GameObject player)
    {
        ChangeScene(null, 1);
    }

    
}
