using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSceneManager : ARGameSceneManager {


    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        GameManager.Instance.InitializeScene();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ChangeScene(GameObject droid,int i)
    {
        List<GameObject> list = new List<GameObject>();
        /*for (int j=0;j< UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects().Length; j++)
        {
            list.Add(UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[j]);
        }*/
        
        //list.Add(GameManager.Instance.CurrentPlayer.gameObject);
        list.Add(droid);

        GameManager.Instance.Save();
        StartCoroutine(LoadAnimation(list,i));
        
    }

    public override void playerTapped(GameObject player)
    {
        ChangeScene(null, 1);
    }
    private IEnumerator LoadAnimation(List<GameObject> list,int i)
    {
        transtionAnim.enabled = true;
        transtionAnim.SetTrigger("active");
        
        yield return new WaitForSeconds(1.5f);
        //transtionAnim.ResetTrigger("active");
        if (i == 0)
            SceneChangeManager.Instance.GoToScene(ARGameConstants.SCENE_COMBAT, list);
        else
            SceneChangeManager.Instance.GoToScene(ARGameConstants.SCENE_PUZZLE, list);

    
        //transtionAnim.SetTrigger("end");
        //yield return new WaitForSeconds(2f);

    }


}
