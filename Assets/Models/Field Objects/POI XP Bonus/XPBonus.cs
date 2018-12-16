using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBonus : MonoBehaviour {

    private int bonus = 40;

    private void OnMouseDown()
    {
        GameManager.Instance.CurrentPlayer.AddXp(bonus);
        Destroy(gameObject);
    }
}
