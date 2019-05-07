using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEntity : MonoBehaviour {

    protected float defaultTime;
    [SerializeField] protected float targetTime;
    [SerializeField] protected bool active = false;
    protected int id;
    public Sprite icon = null;
    protected bool isDefault = false;
    protected string description;
    public string respawnTime;


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

    public float GetTargetTime()
    {
        return targetTime;
    }

    public string GetDescription()
    {    
        return description;
    }

    public float GetDefaultTime()
    {
        return defaultTime;
    }

    public void DisableComponents()
    {
        gameObject.GetComponent<ItemPickup>().enabled = false;
        if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponentsInChildren<MeshRenderer>()[0].enabled = false;
            gameObject.GetComponentsInChildren<MeshRenderer>()[1].enabled = false;
            gameObject.GetComponentsInChildren<MeshRenderer>()[2].enabled = false;
        }
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void DeleteObject()
    {
        Destroy(gameObject);
    }
}
