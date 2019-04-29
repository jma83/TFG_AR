﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] private Text hpText;
    [SerializeField] private Image currentHPBar;
    [SerializeField] private Image XPBar;
    [SerializeField] private Text xpText;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject menu;
    private Button[] menu_sections;
    [SerializeField] private AudioClip menuButtonSound;
    [SerializeField] private AudioClip mainMenuButtonSound;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject slotContainer;
    [SerializeField] private GameObject itemDetail;
    [SerializeField] private GameObject options;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Toggle armas;
    [SerializeField] private Toggle objetos;
    private InventoryUI InvUI;
    private Inventory Inv;
    private ItemDetail itemDt;

    //private int switchInventory = 0;
    private AudioSource audioSource;
    //private int menuSectionCont=3;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Inv = Inventory.Instance;
        inventory.SetActive(true);
        InvUI = slotContainer.GetComponent<InventoryUI>();
        InvUI.Start();
        inventory.SetActive(false);
        itemDetail.SetActive(true);

        itemDt = itemDetail.GetComponent<ItemDetail>();
        itemDetail.SetActive(false);


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
        if (Inv.info) toggleItemDetail(Inv.id_info);
    }

    public void updateLevel() {
        if (GameManager.Instance.CurrentPlayer!=null && levelText!=null)
        levelText.text = GameManager.Instance.CurrentPlayer.Lvl.ToString();
    }

    public void updateXP()
    {
        if (GameManager.Instance.CurrentPlayer != null && xpText != null)
        {
            xpText.text = GameManager.Instance.CurrentPlayer.Xp + " / " + GameManager.Instance.CurrentPlayer.RequiredXp;
            float xp_percent = GameManager.Instance.CurrentPlayer.Xp * 100 / GameManager.Instance.CurrentPlayer.RequiredXp;
            xp_percent = xp_percent / 100;
            XPBar.transform.localScale = new Vector3(xp_percent, 0.7f, 1);

        }

    }
    public void updateHP()
    {
        if (GameManager.Instance.CurrentPlayer != null && hpText != null && currentHPBar != null)
        {
            float hp_value = GameManager.Instance.CurrentPlayer.Hp;
            float f = hp_value / 100;
            hpText.text = "HP: " + hp_value.ToString() + "%";
            currentHPBar.transform.localScale = new Vector3(f, 1, 1);
        }
    }

    public void MenuButtonClick(int a)
    {

        if (a == 0)
        {
            toggleMenu();
        }
        else
        {
            audioSource.PlayOneShot(menuButtonSound);
            toggleInventory();
        }
    }

    private void toggleMenu()
    {
        if (!menu.activeSelf)
        audioSource.PlayOneShot(mainMenuButtonSound);
        menu.SetActive(!menu.activeSelf);
    }
    private void toggleInventory()
    {
        audioSource.PlayOneShot(menuButtonSound);
        inventory.SetActive(!inventory.activeSelf);
        if (objetos.isOn)
            InvUI.SwitchUI(2);
        else
            InvUI.SwitchUI(0);


    }
    public void toggleItemDetail(int id)
    {
        audioSource.PlayOneShot(menuButtonSound);
        itemDetail.SetActive(!itemDetail.activeSelf);
        toggleInventory();
        //GameObject[] gameObj = GameObject.FindGameObjectsWithTag("item");
        if (itemDetail.activeSelf)
        {
            if (!objetos.isOn)
            {
                Equipment[] e = FindObjectsOfType<Equipment>();
                for (int k = 0; k < e.Length; k++)
                {
                    if (e[k].GetInstanceID() == id)
                    {                        
                        itemDt.SetEquipment(e[k]);
                        break;
                    }
                }

            }
            else
            {
                Item[] i = FindObjectsOfType<Item>();
                for (int k = 0; k < i.Length; k++)
                {
                    if (i[k].GetInstanceID() == id)
                    {
                        itemDt.SetItem(i[k]);
                        break;
                    }
                }

            }
        }

        Inv.info = false;


    }

    public void toggleOptions()
    {
        audioSource.PlayOneShot(menuButtonSound);
        options.SetActive(!options.activeSelf);
    }

    public void toggleExitWindow()
    {
        audioSource.PlayOneShot(menuButtonSound);
        WindowAlert.Instance.CreateSelectWindow("Do you want to quit the game?",true, GameManager.Instance.ExitGame,null);
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
            objetos.GetComponentInChildren<Image>().enabled = false;
            armas.GetComponentInChildren<Image>().enabled = true;

        }

        InvUI.SwitchUI(b);
    }

     
}
