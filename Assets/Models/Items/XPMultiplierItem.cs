using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/XPMultiplierItem")]

public class XPMultiplierItem : Item {

    [SerializeField] private float defaultTime;
    private float targetTime;
    private bool active = false;

    // Use this for initialization
    public void Start () {
        name = "XP Multiplier Item";
        defaultTime = 7;
    }
    public override void Update()
    {
        if (active)
        {
            if (targetTime > 0)
            {
                targetTime -= Time.deltaTime;
            }
            else
            {
                RestoreAction();
                active = false;

            }
            //Debug.Log("Multipl XP. Target: " + targetTime + "/" + defaultTime);

        }
    }

    public override void RestoreAction()
    {
        GameManager.Instance.CurrentPlayer.SetXpMultiplier(1);
    }

    public override void Use()
    {
        GameManager.Instance.CurrentPlayer.SetXpMultiplier(2);
        targetTime = defaultTime;
        active = true;
        ItemsManager.Instance.AddItem(this);
    }

    public override bool GetActive()
    {
        return active;
    }
}
