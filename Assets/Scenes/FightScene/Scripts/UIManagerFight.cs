using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerFight : MonoBehaviour {

    [SerializeField] private Text hpText;
    [SerializeField] private Image currentHPBar;
    [SerializeField] private GameObject reloadWeapon;
    [SerializeField] private GameObject reloadShield;
    [SerializeField] private GameObject playerScreen;
    [SerializeField] private Image weaponQualityIcon;
    [SerializeField] private Text weaponTypeText;
    [SerializeField] private Text weaponStatsText;
    private string weaponQuality;    
    private Player ply;
    private PlayerFight plyFight;

    // Use this for initialization
    void Start () {
        ply = GameManager.Instance.CurrentPlayer;
        plyFight=ply.gameObject.GetComponent<PlayerFight>();
        weaponQuality=ply.gameObject.GetComponent<Weapon>().GetWeaponQuality();

        weaponQualityIcon.sprite = Resources.Load<Sprite>(weaponQuality);
        weaponStatsText.text = ply.gameObject.GetComponent<Weapon>().GetWeaponStats().ToString();
        weaponTypeText.text = ply.gameObject.GetComponent<Weapon>().GetWeaponType().ToString();


    }
	
	// Update is called once per frame
	void Update () {
        UpdatePlayerHP();

        GameManager.Instance.CurrentPlayer.gameObject.GetComponent<PlayerFight>();
        if (plyFight.GetHit())
            StartCoroutine(UpdatePlayerScreen(true));

        if (weaponStatsText.text=="0")
        {
            weaponStatsText.text = ply.gameObject.GetComponent<Weapon>().GetWeaponStats().ToString();
            weaponTypeText.text = ply.gameObject.GetComponent<Weapon>().GetWeaponType().ToString();
        }
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

    private IEnumerator UpdatePlayerScreen(bool b)
    {

        playerScreen.SetActive(b);
        yield return new WaitForSeconds(0.5f);
        plyFight.SetHit(!b);
        playerScreen.SetActive(!b);
        yield return null;
    }
}
