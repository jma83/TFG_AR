using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetail : MonoBehaviour {

    [SerializeField] GameObject fullcurrentHPBar;
    [SerializeField] GameObject currentHPBar;
    [SerializeField] Text titleText;
    [SerializeField] Text descText;
    [SerializeField] Text itemBasicText;
    [SerializeField] Text hpText;
    [SerializeField] Text qualityText;
    [SerializeField] Image itemIcon;
    [SerializeField] Image qualityIcon;
    [SerializeField] Text itemTypeText;
    [SerializeField] Text equipTypeText;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetEquipment(Equipment e)
    {
        itemBasicText.text = "- Attack: " + e.GetAttack() + "\n" +
            "- Defense: " + e.GetDefense() + "\n" +
            "- Speed: " + e.GetSpeed() + "\n" +
            "- Total Power: " + e.GetTotalPower() + "\n";

        fullcurrentHPBar.SetActive(true);
        itemTypeText.gameObject.SetActive(false);
        qualityText.text = e.GetEquipmentQuality();
        updateHP(e.GetDurability());
        itemIcon.sprite = e.icon;
        descText.text = e.GetDescription();
        qualityIcon.sprite = Resources.Load<Sprite>(e.GetEquipmentQualityRoute());
        equipTypeText.text = "-Type: " + e.GetEquipmentType();
    }

    public void SetItem(Item i)
    {
        itemIcon.sprite = i.icon;
        itemTypeText.text = i.GetItemTypeName() + "\n";
        itemTypeText.gameObject.SetActive(true);
        fullcurrentHPBar.SetActive(false);
        equipTypeText.text = "";
        if (i.GetRand() != 0)
        {            
            qualityText.text = "Instant Use";
            qualityIcon.sprite = Resources.Load<Sprite>("GUI/timer_off");
            itemBasicText.text = "\nRestores: \n" + i.GetRand().ToString() + "%";
            if (i.GetItemType() == (int)ARGameConstants.TypeObj.Health || i.GetItemType() == (int)ARGameConstants.TypeObj.BigHealth)
                itemBasicText.text += " Energy";
            else
                itemBasicText.text += " Durability";
        }
        else
        {
            qualityText.text = "In Use during certain time";
            itemBasicText.text = "\nActive time: \n" + i.GetDefaultTime().ToString() + " seconds";

            qualityIcon.sprite = Resources.Load<Sprite>("GUI/timer");

        }
        descText.text = i.GetDescription();
    }

    public void updateHP(int hp)
    {
        if (hpText != null && currentHPBar != null)
        {
            float hp_value = hp;
            float f = hp_value / 100;
            hpText.text = "HP: " + hp_value.ToString() + "%";
            currentHPBar.transform.localScale = new Vector3(f, 1, 1);
        }
    }
}
