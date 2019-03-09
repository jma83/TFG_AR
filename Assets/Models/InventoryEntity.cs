﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryEntity : MonoBehaviour {

    protected float defaultTime;
    protected float targetTime;
    protected bool active = false;
    protected int id;
    public Sprite icon = null;
    protected bool isDefault = false;


    // Update is called once per frame
    void Update () {
		
	}

    public void SetID(int i)
    {
        id=i;
    }
    public void SetActive(bool b)
    {
        active = b;
    }

    public int GetID()
    {
        return id;
    }

    public bool GetActive()
    {
        return active;
    }

    public void SetTargetTime(float f)
    {
        targetTime=f;
    }

    public float GetTargetTime()
    {
        return targetTime;
    }

    public void DisableComponents()
    {
        gameObject.GetComponent<ItemPickup>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void DeleteObject()
    {
        Destroy(gameObject);
    }
}
