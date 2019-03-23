using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerFight : MonoBehaviour {

    [SerializeField] private Text hpText;
    [SerializeField] private Image currentHPBar;
    [SerializeField] private GameObject reloadWeapon;
    [SerializeField] private GameObject reloadShield;

    // Use this for initialization
    void Start () {
        UpdatePlayerHP();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdatePlayerHP()
    {
        if (GameManager.Instance.CurrentPlayer != null && hpText != null && currentHPBar != null)
        {
            float hp_value = GameManager.Instance.CurrentPlayer.Hp;
            float f = hp_value / 100;
            hpText.text = "HP: " + hp_value.ToString() + "%";
            currentHPBar.transform.localScale = new Vector3(f, 1, 1);
        }
    }
}
