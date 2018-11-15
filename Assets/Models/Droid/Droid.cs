﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droid : MonoBehaviour {

    [SerializeField] private float spawnRate = 0.10f;
    //[SerializeField] private float catchRate = 0.10f;
    [SerializeField] private int attack = 0;
    [SerializeField] private int defense = 0;
    [SerializeField] private int hp = 10;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public float SpawnRate
    {
        get { return spawnRate;  }
    }

    /*public float CatchRate
    {
        get { return spawnRate; }
    }*/

    public int Attack
    {
        get { return attack; }
    }

    public float Defense
    {
        get { return defense; }
    }

    public float Hp
    {
        get { return hp; }
    }

    private void OnMouseDown()
    {
        ARGameSceneManager[] managers = FindObjectsOfType<ARGameSceneManager>();
        foreach (ARGameSceneManager scenemanager1 in managers)
        {
            if (scenemanager1.gameObject.activeSelf)
            {
                scenemanager1.droidTapped(this.gameObject);
            }
        }
    }
}