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
    PlayerFight playerFight;
    PuzzleManager playerPuzzle;
    private string playerFile = "/playerInfo.dat";
    private string inventoryFile = "/inventoryInfo.dat";
    private string itemManagerFile = "/itemManagerInfo.dat";
    PlayerData dataPly;
    InventoryData dataInv;
    ItemsManagerData dataItemsManager;

    Inventory inv;
    ItemsManager itemsManager;
    DroidFactory droidFactory;
    bool spawned = false;

    private void Start()
    {
        if (spawned == false)
        {
            spawned = true;
            DontDestroyOnLoad(gameObject);
            
            //InitializeScene();

        }
        else
        {
            Debug.Log("DESTURUCCION!");
            DestroyImmediate(gameObject);
        }
    }
    public void InitializeScene()
    {
        Debug.Log("INICIALIZO");
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        dataPly = new PlayerData();
        dataInv = new InventoryData();
        dataItemsManager = new ItemsManagerData();
        FindPlayer();
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

    }

    public Player CurrentPlayer
    {       
        get { FindPlayer(); return currentPlayer; }
    }

    public void ExitGame()
    {
        Debug.Log("Hola y adios");
        Application.Quit();
    }

    private void FindPlayer()
    {
        if (currentPlayer == null)
        {
            currentPlayer = FindObjectOfType<Player>();
            if (currentPlayer == null)
            {
                playerFight = FindObjectOfType<PlayerFight>();
                playerPuzzle = FindObjectOfType<PuzzleManager>();
                currentPlayer = playerFight.gameObject.GetComponent<Player>();
                Debug.Log("playerFight");
            }
        }
        //Assert.IsNotNull(currentPlayer);

    }
    public void Save()
    {
        inv = Inventory.Instance;
        itemsManager = ItemsManager.Instance;
        droidFactory = DroidFactory.Instance;
         if (dataPly == null || dataInv == null || dataItemsManager == null)
        {
            Debug.Log("algun data es null - VAMOS A INICIALIZAR DE NUEVO");

            dataPly = new PlayerData();
            dataInv = new InventoryData();
            dataItemsManager = new ItemsManagerData();
            FindPlayer();
            //InitializeScene();
        }
        playerFight = FindObjectOfType<PlayerFight>();
        playerPuzzle = FindObjectOfType<PuzzleManager>();
        BinaryFormatter bf = new BinaryFormatter();

        //---------------------------------------------------------------
        // GUARDAMOS EL FICHERO DE LOS DATOS DEL JUGADOR
        //---------------------------------------------------------------
        
        if (currentPlayer != null)
        {
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
            dataPly.pos = new float[3];
            dataPly.pos[0] = currentPlayer.GetAuxRespawnPos().x;
            dataPly.pos[1] = currentPlayer.GetAuxRespawnPos().y;
            dataPly.pos[2] = currentPlayer.GetAuxRespawnPos().z;

            dataPly.lastGameDate = System.DateTime.Now.ToString();

            

            if (playerFight == null && droidFactory!= null && playerPuzzle == null)
            {
                dataPly.droidSize = droidFactory.LiveDroids.Count;
                Debug.Log("dataPly.droidSize save: " + dataPly.droidSize);
                dataPly.droidPosX = new float[dataPly.droidSize];
                dataPly.droidPosY = new float[dataPly.droidSize];
                dataPly.droidPosZ = new float[dataPly.droidSize];
                dataPly.droidType = new int[dataPly.droidSize];
                for (int h = 0; h < dataPly.droidSize; h++)
                {
                    dataPly.droidPosX[h] = droidFactory.LiveDroids[h].transform.position.x;
                    dataPly.droidPosY[h] = droidFactory.LiveDroids[h].transform.position.y;
                    dataPly.droidPosZ[h] = droidFactory.LiveDroids[h].transform.position.z;
                    dataPly.droidType[h] = droidFactory.LiveDroids[h].GetDroidType();
                    if (droidFactory.GetDroidIndex() == h)
                        dataPly.droidSelectedType = droidFactory.LiveDroids[h].GetDroidType();
                }
                dataPly.droidCombatID = droidFactory.GetDroidIndex();

            }

            if (playerFight != null)
            {
                dataPly.combatWin = EnemyFightManager.Instance.GetWin();
            }

            bf.Serialize(file, dataPly);
            file.Close();
        }

        //---------------------------------------------------------------
        // GUARDAMOS EL FICHERO DEL INVENTARIO Y LOS ITEMS QUE CONTIENE
        //---------------------------------------------------------------

        if (inv != null)
        {
            FileStream file2 = File.Create(Application.persistentDataPath + inventoryFile);

            dataInv.itemsSize = inv.getItems().Count;
            //Debug.Log("inventory item size save: " + dataInv.itemsSize);
            dataInv.space = 26; // inv.getItems().Capacity
            if (dataInv.itemIDs == null)
            {
                dataInv.itemIDs = new int[dataInv.space];
                dataInv.itemRand = new int[dataInv.space];
                dataInv.itemActive = new bool[dataInv.space];
                dataInv.itemType = new int[dataInv.space];
            }

            for (int i = 0; i < dataInv.itemsSize; i++)
            {
                dataInv.itemIDs[i] = inv.getItems()[i].GetID();
                dataInv.itemRand[i] = inv.getItems()[i].GetRand();
                dataInv.itemActive[i] = inv.getItems()[i].GetActive();
                dataInv.itemType[i] = inv.getItems()[i].GetItemType();
            }


            dataInv.equipmentsSize = inv.getEquipments().Count;
            //Debug.Log("inventory equip size save: " + dataInv.equipmentsSize);
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

            //Debug.Log("ID SAVE inv.GetCurrentEquipmentID(): " + inv.GetCurrentEquipmentID());
            dataInv.id_e_selected = inv.GetCurrentEquipmentID();

            bf.Serialize(file2, dataInv);
            file2.Close();
        }
        else
        {
            if (!DecreaseEquipDurability())
            Debug.Log("El inventario es nulo al guardar");
        }
        //---------------------------------------------------------------
        // GUARDAMOS EL FICHERO DEL GESTOR DE ITEMS (IDS Y OBJETOS ACTIVOS)
        //---------------------------------------------------------------
        if (itemsManager != null)
        {
            FileStream file3 = File.Create(Application.persistentDataPath + itemManagerFile);

            dataItemsManager.lastIDEquip = itemsManager.GetLastEquipID();
            dataItemsManager.lastIDItem = itemsManager.GetLastItemID();
            dataItemsManager.itemSize = itemsManager.GetItems().Count;

            dataItemsManager.maxSizeActive = 3; //itemsManager.GetItems().Capacity;  
            //Debug.Log("item manager size save: " + dataItemsManager.itemSize);
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

                //Debug.Log("ID save: " + dataItemsManager.itemIDs[i]);
                //Debug.Log("Active save: " + dataItemsManager.itemActive[i]);
                //Debug.Log("Target save: " + dataItemsManager.itemTargetTime[i]);
            }

            bf.Serialize(file3, dataItemsManager);
            file3.Close();
        }
        else
        {
            Debug.Log("El itemManager es nulo al guardar");
        }

    }
    public void Load()
    {
        itemsManager = ItemsManager.Instance;
        inv = Inventory.Instance;
        droidFactory = DroidFactory.Instance;

        if (File.Exists(Application.persistentDataPath + playerFile))
        {
            playerFight = FindObjectOfType<PlayerFight>();
            playerPuzzle = FindObjectOfType<PuzzleManager>();
            BinaryFormatter bf = new BinaryFormatter();

            //---------------------------------------------------------------
            // ABRIMOS EL FICHERO DE LOS DATOS DEL JUGADOR
            //---------------------------------------------------------------

            FileStream file1 = File.Open(Application.persistentDataPath + playerFile, FileMode.Open);
            if (file1.Length > 0)
            {
                dataPly = (PlayerData)bf.Deserialize(file1);
                file1.Close();

            }
            else
            {
                file1.Close();
                File.Delete(Application.persistentDataPath + playerFile);
            }
            if (dataPly != null)
            {
                currentPlayer.Xp = dataPly.xp;
                currentPlayer.RequiredXp = dataPly.requiredXp;
                currentPlayer.LevelBase = dataPly.levelBase;
                currentPlayer.Lvl = dataPly.lvl;
                currentPlayer.Total_xp = dataPly.total_xp;
                currentPlayer.Hp = dataPly.hp;
                currentPlayer.MaxHp = dataPly.maxHp;
                currentPlayer.CaptureRange = dataPly.captureRange;
                currentPlayer.Xp_Multiplier = dataPly.xp_multiplier;
                Vector3 v3 = new Vector3(dataPly.pos[0], dataPly.pos[1], dataPly.pos[2]);
                currentPlayer.SetAuxRespawnPos(v3);

                

                if (playerPuzzle == null)
                {
                    if (playerFight == null && droidFactory != null)
                    {
                        if (System.DateTime.Now < DateTime.Parse(dataPly.lastGameDate).AddMinutes(20))
                        {
                            Debug.Log("dataPly.droidSize load: " + dataPly.droidSize);
                            //droidFactory.SetGameStarted();
                            /*if (droidFactory.LiveDroids.Count != dataPly.droidSize)
                                droidFactory.SetStartingDroids(dataPly.droidSize);*/
                            droidFactory.SetGameStarted(dataPly.droidSize, dataPly.droidType);
                            for (int h = 0; h < dataPly.droidSize; h++)
                            {
                                droidFactory.LiveDroids[h].transform.position = new Vector3(dataPly.droidPosX[h], dataPly.droidPosY[h], dataPly.droidPosZ[h]);
                                droidFactory.LiveDroids[h].SetDroidType(dataPly.droidType[h]);
                            }
                            Debug.Log("dataPly.combatWin load (borramos?): " + dataPly.combatWin);
                            if (dataPly.combatWin)
                                droidFactory.SetDefeated(dataPly.droidCombatID);
                            dataPly.combatWin = false;
                        }
                    }
                    else
                    {
                        EnemyFightManager.Instance.InstantiateEnemies(dataPly.droidSelectedType);
                    }
                }
            }
            else
            {
                Debug.Log("El player es nulo al cargar");
            }

            //---------------------------------------------------------------
            // ABRIMOS EL FICHERO DEL GESTOR DE ITEMS (IDS Y OBJETOS ACTIVOS)
            //---------------------------------------------------------------
            Item item = null;

            if (itemsManager != null)
            {
                FileStream file3 = File.Open(Application.persistentDataPath + itemManagerFile, FileMode.Open);
                if (file3.Length > 0)
                {
                    dataItemsManager = (ItemsManagerData)bf.Deserialize(file3);
                    file3.Close();

                }
                else
                {
                    file3.Close();

                    File.Delete(Application.persistentDataPath + itemManagerFile);
                }

                if (dataItemsManager != null)
                {
                    itemsManager.SetLastEquipID(dataItemsManager.lastIDEquip);
                    itemsManager.SetLastItemID(dataItemsManager.lastIDItem);

                    //Debug.Log("item size load: " + dataItemsManager.itemSize);

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
                }
            }
            else
            {
                Debug.Log("El itemManager es nulo al cargar");
            }
            //---------------------------------------------------------------
            // ABRIMOS EL FICHERO DEL INVENTARIO Y LOS ITEMS QUE CONTIENE
            //---------------------------------------------------------------

            if (inv != null)
            {
                FileStream file2 = File.Open(Application.persistentDataPath + inventoryFile, FileMode.Open);
                if (file2.Length > 0)
                {
                    dataInv = (InventoryData)bf.Deserialize(file2);
                    file2.Close();

                }
                else
                {
                    file2.Close();
                    File.Delete(Application.persistentDataPath + inventoryFile);
                }

                item = null;
                if (dataInv != null)
                {

                    for (int i = 0; i < dataInv.itemsSize; i++)
                    {
                        //crear objeto instancia de los ITEMs
                        item = CreateItemByType(dataInv.itemType[i]);

                        item.SetID(dataInv.itemIDs[i]);
                        item.SetRandNum(dataInv.itemRand[i]);
                        item.SetActive(dataInv.itemActive[i]);
                        item.SetType(dataInv.itemType[i]);
                        item.DisableComponents();
                        //if (inv.getItems().Count > i)
                        inv.SetItem(item, i);
                    }
                    //Debug.Log("items inventory size load: " + inv.getItems().Count);
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
                        //if (inv.getEquipments().Count>i)
                        inv.SetEquip(equip, i);
                    }
                    //Debug.Log("equip inventory size load: " + inv.getEquipments().Count);
                    inv.modified = true;
                    inv.SetSpace(dataInv.space);

                    //Debug.Log("LOAD dataInv.id_e_selected: " + dataInv.id_e_selected);
                    inv.SelectEquipmentID(dataInv.id_e_selected);

                }
            }
            else
            {

                if (!SetFightEquipment())
                    Debug.Log("El inventario es nulo al cargar");


            }


        }

        playerFight = FindObjectOfType<PlayerFight>();
        playerPuzzle = FindObjectOfType<PuzzleManager>();

        if (playerFight == null && droidFactory != null && playerPuzzle == null)
        {
            droidFactory.SetGameStarted(-1,new int[1]);
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
            case 4:
                gmObject = Instantiate(Resources.Load("Items/DurabilityUP", typeof(GameObject))) as GameObject;
                item = gmObject.GetComponent<DurabilityUP>();
                break;
        }
        return item;
    }

    public bool SetFightEquipment()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file2 = File.Open(Application.persistentDataPath + inventoryFile, FileMode.Open);
        if (file2.Length > 0)
        {
            dataInv = (InventoryData)bf.Deserialize(file2);
            file2.Close();

        }
        else
        {
            file2.Close();
            File.Delete(Application.persistentDataPath + inventoryFile);
        }

        if (dataInv != null)
        {
            Debug.Log("Intentamos cargar equipment en escena batalla");

            if (playerFight != null)
            {
                Debug.Log("No es nulo!");
                Weapon w = null;
                for (int i = 0; i < dataInv.equipmentsSize; i++)
                {
                    if (dataInv.id_e_selected == dataInv.equipIDs[i])
                    {
                        Debug.Log("encontrado!");
                        w = playerFight.gameObject.GetComponent<Weapon>();
                        if (w != null)
                        {
                            Debug.Log("cargamos!");
                            w.SetWeaponStats(dataInv.equipDurability[i],dataInv.equipType[i],dataInv.equipQuality[i],dataInv.equipAttack[i], dataInv.equipDefense[i], dataInv.equipSpeed[i]);
                            return true;
                        }
                        else
                        {
                            Debug.Log("Sa liao!");
                        }
                        break;
                    }
                }

            }
        }
        return false;
    }

    public bool AddNewEquipment(Equipment eq)
    {
        if (playerPuzzle != null) { 
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file2 = File.Open(Application.persistentDataPath + inventoryFile, FileMode.Open);
            if (file2.Length > 0)
            {
                dataInv = (InventoryData)bf.Deserialize(file2);
                file2.Close();
            }
            else
            {
                file2.Close();
                File.Delete(Application.persistentDataPath + inventoryFile);
            }

            if (dataInv != null)
            {
                file2 = File.Open(Application.persistentDataPath + inventoryFile, FileMode.Open);
                eq.SetDurability(100);
                dataInv.equipmentsSize++;

                int[] quality = new int[dataInv.equipmentsSize];
                int[] type = new int[dataInv.equipmentsSize];
                int[] attack = new int[dataInv.equipmentsSize];
                int[] defense = new int[dataInv.equipmentsSize];
                int[] speed = new int[dataInv.equipmentsSize];
                int[] durability = new int[dataInv.equipmentsSize];
                bool[] active = new bool[dataInv.equipmentsSize];
                int[] id = new int[dataInv.equipmentsSize];

                for (int i=0;i< dataInv.equipmentsSize;i++)
                {
                    if (i != dataInv.equipmentsSize-1)
                    {
                        quality[i] = dataInv.equipQuality[i];
                        type[i] = dataInv.equipType[i];
                        attack[i] = dataInv.equipAttack[i];
                        defense[i] = dataInv.equipDefense[i];
                        speed[i] = dataInv.equipSpeed[i];
                        durability[i] = dataInv.equipDurability[i];
                        active[i] = dataInv.equipActive[i];
                        id[i] = dataInv.equipIDs[i];
                    }
                    else
                    {
                        dataInv.equipQuality[i] = (int)eq.GetEquipmentQualityNum();
                        dataInv.equipType[i] = (int)eq.GetEquipmentTypeNum();
                        dataInv.equipAttack[i] = eq.GetAttack();
                        dataInv.equipDefense[i] = eq.GetDefense();
                        dataInv.equipSpeed[i] = eq.GetSpeed();
                        dataInv.equipDurability[i] = eq.GetDurability();
                        dataInv.equipActive[i] = eq.GetActive();
                        dataInv.equipIDs[i] = eq.GetID();
                    }
                }
                bf.Serialize(file2, dataInv);
                file2.Close();
                return true;
            }
        }
        return false;
    }
    public bool DecreaseEquipDurability()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file2 = File.Open(Application.persistentDataPath + inventoryFile, FileMode.Open);
        if (file2.Length > 0)
        {
            dataInv = (InventoryData)bf.Deserialize(file2);
            file2.Close();

        }
        else
        {
            file2.Close();
            File.Delete(Application.persistentDataPath + inventoryFile);
        }

        if (dataInv != null)
        {
            Debug.Log("Intentamos establecer la durabilidad del equipment en escena batalla");

            if (playerFight != null)
            {
                Debug.Log("No es nulo!");
                Weapon w = null;
                for (int i = 0; i < dataInv.equipmentsSize; i++)
                {
                    if (dataInv.id_e_selected == dataInv.equipIDs[i])
                    {
                        Debug.Log("encontrado!");
                        w = playerFight.gameObject.GetComponent<Weapon>();
                        if (w != null)
                        {
                            Debug.Log("cargamos!");
                            file2 = File.Open(Application.persistentDataPath + inventoryFile, FileMode.Open);
                            dataInv.equipDurability[i] = w.GetWeaponDurability();
                            bf.Serialize(file2, dataInv);
                            file2.Close();
                            return true;
                        }
                        else
                        {
                            Debug.Log("Sa liao!");
                        }
                        break;
                    }
                }

            }
        }
        return false;
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
    public float[] pos;

    public float[] droidPosX;
    public float[] droidPosY;
    public float[] droidPosZ;
    public int[] droidType;
    public int droidSelectedType;
    public int droidSize;
    public int droidCombatID;
    public bool combatWin;
    public string lastGameDate;
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
