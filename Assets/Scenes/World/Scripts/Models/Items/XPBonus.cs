using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBonus : ItemPickup {

    private int bonus = 10;
    Renderer rend;
    bool checkRender = true;
    MoveAndRotate m;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        m = GetComponent<MoveAndRotate>();
        gameObject.name = "XpBonus";
    }

    // Update is called once per frame
    public override void Update3()
    {
        if (!checkRender) { 
            Destroy(this.gameObject);
        }
    }

    protected override void PickUPAction()
    {
        if (captureRange && this.enabled==true)
        {
            GameManager.Instance.CurrentPlayer.AddXp(bonus);
            MeshRenderer[] aux = gameObject.GetComponentsInChildren<MeshRenderer>();

            for (int i = 0; i < aux.Length; i++)
            {
                aux[i].enabled = false;
            }
            gameObject.GetComponent<BoxCollider>().enabled = false;
            checkRender = false;
        }
    }
}
