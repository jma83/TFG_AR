using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Equipment : InventoryEntity
{
    protected ARGameConstants.EquipmentQuality quality;
    protected ARGameConstants.EquipmentType type;
    protected int bonusRate = 0;
    protected int attack = 0;
    protected int defense = 0;
    protected int speed = 0; 
    protected int durability = 0;
    protected ItemsManager itemManager;

    private Player player;

    public void Start()
    {
        itemManager = ItemsManager.Instance;
        player = GameManager.Instance.CurrentPlayer;
        
        if (attack == 0 && defense == 0 && speed == 0 && durability == 0)
        {
            if (itemManager!=null)
            id = itemManager.GetNewEquipID();            
            SetDurability(100);

            //Debug.Log("Inicializo arma");
        }
        else
        {
            SetDescription();
            //Debug.Log("Else!!! No inicializamos los stats del arma: "+ id);
        }
    }

    public void SetRandomStats()
    {
        SetType();
        SetQuality();
        SetEquipmentStats();
        SetDescription();

    }

    private void SetEquipmentStats()
    {
        int[] arr = GetDefaultStatsByQuality();

        if (type == ARGameConstants.EquipmentType.Balanced)
        {
            speed = arr[1];
            arr = GetDefaultStatsByQuality();
            defense = arr[1];
            arr = GetDefaultStatsByQuality();
            attack = arr[1];

        }
        else
        {
            int i=Random.Range(0,2);

            if (i == 0)
            {
                defense = arr[1];
                attack = arr[2];
            }
            else
            {
                defense = arr[2];
                attack = arr[1];
            }
            speed = arr[1];
            if (type == ARGameConstants.EquipmentType.Fast)
            {
                speed = arr[0];              
            }
            else if (type == ARGameConstants.EquipmentType.Defensive)
            {
                defense = arr[0];
            }
            else if (type == ARGameConstants.EquipmentType.Ofensive)
            {
                attack = arr[0];
            }
            
        }

    }

    private int[] GetDefaultStatsByQuality()
    {
        int[] arr = new int[3];

        if (player == null)
            player = GameManager.Instance.CurrentPlayer;
        int lvlFactor = player.Lvl * 2;

        if (quality == ARGameConstants.EquipmentQuality.Legend)
        {
            arr[0] = Random.Range((lvlFactor + 80), (lvlFactor + 100)); //high
            arr[1] = Random.Range((lvlFactor + 60), (lvlFactor + 79));  //medium
            arr[2] = Random.Range((lvlFactor + 40), (lvlFactor + 59));  //low
        }
        else if (quality == ARGameConstants.EquipmentQuality.Epic)
        {
            arr[0] = Random.Range((lvlFactor + 60), (lvlFactor + 79));  //high
            arr[1] = Random.Range((lvlFactor + 40), (lvlFactor + 59));  //medium
            arr[2] = Random.Range((lvlFactor + 20), (lvlFactor + 39));  //low
        }
        else if (quality == ARGameConstants.EquipmentQuality.Rare)
        {
            arr[0] = Random.Range((lvlFactor + 40), (lvlFactor + 59));  //high
            arr[1] = Random.Range((lvlFactor + 20), (lvlFactor + 39));  //medium
            arr[2] = Random.Range((lvlFactor + 10), (lvlFactor + 19));  //low
        }
        else if (quality == ARGameConstants.EquipmentQuality.Basic)
        {
            arr[0] = Random.Range((lvlFactor + 20), (lvlFactor + 29));  //high
            arr[1] = Random.Range((lvlFactor + 10), (lvlFactor + 19));  //medium
            arr[2] = Random.Range((lvlFactor + 0), (lvlFactor + 9));    //low
        }


        return arr;
    }

    private void SetType()
    {
        int r = Random.Range(ARGameConstants.MINRATE, ARGameConstants.MAXRATE);
        int aux = ARGameConstants.MAXRATE / 4;
        if (r < aux)     //balanced
        {
            type = ARGameConstants.EquipmentType.Balanced;
        }
        else if (r >= aux && r <= aux*2)
        {
            type = ARGameConstants.EquipmentType.Fast;
        }
        else if (r > aux*2 && r < aux*3)
        {
            type = ARGameConstants.EquipmentType.Defensive;
        }
        else if (r >= aux*3)
        {
            type = ARGameConstants.EquipmentType.Ofensive;
        }
        SetTypeNum((int)type);


    }
    public void SetTypeNum(int t)
    {
        if (t == 0)
        {
            icon = Resources.Load<Sprite>("Equipment/sword-and-shield");
        }
        else if (t == 1)
        {
            icon = Resources.Load<Sprite>("Equipment/laser");
        }
        else if (t == 2)
        {
            icon = Resources.Load<Sprite>("Equipment/armor");
        }
        else if (t == 3)
        {
            icon = Resources.Load<Sprite>("Equipment/sword-icon");
        }
        type = (ARGameConstants.EquipmentType)t;
    }

    private void SetQuality()
    {
        int r = Random.Range(ARGameConstants.MINRATE, ARGameConstants.MAXRATE);
        if ( (ARGameConstants.MAXRATE - ARGameConstants.RATE3 < bonusRate) ) bonusRate = 0;

        if (r < ARGameConstants.RATE1 + bonusRate)
        {
            quality = ARGameConstants.EquipmentQuality.Legend;
        }
        else if (r >= ARGameConstants.RATE1 + bonusRate && r <= ARGameConstants.RATE2 + bonusRate)
        {
            quality = ARGameConstants.EquipmentQuality.Rare;
        }
        else if (r > ARGameConstants.RATE2 + bonusRate && r < ARGameConstants.RATE3 + bonusRate)
        {
            quality = ARGameConstants.EquipmentQuality.Epic;
        }
        else if (r >= ARGameConstants.RATE3 + bonusRate)
        {
            quality = ARGameConstants.EquipmentQuality.Basic;
        }
    }

    public void SetQualityNum(int q)
    {
        quality = (ARGameConstants.EquipmentQuality) q;
       
    }

    public void SetBonusRate(int b)
    {
        bonusRate = b;
    }

    public void SetAttack(int a)
    {
        attack = a;
    }

    public void SetDefense(int d)
    {
        defense = d;
    }

    public void SetSpeed(int s)
    {
        speed = s;
    }

    public void SetDurability(int d)
    {
        durability = d;
        if (durability > 100)
            durability = 100;


    }

    public void SetDescription()
    {
        description = "This equipment, with a ";

        if (quality == ARGameConstants.EquipmentQuality.Legend)
        {
            description += "Legendary quality, has extremely powerful stats, where the ";
        }
        else if (quality == ARGameConstants.EquipmentQuality.Epic)
        {
            description += "Epic quality, has a very powerful stats, where the ";
        }
        else if (quality == ARGameConstants.EquipmentQuality.Rare)
        {
            description += "Rare quality, has a powerful stats, where the";
        }
        else if (quality == ARGameConstants.EquipmentQuality.Basic)
        {
            description += "Basic quality, has a generic stats, where the ";
        }

        if (type == ARGameConstants.EquipmentType.Balanced)
        {
            description += "balance highlights. \n\n This means that the weapon allows defending and attacking in the same measure.";
        }
        else if (type == ARGameConstants.EquipmentType.Fast)
        {
            description += "speed highlights over the rest. \n\n This means that the weapon allows shooting faster in combat than most of the others (with the same quality).";
        }
        else if (type == ARGameConstants.EquipmentType.Defensive)
        {
            description += "defensive one highlights over the rest. \n\n This means that the weapon allows you to protect during more time in combat than others (with the same quality).";
        }
        else if (type == ARGameConstants.EquipmentType.Ofensive)
        {
            description += "ofensive one highlights over the rest. \n\n This means that the weapon allows you to deal more damage during more time in combat than the others (with the same quality), becoming really deadly.";
        }
    }

    public void AddDurability(int d)
    {
        if (durability + d > 100)
            durability = 100;
        else if (d + durability < 0)
            durability = 0;
        else 
            durability = durability + d;
    }

    public void DecreaseDurability(int d)
    {
        if (durability - d > 100)
            durability = 100;
        else if (durability-d < 0)
            durability = 0;
        else
            durability= durability - d;
    }
    public int GetDurability()
    {
        return durability;
    }

    public string GetEquipmentType()
    {
        if (type == ARGameConstants.EquipmentType.Fast)
        {
            return "Fast";
        }
        else if (type == ARGameConstants.EquipmentType.Defensive)
        {
            return "Defensive";
        }
        else if (type == ARGameConstants.EquipmentType.Ofensive)
        {
            return "Ofensive";
        }
        else if (type == ARGameConstants.EquipmentType.Balanced)
        {
            return "Balanced";
        }

        return null;
    }
    public ARGameConstants.EquipmentType GetEquipmentTypeNum()
    {
        return type;
    }

    public string GetEquipmentQuality()
    {
        if (quality == ARGameConstants.EquipmentQuality.Legend)
        {
            return "Legend";
        }
        else if (quality == ARGameConstants.EquipmentQuality.Epic)
        {
            return "Epic";
        }
        else if (quality == ARGameConstants.EquipmentQuality.Rare)
        {
            return "Rare";
        }
        else if (quality == ARGameConstants.EquipmentQuality.Basic)
        {
            return "Basic";
        }
        return null;
    }

    public string GetEquipmentQualityRoute()
    {
        if (quality == ARGameConstants.EquipmentQuality.Legend)
        {
            return "GUI/legendQuality";
        }
        else if (quality == ARGameConstants.EquipmentQuality.Epic)
        {
            return "GUI/epicQuality";
        }
        else if (quality == ARGameConstants.EquipmentQuality.Rare)
        {
            return "GUI/rareQuality";
        }
        else if (quality == ARGameConstants.EquipmentQuality.Basic)
        {
            return "GUI/basicQuality";
        }
        return null;
    }

    public ARGameConstants.EquipmentQuality GetEquipmentQualityNum()
    {
        return quality;
    }

    public int GetTotalPower()
    {
        return (attack + speed + defense);
    }

    public int GetAttack()
    {
        return attack;
    }

    public int GetDefense()
    {
        return defense;
    }

    public int GetSpeed()
    {
        return speed;
    }

}
