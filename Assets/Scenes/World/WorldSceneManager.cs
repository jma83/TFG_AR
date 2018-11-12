using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSceneManager : ARGameSceneManager {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void droidTapped(GameObject droid)
    {
            
    }

    public override void playerTapped(GameObject player)
    {
        SceneManager.LoadScene(ARGameConstants.SCENE_COMBAT, LoadSceneMode.Additive);
    }

    
}
