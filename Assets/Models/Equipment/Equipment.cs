using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentQuality : int { Basic=0, Rare=1, Epic=2, Legend=3 };
public enum EquipmentType : int { Defensive=0, Ofensive=1, Balanced=2, Fast=3 };
public class Equipment : InventoryEntity
{
    protected EquipmentQuality quality;
    protected EquipmentType type;
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
        if (id==0 || id==-1)
        id = itemManager.GetNewEquipID();
        if (attack == 0 && defense == 0 && speed == 0 && durability == 0)
        {
            player = GameManager.Instance.CurrentPlayer;
            SetDurability(100);
            SetRandomStats();
        }
    }

    public void SetRandomStats()
    {
        SetType();
        SetQuality();
        SetEquipmentStats();
    }

    private void SetEquipmentStats()
    {
        int[] arr = GetDefaultStatsByQuality();

        if (type == EquipmentType.Balanced)
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

            if (type == EquipmentType.Fast)
            {
                speed = arr[0];              
            }
            else if (type == EquipmentType.Defensive)
            {
                defense = arr[0];
            }
            else if (type == EquipmentType.Ofensive)
            {
                attack = arr[0];
            }
            
        }

    }

    private int[] GetDefaultStatsByQuality()
    {
        int[] arr = new int[3];

        int lvlFactor = player.Lvl * 2;

        if (quality == EquipmentQuality.Legend)
        {
            arr[0] = Random.Range((lvlFactor + 80), (lvlFactor + 100)); //high
            arr[1] = Random.Range((lvlFactor + 60), (lvlFactor + 79));  //medium
            arr[2] = Random.Range((lvlFactor + 40), (lvlFactor + 59));  //low
        }
        else if (quality == EquipmentQuality.Epic)
        {
            arr[0] = Random.Range((lvlFactor + 60), (lvlFactor + 79));  //high
            arr[1] = Random.Range((lvlFactor + 40), (lvlFactor + 59));  //medium
            arr[2] = Random.Range((lvlFactor + 20), (lvlFactor + 39));  //low
        }
        else if (quality == EquipmentQuality.Rare)
        {
            arr[0] = Random.Range((lvlFactor + 40), (lvlFactor + 59));  //high
            arr[1] = Random.Range((lvlFactor + 20), (lvlFactor + 39));  //medium
            arr[2] = Random.Range((lvlFactor + 10), (lvlFactor + 19));  //low
        }
        else if (quality == EquipmentQuality.Basic)
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
            type = EquipmentType.Balanced;
        }
        else if (r >= aux && r <= aux*2)
        {
            type = EquipmentType.Fast;
        }
        else if (r > aux*2 && r < aux*3)
        {
            type = EquipmentType.Defensive;
        }
        else if (r >= aux*3)
        {
            type = EquipmentType.Ofensive;
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
        type = (EquipmentType)t;
    }

    private void SetQuality()
    {
        int r = Random.Range(ARGameConstants.MINRATE, ARGameConstants.MAXRATE);
        if ( (ARGameConstants.MAXRATE - ARGameConstants.RATE3 < bonusRate) ) bonusRate = 0;

        if (r < ARGameConstants.RATE1 + bonusRate)
        {
            quality = EquipmentQuality.Legend;
        }
        else if (r >= ARGameConstants.RATE1 + bonusRate && r <= ARGameConstants.RATE2 + bonusRate)
        {
            quality = EquipmentQuality.Rare;
        }
        else if (r > ARGameConstants.RATE2 + bonusRate && r < ARGameConstants.RATE3 + bonusRate)
        {
            quality = EquipmentQuality.Epic;
        }
        else if (r >= ARGameConstants.RATE3 + bonusRate)
        {
            quality = EquipmentQuality.Basic;
        }
    }

    public void SetQualityNum(int q)
    {
        quality = (EquipmentQuality) q;
       
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
        if (type == EquipmentType.Fast)
        {
            return "Fast";
        }
        else if (type == EquipmentType.Defensive)
        {
            return "Defensive";
        }
        else if (type == EquipmentType.Ofensive)
        {
            return "Ofensive";
        }
        else if (type == EquipmentType.Balanced)
        {
            return "Balanced";
        }

        return null;
    }
    public EquipmentType GetEquipmentTypeNum()
    {
        return type;
    }

    public string GetEquipmentQuality()
    {
        if (quality == EquipmentQuality.Legend)
        {
            return "GUI/legendQuality";
        }
        else if (quality == EquipmentQuality.Epic)
        {
            return "GUI/epicQuality";
        }
        else if (quality == EquipmentQuality.Rare)
        {
            return "GUI/rareQuality";
        }
        else if (quality == EquipmentQuality.Basic)
        {
            return "GUI/basicQuality";
        }
        return null;
    }

    public EquipmentQuality GetEquipmentQualityNum()
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
