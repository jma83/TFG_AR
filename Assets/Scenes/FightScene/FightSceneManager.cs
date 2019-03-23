using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightSceneManager : ARGameSceneManager {
    

    // Use this for initialization
    void Start () {
        //StartCoroutine(LoadAnimation());
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        GameManager.Instance.InitializeScene();

        Debug.Log(GameManager.Instance.CurrentPlayer.Lvl);
        Debug.Log(GameManager.Instance.CurrentPlayer.Hp);
    }
	
	// Update is called once per frame
	void Update () {
    }

    public override void ChangeScene(GameObject droid,int i)
    {

        List<GameObject> list = new List<GameObject>();
        list.Add(droid);
        list = null;
        SceneChangeManager.Instance.GoToScene(ARGameConstants.SCENE_WORLD, list);
    }

    public override void playerTapped(GameObject player)
    {
        ChangeScene(null, 0);
    }

    private IEnumerator LoadAnimation()
    {
        transtionAnim.SetTrigger("end");
        yield return new WaitForSeconds(2.5f);
        

    }
}
