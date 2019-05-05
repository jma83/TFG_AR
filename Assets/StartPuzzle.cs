using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartPuzzle : MapEntity {

    private bool active;
    private float timer;

	// Use this for initialization
	void Start () {
        active = true;

    }
	
	// Update is called once per frame
	void Update () {
		if (Time.realtimeSinceStartup > 180)
        {
            active = true;
        }
	}

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (captureRange && active)
            {
                if (GameManager.Instance.CurrentPlayer.Hp > 0)
                {
                    ARGameSceneManager[] managers = FindObjectsOfType<ARGameSceneManager>();

                    foreach (ARGameSceneManager scenemanager1 in managers)
                    {
                        if (scenemanager1.gameObject.activeSelf)
                        {
                            active = false;
                            scenemanager1.ChangeScene(null, 2);

                            //SceneManager.LoadScene("FightScene");
                        }
                    }
                }
                else
                {
                    WindowAlert.Instance.CreateConfirmWindow("Necesitas energia para realizar este puzzle", true);
                }
            }
        }
    }
}
