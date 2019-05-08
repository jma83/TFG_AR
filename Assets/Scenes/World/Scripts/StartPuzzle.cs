﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartPuzzle : MapEntity {

    [SerializeField] private bool active;
    [SerializeField] private string time;
    private bool selected;
    private string location_text=null;

	// Use this for initialization
	void Start () {
        active = true;
        selected = false;
        player = GameManager.Instance.CurrentPlayer;

    }

    // Update is called once per frame
    public override void Update2 () {
        if (time != null && time != "")
            if (System.DateTime.Now < System.DateTime.Parse(time).AddMinutes(1))
            {
                active = true;  //debug
                Debug.Log("hpola");
            }
        
        //if (location_text == null && Time.realtimeSinceStartup > 2)
            //FindLocationInfo();

        CalculatePlayerDistance();

    }

    



    private void OnMouseDown()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
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
                            Debug.Log("location_text: " + location_text);
                            active = false;
                            selected = true;
                            time = System.DateTime.Now.ToString();
                            scenemanager1.ChangeScene(null, 2);
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

    public void SetLocationInfo(string str)
    {
        location_text = str;
    }

    public void SetTime(string t)
    {
        time = t;
    }
    public void SetActive(bool b)
    {
        active = b;
    }

    public string GetLocationInfo()
    {
        return location_text;
    }

    public string GetTime()
    {
        return time;
    }

    public bool GetActivePuzzle()
    {
        return active;
    }
    public bool GetSelectedPuzzle()
    {
        return selected;
    }
}