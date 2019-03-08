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
    private string itemManagerFile = "/itemManagerInfo.dat";
    PlayerData dataPly;
    InventoryData dataInv;
    ItemsManagerData dataItemsManager;
    Inventory inv;

    private void Start()
    {
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        dataPly = new PlayerData();
        dataInv = new InventoryData();
        dataItemsManager = new ItemsManagerData();
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
        inv = Inventory.Instance;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + playerFile);

        dataPly.xp = currentPlayer.Xp;
        dataPly.requiredXp = currentPlayer.RequiredXp;
        dataPly.levelBase = currentPlayer.LevelBase;
        dataPly.lvl = currentPlayer.Lvl;
        dataPly.total_xp = currentPlayer.Total_xp;
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
        dataInv.itemsSize = inv.getItems().Count;
        Debug.Log("EQUIPMENT SIZE: " + dataInv.itemsSize);

        dataInv.space = 26;
        if (dataInv.itemIDs == null)
        {
            dataInv.itemIDs = new int[dataInv.space];
            dataInv.itemRand = new int[dataInv.space];
            dataInv.itemActive = new bool[dataInv.space];
            dataInv.itemType = new int[dataInv.space];
        }

        for (int i=0;i< dataInv.itemsSize; i++)
        {
            dataInv.itemIDs[i] = inv.getItems()[i].GetID();
            dataInv.itemRand[i] = inv.getItems()[i].GetRand();
            dataInv.itemActive[i] = inv.getItems()[i].GetActive();
            dataInv.itemType[i] = inv.getItems()[i].GetItemType();
        }


        dataInv.equipmentsSize = inv.getEquipments().Count;
        Debug.Log("EQUIPMENT SIZE: " + dataInv.equipmentsSize);
        if (dataInv.equipIDs == null)
        {
            dataInv.equipIDs = new int[dataInv.space];
            dataInv.equipQuality = new int[dataInv.space];
            dataInv.equipType = new int[dataInv.space];
            dataInv.equipAttack = new int[dataInv.space];
            dataInv.equipDefense = new int[dataInv.space];
            dataInv.equipSpeed = new int[dataInv.space];
            dataInv.equipDurability = new int[dataInv.space];
            dataInv.equipActive = new bool[dataInv.space];
        }
        for (int i = 0; i < dataInv.equipmentsSize; i++)
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

        dataInv.id_e_selected = inv.GetCurrentEquipmentID();

        bf.Serialize(file2, dataInv);
        file2.Close();

        FileStream file3 = File.Create(Application.persistentDataPath + itemManagerFile);

        dataItemsManager.lastIDEquip = ItemsManager.Instance.GetLastEquipID();
        dataItemsManager.lastIDItem = ItemsManager.Instance.GetLastItemID();
        bf.Serialize(file3, dataItemsManager);
        file3.Close();

    }
    public void Load()
    {
        // Debug.Log(Application.persistentDataPath);
        if (File.Exists(Application.persistentDataPath + playerFile))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file3 = File.Open(Application.persistentDataPath + itemManagerFile, FileMode.Open);
            dataItemsManager = (ItemsManagerData)bf.Deserialize(file3);
            file3.Close();

            ItemsManager.Instance.SetLastEquipID(dataItemsManager.lastIDEquip);
            ItemsManager.Instance.SetLastItemID(dataItemsManager.lastIDItem);


            FileStream file1 = File.Open(Application.persistentDataPath + playerFile, FileMode.Open);
            dataPly = (PlayerData)bf.Deserialize(file1);
            file1.Close();

            currentPlayer.Xp = dataPly.xp;
            currentPlayer.RequiredXp = dataPly.requiredXp;
            currentPlayer.LevelBase = dataPly.levelBase;
            currentPlayer.Lvl = dataPly.lvl;
            currentPlayer.Total_xp = dataPly.total_xp;
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
            ItemsManager itemM = ItemsManager.Instance;
            GameObject gmObject = null;
            Item item=null;

            Debug.Log("Items: " + dataInv.itemsSize);
            for (int i = 0; i < dataInv.itemsSize; i++)
            {
                switch (dataInv.itemType[i])
                {
                    case 0:
                        gmObject= Instantiate(Resources.Load("Items/ManaPot", typeof(GameObject))) as GameObject;
                        item = gmObject.GetComponent<HealthItem>();
                        break;
                    case 1:
                        gmObject= Instantiate(Resources.Load("Items/LifePot", typeof(GameObject))) as GameObject;
                        item = gmObject.GetComponent<BigHealthItem>();
                        break;
                    case 2:
                        gmObject= Instantiate(Resources.Load("Items/Shield", typeof(GameObject))) as GameObject;
                        item = gmObject.GetComponent<ExtendCaptureItem>();
                        break;
                    case 3:
                        gmObject= Instantiate(Resources.Load("Items/Key", typeof(GameObject))) as GameObject;
                        item = gmObject.GetComponent<XPMultiplierItem>();
                        break;
                }

                //crear objeto instancia de objeto (a partir de prefab o algo asi)
                item.SetID(dataInv.itemIDs[i]);
                item.SetRandNum(dataInv.itemRand[i]);
                item.SetActive(dataInv.itemActive[i]);
                item.SetType(dataInv.itemType[i]);
                item.DisableComponents();
                inv.SetItem(item, i);
            }

            Equipment equip = null;
            Debug.Log("Equipments: " + dataInv.equipmentsSize);
            for (int i = 0; i < dataInv.equipmentsSize; i++)
            {
                //crear objeto instancia de equipamiento (a partir de prefab o algo asi)
                gmObject = Instantiate(Resources.Load(itemM.GetEquipmentPrefab(), typeof(GameObject))) as GameObject;
                equip = gmObject.GetComponent<Equipment>();

                equip.SetID(dataInv.equipIDs[i]);
                equip.SetQualityNum(dataInv.equipQuality[i]);
                equip.SetTypeNum(dataInv.equipType[i]);
                equip.SetAttack(dataInv.equipAttack[i]);
                equip.SetDefense(dataInv.equipDefense[i]);
                equip.SetSpeed(dataInv.equipSpeed[i]);
                equip.SetDurability(dataInv.equipDurability[i]);
                equip.SetActive(dataInv.equipActive[i]);
                equip.DisableComponents();
                inv.SetEquip(equip,i);
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
    public int itemsSize;

    public int[] equipIDs;
    public int[] equipQuality;
    public int[] equipType;
    public int[] equipAttack;
    public int[] equipDefense;
    public int[] equipSpeed;
    public int[] equipDurability;
    public bool[] equipActive;
    public int equipmentsSize;

    public int id_e_selected;
    public int space;
}

[Serializable]
class ItemsManagerData
{
    public int lastIDItem;
    public int lastIDEquip;
}
