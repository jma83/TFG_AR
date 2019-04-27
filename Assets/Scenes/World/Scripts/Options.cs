using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    [SerializeField] private Slider soundSlider;
    [SerializeField] private Text soundLevel;
    [SerializeField] private Text optionsTittle;
    [SerializeField] private Text howToPlayText;
    [SerializeField] private Dropdown howToPlayDropDown;


    [SerializeField] private GameObject howToPlay;
    [SerializeField] private GameObject aumentedReality;
    [SerializeField] private GameObject mainOptions;


    // Use this for initialization
    void Start () {
        soundLevel.text = soundSlider.value.ToString();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void UpdateVolume()
    {
        soundLevel.text = soundSlider.value.ToString();
        AudioSource[] audio = FindObjectsOfType<AudioSource>();

        for (int i = 0; i < audio.Length; i++)
        {
            audio[i].volume = soundSlider.value/100;
        }
    }
    public void ChangeSection(int i)
    {
        if (i == 1)
        {
            howToPlay.SetActive(false);
            aumentedReality.SetActive(true);
            mainOptions.SetActive(false);
            ChangeTittle("Aumented Reality");
        }
        else if (i == 2)
        {
            howToPlay.SetActive(true);
            aumentedReality.SetActive(false);
            mainOptions.SetActive(false);
            ChangeTittle("How to play");
        }
        else
        {
            howToPlay.SetActive(false);
            aumentedReality.SetActive(false);
            mainOptions.SetActive(true);
            ChangeTittle("Options");
        }
    }

    public void ClickLink()
    {
        Application.OpenURL("https://bit.ly/2V312A1");
    }

    public void CheckHowToPlay_Option()
    {
        switch (howToPlayDropDown.value)
        {
            case 0:
                howToPlayText.text = "The objective of the game is to eliminate the droids that are located all around the world. With the localization active, you can fight and beat them with the aumented reality system.";
                howToPlayText.text += "\n\nWhen you defeat an enemy you'll get experience to level up. The level, moreover than representing the progression in the game, increases the probability of getting better equipment.";
                howToPlayText.text += "\n\nThese weapons will improve your fight skills by equiping them(check details of every weapon in the inventory section). In this way you can beat enemies easily.";
                howToPlayText.text += "\n\nAlso, when you reach higher levels, the enemies will be more dangerous, till you reach level 30, where the final boss will apear. If you defeat it, you will beat the game, but there will be still more enemies to defeat.";
                break;
            case 1:
                howToPlayText.text = "The inventory is the place where every picked object is stored. It is separated in 2 sections: Equipment and items. Both of them have in common the 27 availiable slots for each one.";
                howToPlayText.text += "\n\nOn the one hand, the equipment stores every kind of weapon obtained in the field or during a battle. Here you can do 3 actions: ";
                howToPlayText.text += "\n- Equip: tapping over it.";
                howToPlayText.text += "\n- Delete: selecting the rubbish and then the desired weapon.";
                howToPlayText.text += "\n- View info: selecting the info icon and the the desired weapon.";
                howToPlayText.text += "\n\nEvery equipment is unique because its stats are generated randomly.";
                howToPlayText.text += "\n\nOn the other side, we've the items. They are obtained only in the field. There are 5 types of objects, based on their effect: heal character, repair weapon, increase capture range and multiply experience.";
                howToPlayText.text += "\n\nThe uses are the same than the equipment, but instead of equip you can use items.";
                break;
            case 2:

                howToPlayText.text = "The battle actions are really simple and there are 3 in total:";
                howToPlayText.text += "\n\n- Fire / Attack: There is a button located at the right side of the screen to shoot enemies.The damage and the shooting speed are affected by the weapon equiped.";
                howToPlayText.text += "\n\n- Defend: There is a button in the left side of the screen to defend during certain time.This time you will be immune to any damage.The activation time is affected bu the weapon equiped.";
                howToPlayText.text += "\n\n- Move camera: By rotating the mobile, you can point to the different enemies in order to prevent their attacks and shoot them in time.";
                howToPlayText.text += "\n\nIf you defeat all the enemies before your energy is 0%, you will win. In that case you will earn some XP.But if you loose, you won't earn nothing. In both cases the equipment durability is decreased for the use during the battle..";
             
                break;
            case 3:
                howToPlayText.text = "For the puzzle you can use an image target. Please, check out the \"Aumented reality\" section first.";
                howToPlayText.text += "\n\nThis puzzle consists in a maze. The player must move the ball based in the gravity physics, to the reach a point before the time is over.";
                howToPlayText.text += "\n\nThe puzzle can be solved by using the aumented reality (based in the image target) or not (with the mobile accelerometer).";
                howToPlayText.text += "\n\nIf you use the aumented reality, you should point your mobile camera to the image. Once the maze is generated, you shoul rotate the image surface to move the ball as desired. Otherwise, if the AR is disabled the puzzle will appear in the middle of the screen.";
                howToPlayText.text += "\n\nIf you win you will get some XP. The AR has some extra rewards if you reach your goal. Wheter you loose or win your player energy will be decreased.";
                break;
        }
    }

    public void ChangeTittle(string str)
    {
        optionsTittle.text = str;
    }
}
