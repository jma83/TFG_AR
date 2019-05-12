using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapEntity : MonoBehaviour {

    protected bool captureRange;
    protected float targetTime;
    protected bool alwaysActiveCapture;
    protected Player player;
    public int check = 0;

    // Use this for initialization
    void Start () {
        captureRange = false;
        alwaysActiveCapture = false;
        targetTime = 0;
        player = GameManager.Instance.CurrentPlayer;
    }

    // Update is called once per frame
    public void Update () {
        if (captureRange)
        {
            if (targetTime > 0)
                targetTime -= Time.deltaTime;
            if (targetTime <= 0)
            {
                targetTime = 0;
                if (alwaysActiveCapture == false)
                    captureRange = false;
            }
        }

        Update2(); 
        
    }

    public virtual void Update2()
    {
        //implments in the children
    }

    public void CalculatePlayerDistance()
    {
        int i = 0;

        if (player == null)
            player = GameManager.Instance.CurrentPlayer;

        if (player!=null)
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 60)
        {
            if (check != 1)
            {
                check = 1;

                if (gameObject.GetComponent<MeshRenderer>() != null)
                {
                    i = 0;
                    while (gameObject.GetComponents<MeshRenderer>().Length > i)
                    {
                        gameObject.GetComponents<MeshRenderer>()[i].enabled = true;
                        i++;
                    }
                }
                else
                {
                    i = 0;
                    if (GetComponentsInChildren<MeshRenderer>().Length > 0 && GetComponentInChildren<MeshRenderer>() != null)
                        while (GetComponentsInChildren<MeshRenderer>().Length > i)
                        {
                            gameObject.GetComponentsInChildren<MeshRenderer>()[i].enabled = true;
                            i++;
                        }
                }
            }
        }
        else if (check!=2)
        {
            check = 2;

            if (gameObject.GetComponent<MeshRenderer>() != null)
            {
                i = 0;
                while (gameObject.GetComponents<MeshRenderer>().Length > i)
                {
                    gameObject.GetComponents<MeshRenderer>()[i].enabled = false;
                    i++;
                }
            }
            else
            {
                i = 0;
                if (GetComponentsInChildren<MeshRenderer>().Length > 0 && GetComponentInChildren<MeshRenderer>() != null)
                    while (GetComponentsInChildren<MeshRenderer>().Length > i)
                    {
                        gameObject.GetComponentsInChildren<MeshRenderer>()[i].enabled = false;
                        i++;
                    }
            }
        }
    }

    public void SetCaptureRange(bool value)
    {
        captureRange = value;
        targetTime = 4.0f;
    }
    public void SetFixedCaptureRange(bool value)
    {
        alwaysActiveCapture = value;
        SetCaptureRange(value);
    }

    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
