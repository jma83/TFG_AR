using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private Player currentPlayer;
    private string playerFile = "/playerInfo.dat";
    private string inventoryFile = "/inventoryInfo.dat";
    PlayerData dataPly;
    InventoryData dataInv;
    Inventory inv;

    private void Start()
    {
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        dataPly = new PlayerData();
        dataInv = new InventoryData();
        inv = Inventory.Instance;
        
        Load();
    }
    public Player CurrentPlayer
    {       
        get { FindPlayer(); return currentPlayer; }
    }

    private void FindPlayer()
    {
        if (currentPlayer == null)
        {
            currentPlayer = FindObjectOfType<Player>();
        }

        //Assert.IsNotNull(currentPlayer);

    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + playerFile);

        dataPly.xp = currentPlayer.Xp;
        dataPly.requiredXp = currentPlayer.RequiredXp;
        dataPly.levelBase = currentPlayer.LevelBase;
        dataPly.hp = currentPlayer.Hp;
        dataPly.maxHp = currentPlayer.MaxHp;
        dataPly.captureRange = currentPlayer.CaptureRange;
        dataPly.xp_multiplier = currentPlayer.Xp_Multiplier;

        bf.Serialize(file, dataPly);
        file.Close();

        FileStream file2 = File.Create(Application.persistentDataPath + inventoryFile);

        /*dataInv.items = inv.getItems();
        dataInv.equipment = inv.getEquipments();
        dataInv.e_selected = inv.GetCurrentEquipment();
        dataInv.space = inv.getEquipments().Count;*/
        int size = inv.getItems().Count;
        dataInv.itemIDs = new int[size];
        dataInv.itemRand = new int[size];
        dataInv.itemActive = new bool[size];
        dataInv.itemType = new int[size];

        for (int i=0;i< inv.getItems().Count; i++)
        {
            dataInv.itemIDs[i] = inv.getItems()[i].GetID();
            dataInv.itemRand[i] = inv.getItems()[i].GetRand();
            dataInv.itemActive[i] = inv.getItems()[i].GetActive();
            dataInv.itemType[i] = inv.getItems()[i].GetItemType();
        }


        size = inv.getEquipments().Count;
        dataInv.equipIDs = new int[size];
        dataInv.equipQuality = new int[size];
        dataInv.equipType = new int[size];
        dataInv.equipAttack = new int[size];
        dataInv.equipDefense = new int[size];
        dataInv.equipSpeed = new int[size];
        dataInv.equipDurability = new int[size];
        dataInv.equipActive = new bool[size];

        for (int i = 0; i < inv.getEquipments().Count; i++)
        {
            dataInv.equipIDs[i] = inv.getEquipments()[i].GetID();
            dataInv.equipQuality[i] = (int)inv.getEquipments()[i].GetEquipmentQualityNum();
            dataInv.equipType[i] = (int)inv.getEquipments()[i].GetEquipmentTypeNum();
            dataInv.equipAttack[i] = inv.getEquipments()[i].GetAttack();
            dataInv.equipDefense[i] = inv.getEquipments()[i].GetDefense();
            dataInv.equipSpeed[i] = inv.getEquipments()[i].GetSpeed();
            dataInv.equipDurability[i] = inv.getEquipments()[i].GetDurability();
            dataInv.equipActive[i] = inv.getEquipments()[i].GetActive();
        }

        dataInv.modified = inv.modified;
        dataInv.space = 26;
        dataInv.id_e_selected = inv.GetCurrentEquipmentID();

        bf.Serialize(file2, dataInv);
        file2.Close();

    }
    public void Load()
    {
        // Debug.Log(Application.persistentDataPath);
        if (File.Exists(Application.persistentDataPath + playerFile))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file1 = File.Open(Application.persistentDataPath + playerFile, FileMode.Open);
            dataPly = (PlayerData)bf.Deserialize(file1);
            file1.Close();

            currentPlayer.Xp = dataPly.xp;
            currentPlayer.RequiredXp = dataPly.requiredXp;
            currentPlayer.LevelBase = dataPly.levelBase;
            currentPlayer.Hp = dataPly.hp;
            currentPlayer.MaxHp = dataPly.maxHp;
            currentPlayer.CaptureRange = dataPly.captureRange;
            currentPlayer.Xp_Multiplier = dataPly.xp_multiplier;

            FileStream file2 = File.Open(Application.persistentDataPath + inventoryFile, FileMode.Open);
            dataInv = (InventoryData)bf.Deserialize(file2);
            file2.Close();

            /*Debug.Log(dataInv.items);
            inv.SetItems(dataInv.items);
            Debug.Log(dataInv.equipment);
            inv.SetEquipments(dataInv.equipment);
            Debug.Log(dataInv.e_selected);
            inv.SelectEquipment(dataInv.e_selected);
            inv.SetSpace(dataInv.space);
            Debug.Log(dataInv.space);
            Debug.Log(dataInv.equipment.Count);*/

            for (int i = 0; i < inv.getItems().Count; i++)
            {
                //crear objeto instancia de objeto (a partir de prefab o algo asi)
                dataInv.itemIDs[i] = inv.getItems()[i].GetID();
                dataInv.itemRand[i] = inv.getItems()[i].GetRand();
                dataInv.itemActive[i] = inv.getItems()[i].GetActive();
                dataInv.itemType[i] = inv.getItems()[i].GetItemType();
            }

            for (int i = 0; i < inv.getEquipments().Count; i++)
            {
                //crear objeto instancia de equipamiento (a partir de prefab o algo asi)
                inv.getEquipments()[i].SetID(dataInv.equipIDs[i]);
                inv.getEquipments()[i].SetQualityNum(dataInv.equipQuality[i]);
                inv.getEquipments()[i].SetTypeNum(dataInv.equipType[i]);
                inv.getEquipments()[i].SetAttack(dataInv.equipAttack[i]);
                inv.getEquipments()[i].SetDefense(dataInv.equipDefense[i]);
                inv.getEquipments()[i].SetSpeed(dataInv.equipSpeed[i]);
                inv.getEquipments()[i].SetDurability(dataInv.equipDurability[i]);
                inv.getEquipments()[i].SetActive(dataInv.equipActive[i]);
            }

            inv.modified=true;
            inv.SetSpace(dataInv.space);
            inv.SelectEquipmentID(dataInv.id_e_selected);
        }
    }
}



[Serializable]
class PlayerData
{
    public int xp;
    public int requiredXp;
    public int levelBase;
    public int lvl;
    public int total_xp;
    public int hp;
    public int maxHp;
    public float captureRange;
    public int xp_multiplier;
}

[Serializable]
class InventoryData
{
    //public List<Item> items;
    //public List<Equipment> equipment;
    public int[] itemIDs;
    public int[] itemRand;
    public bool[] itemActive;
    public int[] itemType;

    public int[] equipIDs;
    public int[] equipQuality;
    public int[] equipType;
    public int[] equipAttack;
    public int[] equipDefense;
    public int[] equipSpeed;
    public int[] equipDurability;
    public bool[] equipActive;

    public int id_e_selected;
    public int space;
    public bool modified;
}

