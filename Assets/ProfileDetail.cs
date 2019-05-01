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
        //boss_probability
    }
}
