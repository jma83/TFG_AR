using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] private Text hpText;
    [SerializeField] private Image currentHPBar;
    [SerializeField] private Text xpText;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject menu;
    [SerializeField] private AudioClip menuButtonSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        Assert.IsNotNull(hpText);
        Assert.IsNotNull(xpText);
        Assert.IsNotNull(levelText);
        Assert.IsNotNull(menu);
        Assert.IsNotNull(menuButtonSound); 
    }

    private void Update()
    {
        updateLevel();
        updateXP();
        updateHP();
    }

    public void updateLevel() {
        levelText.text = GameManager.Instance.CurrentPlayer.Lvl.ToString();
    }

    public void updateXP()
    {
        xpText.text = GameManager.Instance.CurrentPlayer.Xp + " / " + GameManager.Instance.CurrentPlayer.RequiredXp;

    }
    public void updateHP()
    {
        float hp_value = GameManager.Instance.CurrentPlayer.Hp;
        float f = hp_value / 100;
        hpText.text = "HP: "+ hp_value.ToString() + "%";
        currentHPBar.transform.localScale = new Vector3(f, 1, 1);
    }

    public void MenuButtonClick()
    {
        audioSource.PlayOneShot(menuButtonSound);
      //  toggleMenu();
    }

    private void toggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }
}
