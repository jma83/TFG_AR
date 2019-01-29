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
    [SerializeField] private GameObject slotContainer;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Toggle armas;
    [SerializeField] private Toggle objetos;
    private InventoryUI InvUI;
    private Inventory Inv;
    private int switchInventory = 0;
    private AudioSource audioSource;
    private int menuSectionCont=3;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Inv = Inventory.Instance;
        inventory.SetActive(true);
        InvUI = slotContainer.GetComponent<InventoryUI>();
        InvUI.Start();
        inventory.SetActive(false);


    }

    private void Awake()
    {
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
        if (Inv.modified == true && InvUI!=null) InvUI.UpdateUI();
        
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
        objetos.GetComponentInChildren<Image>().enabled = false;
        armas.GetComponentInChildren<Image>().enabled = true;
        InvUI.SwitchUI(0);


    }
    public void toggleCapture()
    {
        GameManager.Instance.CurrentPlayer.captureRangeObj.SetEntitiesCaptureRange(!toggle.isOn);

    }

    public void InventorySectionClick(int b)
    {
        if (b == 2)
        {
            objetos.GetComponentInChildren<Image>().enabled = true;
            armas.GetComponentInChildren<Image>().enabled = false;


        }
        else
        {
            objetos.GetComponentInChildren<Image>().enabled = true;
            armas.GetComponentInChildren<Image>().enabled = false;

        }

        InvUI.SwitchUI(b);
    }

     
}
