using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] private Text hpText;
    [SerializeField] private Image currentHPBar;
    [SerializeField] private Text xpText;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject menu;
    private Button[] menu_sections;
    [SerializeField] private AudioClip menuButtonSound;
    [SerializeField] private GameObject inventory;
    [SerializeField] private Toggle toggle;

    private AudioSource audioSource;
    private int menuSectionCont=3;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(hpText);
        Assert.IsNotNull(xpText);
        Assert.IsNotNull(levelText);
        Assert.IsNotNull(menu);
        Assert.IsNotNull(menuButtonSound);

        menuSectionCont = menu.transform.childCount;
        menu_sections = new Button[menuSectionCont];

        for (int i = 0; i < menuSectionCont; i++)
        {
            menu_sections[i] = menu.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }

    private void Update()
    {
        updateLevel();
        updateXP();
        updateHP();
        //menu_sections[0].onClick.AddListener(toggleInventory);
        //menu_sections[0].onClick.AddListener(toggleMenu);
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

    public void MenuButtonClick(int a)
    {
        
        audioSource.PlayOneShot(menuButtonSound);
        if (a == 0)
            toggleMenu();
        else
            toggleInventory();
    }

    private void toggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }
    private void toggleInventory()
    {

        inventory.SetActive(!inventory.activeSelf);
    }
    public void toggleCapture()
    {
        GameManager.Instance.CurrentPlayer.captureRangeObj.DisbleCaptureRange(!toggle.isOn);

    }

}
