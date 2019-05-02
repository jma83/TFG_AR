using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileDetail : MonoBehaviour {

    
    [SerializeField] Image qualityImage;
    [SerializeField] Text user_name;
    [SerializeField] Text equip_type;
    [SerializeField] Text equip_power;
    [SerializeField] Text boss_probability;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void SetInfoParameters()
    {
        if (Inventory.Instance.GetCurrentEquipment() != null)
        {
            qualityImage.sprite = Resources.Load<Sprite>(Inventory.Instance.GetCurrentEquipment().GetEquipmentQualityRoute());
            equip_type.text = "Type: "+Inventory.Instance.GetCurrentEquipment().GetEquipmentType();
            equip_power.text = "Power: " + Inventory.Instance.GetCurrentEquipment().GetTotalPower().ToString();
        }


        user_name.text = GameManager.Instance.CurrentPlayer.GetUserName();
        int level = GameManager.Instance.CurrentPlayer.Lvl;
        if (level % 5 != 0) {
            int aux = 1;
            int aux2 = 5;
            while (level > aux2)
            {
                aux2 = aux * 5;
                aux++;
            }

            boss_probability.text = "Boss probability: 8% (reach level " + aux2 + " for more).";
        }
        else
        {
            boss_probability.text = "Boss probability: 48% (max)";
        }
        //boss_probability
    }
}
