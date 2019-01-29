using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBonus : ItemPickup {

    private int bonus = 40;
    [SerializeField] private float rotateSpeed = 50f;
    [SerializeField] private float floatAmplitude = 1f;
    [SerializeField] private float floatFrequency = 1f;

    private Vector3 startPos;   
    Renderer rend;

    void Start()
    {
        startPos = transform.position;
        rend = GetComponent<Renderer>();
        gameObject.name = "XpBonus";
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject != null && this.transform != null && this.enabled)
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
            Vector3 tempPos = startPos;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;
            transform.position = tempPos;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    protected override void PickUPAction()
    {
        if (captureRange)
        {
            GameManager.Instance.CurrentPlayer.AddXp(bonus);
            this.enabled = false;
            rend.enabled = false;
        }
    }
}
