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
    ItemsManager itemsManager;

    private void Start()
    {
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        dataPly = new PlayerData();
        dataInv = new InventoryData();
        dataItemsManager = new ItemsManagerData();
        inv = Inventory.Instance;
        
        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            Save();
        //else
            //Load();

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
        itemsManager = ItemsManager.Instance;

        BinaryFormatter bf = new BinaryFormatter();

        //---------------------------------------------------------------
        // GUARDAMOS EL FICHERO DE LOS DATOS DEL JUGADOR
        //---------------------------------------------------------------

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

        //---------------------------------------------------------------
        // GUARDAMOS EL FICHERO DEL INVENTARIO Y LOS ITEMS QUE CONTIENE
        //---------------------------------------------------------------

        FileStream file2 = File.Create(Application.persistentDataPath + inventoryFile);       
        
        dataInv.itemsSize = inv.getItems().Count;

        dataInv.space = 26; // inv.getItems().Capacity
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

        //---------------------------------------------------------------
        // GUARDAMOS EL FICHERO DEL GESTOR DE ITEMS (IDS Y OBJETOS ACTIVOS)
        //---------------------------------------------------------------

        FileStream file3 = File.Create(Application.persistentDataPath + itemManagerFile);

        dataItemsManager.lastIDEquip = itemsManager.GetLastEquipID();
        dataItemsManager.lastIDItem = itemsManager.GetLastItemID();
        dataItemsManager.itemSize = itemsManager.GetItems().Count;

        dataItemsManager.maxSizeActive = 3; //itemsManager.GetItems().Capacity;  
        Debug.Log("item size save: " + dataItemsManager.itemSize);
        if (dataItemsManager.itemIDs == null)
        {
            dataItemsManager.itemIDs = new int[dataItemsManager.maxSizeActive];
            dataItemsManager.itemRand = new int[dataItemsManager.maxSizeActive];
            dataItemsManager.itemActive = new bool[dataItemsManager.maxSizeActive];
            dataItemsManager.itemType = new int[dataItemsManager.maxSizeActive];
            dataItemsManager.itemTargetTime = new float[dataItemsManager.maxSizeActive];
        }

        for (int i = 0; i < dataItemsManager.itemSize; i++)
        {
            dataItemsManager.itemIDs[i] = itemsManager.GetItems()[i].GetID();
            dataItemsManager.itemRand[i] = itemsManager.GetItems()[i].GetRand();
            dataItemsManager.itemActive[i] = itemsManager.GetItems()[i].GetActive();
            dataItemsManager.itemType[i] = itemsManager.GetItems()[i].GetItemType();
            dataItemsManager.itemTargetTime[i] = itemsManager.GetItems()[i].GetTargetTime();

            Debug.Log("ID save: " + dataItemsManager.itemIDs[i]);
            Debug.Log("Active save: " + dataItemsManager.itemActive[i]);
            Debug.Log("Target save: " + dataItemsManager.itemTargetTime[i]);
        }

        bf.Serialize(file3, dataItemsManager);
        file3.Close();

    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + playerFile))
        {
            itemsManager = ItemsManager.Instance;

            BinaryFormatter bf = new BinaryFormatter();

            

            //---------------------------------------------------------------
            // ABRIMOS EL FICHERO DE LOS DATOS DEL JUGADOR
            //---------------------------------------------------------------

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

            //---------------------------------------------------------------
            // ABRIMOS EL FICHERO DEL GESTOR DE ITEMS (IDS Y OBJETOS ACTIVOS)
            //---------------------------------------------------------------

            FileStream file3 = File.Open(Application.persistentDataPath + itemManagerFile, FileMode.Open);
            dataItemsManager = (ItemsManagerData)bf.Deserialize(file3);
            file3.Close();

            itemsManager.SetLastEquipID(dataItemsManager.lastIDEquip);
            itemsManager.SetLastItemID(dataItemsManager.lastIDItem);
            Item item = null;

            Debug.Log("item size load: " + dataItemsManager.itemSize);

            for (int i = 0; i < dataItemsManager.itemSize; i++)
            {
                item = CreateItemByType(dataItemsManager.itemType[i]);

                //crear instancia de los ITEMS ACTIVOS
                item.SetID(dataItemsManager.itemIDs[i]);
                item.SetRandNum(dataItemsManager.itemRand[i]);
                item.SetTargetTime(dataItemsManager.itemTargetTime[i]);

                //Debug.Log("ID load: " + item.GetID());
                //Debug.Log("Active load: " + dataItemsManager.itemActive[i]);
                //Debug.Log("Target load: " + item.GetTargetTime());
                item.SetActive(dataItemsManager.itemActive[i]);

                item.SetType(dataItemsManager.itemType[i]);
                item.timeStampAux = dataItemsManager.itemTargetTime[i];
                item.DisableComponents();
                if (!itemsManager.AddItem(item)) Debug.Log("algo salio mal!");

            }

            //---------------------------------------------------------------
            // ABRIMOS EL FICHERO DEL INVENTARIO Y LOS ITEMS QUE CONTIENE
            //---------------------------------------------------------------

            FileStream file2 = File.Open(Application.persistentDataPath + inventoryFile, FileMode.Open);
            dataInv = (InventoryData)bf.Deserialize(file2);
            file2.Close();

            item = null;

            for (int i = 0; i < dataInv.itemsSize; i++)
            {
                //crear objeto instancia de los ITEMs
                item = CreateItemByType(dataInv.itemType[i]);

                item.SetID(dataInv.itemIDs[i]);
                item.SetRandNum(dataInv.itemRand[i]);
                item.SetActive(dataInv.itemActive[i]);
                item.SetType(dataInv.itemType[i]);
                item.DisableComponents();
                inv.SetItem(item, i);
            }

            Equipment equip = null;
            GameObject gmObject = null;

            for (int i = 0; i < dataInv.equipmentsSize; i++)
            {
                //crear instancia de equipamiento
                gmObject = Instantiate(Resources.Load(itemsManager.GetEquipmentPrefab(), typeof(GameObject))) as GameObject;
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

    public Item CreateItemByType(int i)
    {
        GameObject gmObject = null;
        Item item = null;

        switch (i)
        {
            case 0:
                gmObject = Instantiate(Resources.Load("Items/ManaPot", typeof(GameObject))) as GameObject;
                item = gmObject.GetComponent<HealthItem>();
                break;
            case 1:
                gmObject = Instantiate(Resources.Load("Items/LifePot", typeof(GameObject))) as GameObject;
                item = gmObject.GetComponent<BigHealthItem>();
                break;
            case 2:
                gmObject = Instantiate(Resources.Load("Items/Shield", typeof(GameObject))) as GameObject;
                item = gmObject.GetComponent<ExtendCaptureItem>();
                break;
            case 3:
                gmObject = Instantiate(Resources.Load("Items/Key", typeof(GameObject))) as GameObject;
                item = gmObject.GetComponent<XPMultiplierItem>();
                break;
        }
        return item;
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
    public int maxSizeActive;
    public int[] itemIDs;
    public int[] itemRand;
    public bool[] itemActive;
    public int[] itemType;
    public float[] itemTargetTime;
    public int itemSize;
}
