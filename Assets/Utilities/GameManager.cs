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
    private string puzzleFile = "/puzzleInfo.dat";
    PlayerData dataPly;
    InventoryData dataInv;
    ItemsManagerData dataItemsManager;
    PuzzleData dataPuzzle;

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
        dataPuzzle = new PuzzleData();
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
         if (dataPly == null || dataInv == null || dataItemsManager == null || dataPuzzle == null)
        {
            Debug.Log("algun data es null - VAMOS A INICIALIZAR DE NUEVO");

            dataPly = new PlayerData();
            dataInv = new InventoryData();
            dataItemsManager = new ItemsManagerData();
            dataPuzzle = new PuzzleData();
            FindPlayer();
            //InitializeScene();
        }
        playerFight = FindObjectOfType<PlayerFight>();
        playerPuzzle = FindObjectOfType<PuzzleManager>();

        //---------------------------------------------------------------
        // GUARDAMOS EL FICHERO DE LOS DATOS DEL JUGADOR
        //---------------------------------------------------------------

        SavePlayerData();

        //---------------------------------------------------------------
        // GUARDAMOS EL FICHERO DEL INVENTARIO Y LOS ITEMS QUE CONTIENE
        //---------------------------------------------------------------

        SaveInventoryData();


        //---------------------------------------------------------------
        // GUARDAMOS EL FICHERO DEL GESTOR DE ITEMS (IDS Y OBJETOS ACTIVOS)
        //---------------------------------------------------------------

        SaveItemData();

        //---------------------------------------------------------------
        // GUARDAMOS EL FICHERO DEL GESTOR DE ITEMS (IDS Y OBJETOS ACTIVOS)
        //---------------------------------------------------------------

        SavePuzzleData();

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

            LoadPlayerData();

            //---------------------------------------------------------------
            // ABRIMOS EL FICHERO DEL GESTOR DE ITEMS (IDS Y OBJETOS ACTIVOS)
            //---------------------------------------------------------------

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

                LoadItemData();
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

                LoadInventoryData();
            }
            else
            {
                if (!SetFightEquipment())
                    Debug.Log("El inventario es nulo al cargar");

            }

            FileStream file4 = File.Open(Application.persistentDataPath + puzzleFile, FileMode.Open);
            if (file4.Length > 0)
            {
                dataPuzzle = (PuzzleData)bf.Deserialize(file4);
                file4.Close();

            }
            else
            {
                file4.Close();
                File.Delete(Application.persistentDataPath + puzzleFile);
            }

            LoadPuzzleData();
        }
        else
        {
            FindObjectOfType<WelcomeScript>().ToggleWindowGroup();
        }

        playerFight = FindObjectOfType<PlayerFight>();
        playerPuzzle = FindObjectOfType<PuzzleManager>();

        if (playerFight == null && droidFactory != null && playerPuzzle == null)
        {
            droidFactory.SetGameStarted(-1, new int[1]);
        }

    }

    private void SavePuzzleData()
    {
        BinaryFormatter bf = new BinaryFormatter();

        
        FileStream file4 = File.Create(Application.persistentDataPath + puzzleFile);

        dataPuzzle.win = false;

        if (playerFight == null && droidFactory != null && playerPuzzle == null)
        {
            dataPuzzle.disabled_locations = new string[20];
            dataPuzzle.time = new string[20];

            LocationInfo[] locationInfo_array = FindObjectsOfType<LocationInfo>();
            int cont = 0;

            dataPuzzle.win = false;
            for (int i = 0; i < locationInfo_array.Length; i++)
            {
                if (locationInfo_array[i].gameObject.activeSelf)
                {
                    if (locationInfo_array[i].GetPuzzle().GetSelectedPuzzle())
                    {
                        dataPuzzle.selected_location = locationInfo_array[i].GetPuzzle().GetLocationInfo();
                        dataPuzzle.disabled_locations[cont] = locationInfo_array[i].GetPuzzle().GetLocationInfo();
                        dataPuzzle.time[cont] = locationInfo_array[i].GetPuzzle().GetTime();
                        cont++;
                    }
                    else
                    {
                        if (!locationInfo_array[i].GetPuzzle().GetActivePuzzle() || (locationInfo_array[i].GetPuzzle().GetTime()!=null && locationInfo_array[i].GetPuzzle().GetTime() != ""))
                        {
                            if (dataPuzzle.disabled_locations.Length > cont)
                            {
                                dataPuzzle.disabled_locations[cont] = locationInfo_array[i].GetPuzzle().GetLocationInfo();
                                dataPuzzle.time[cont] = locationInfo_array[i].GetPuzzle().GetTime();
                                Debug.Log("dataPuzzle.disabled_locations: " + dataPuzzle.disabled_locations[cont]);
                                cont++;
                            }
                        }
                    }
                }
            }

            dataPuzzle.badges_size=Badges.Instance.GetSize();

            dataPuzzle.badges = new string[dataPuzzle.badges_size];

            for (int i = 0; i < dataPuzzle.badges_size; i++)
            {
                dataPuzzle.badges[i] = Badges.Instance.GetTextArray()[i];
            }
        }
        else if (playerPuzzle!=null)
        {
            dataPuzzle.win = PuzzleManager.Instance.GetWin();
            
        }

        bf.Serialize(file4, dataPuzzle);
        file4.Close();
        

    }

    private void SaveItemData()
    {
        BinaryFormatter bf = new BinaryFormatter();

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
                dataItemsManager.itemPosX = new float[dataItemsManager.maxSizeActive];
                dataItemsManager.itemPosY = new float[dataItemsManager.maxSizeActive];
                dataItemsManager.itemPosZ = new float[dataItemsManager.maxSizeActive];
            }

            for (int i = 0; i < dataItemsManager.itemSize; i++)
            {
                dataItemsManager.itemIDs[i] = itemsManager.GetItems()[i].GetID();
                dataItemsManager.itemRand[i] = itemsManager.GetItems()[i].GetRand();
                dataItemsManager.itemActive[i] = itemsManager.GetItems()[i].GetActive();
                dataItemsManager.itemType[i] = itemsManager.GetItems()[i].GetItemType();
                dataItemsManager.itemTargetTime[i] = itemsManager.GetItems()[i].GetTargetTime();
                dataItemsManager.itemPosX[i] = itemsManager.GetItems()[i].transform.localPosition.x;
                dataItemsManager.itemPosY[i] = itemsManager.GetItems()[i].transform.localPosition.y;
                dataItemsManager.itemPosZ[i] = itemsManager.GetItems()[i].transform.localPosition.z;

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

    private void SaveInventoryData()
    {
        BinaryFormatter bf = new BinaryFormatter();

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
                dataInv.itemPosX = new float[dataInv.space];
                dataInv.itemPosY = new float[dataInv.space];
                dataInv.itemPosZ = new float[dataInv.space];
            }

            for (int i = 0; i < dataInv.itemsSize; i++)
            {
                dataInv.itemIDs[i] = inv.getItems()[i].GetID();
                dataInv.itemRand[i] = inv.getItems()[i].GetRand();
                dataInv.itemActive[i] = inv.getItems()[i].GetActive();
                dataInv.itemType[i] = inv.getItems()[i].GetItemType();
                dataInv.itemPosX[i] = inv.getItems()[i].transform.localPosition.x;
                dataInv.itemPosY[i] = inv.getItems()[i].transform.localPosition.y;
                dataInv.itemPosZ[i] = inv.getItems()[i].transform.localPosition.z;
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
                dataInv.equipPosX = new float[dataInv.space];
                dataInv.equipPosY = new float[dataInv.space];
                dataInv.equipPosZ = new float[dataInv.space];
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
                dataInv.equipPosX[i] = inv.getEquipments()[i].transform.localPosition.x;
                dataInv.equipPosY[i] = inv.getEquipments()[i].transform.localPosition.y;
                dataInv.equipPosZ[i] = inv.getEquipments()[i].transform.localPosition.z;
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
    }

    private void SavePlayerData()
    {
        BinaryFormatter bf = new BinaryFormatter();

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

            dataPly.volume = currentPlayer.GetSoundLevel();
            dataPly.user_name = currentPlayer.GetUserName();
            dataPly.lastGameDate = System.DateTime.Now.ToString();



            if (playerFight == null && droidFactory != null && playerPuzzle == null)
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
    }


    private void LoadPuzzleData()
    {
        if (playerFight == null && droidFactory != null && playerPuzzle == null)
        {
            Badges b = Badges.Instance;
            for (int i=0;i< dataPuzzle.badges_size; i++)
            {
                b.AddSlot(dataPuzzle.badges[i]);
            }
            if (dataPuzzle.win)
                b.AddSlot(dataPuzzle.selected_location);

            StartCoroutine(Wait(2f));



        }
    }

    

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
            CheckDataPuzzle();

    }

    void CheckDataPuzzle()
    {
        LocationInfo[] locationInfo_array = FindObjectsOfType<LocationInfo>();
        StartPuzzle sp = null;
        int size = locationInfo_array.Length;
        if (dataPuzzle!=null && dataPuzzle.disabled_locations!=null)
        for (int i = 0; i < dataPuzzle.disabled_locations.Length; i++)
        {
            for (int j = 0; j < locationInfo_array.Length; j++)
            {
                sp = locationInfo_array[j].GetPuzzle();
                if (sp.GetLocationInfo() == dataPuzzle.disabled_locations[i])
                {
                    Debug.Log("CheckDataPuzzle: " + sp.GetLocationInfo());
                    sp.SetTime(dataPuzzle.time[i]);
                    sp.SetActive(false);
                    break;
                }

            }
        }
    }

    private void LoadInventoryData()
    {
        Item item = null;

        if (dataInv != null)
        {

            for (int i = 0; i < dataInv.itemsSize; i++)
            {
                //crear objeto instancia de los ITEMs
                item = CreateItemByType(dataInv.itemType[i], new Vector3(dataInv.itemPosX[i], dataInv.itemPosY[i], dataInv.itemPosZ[i]));

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
                gmObject = Instantiate(Resources.Load(itemsManager.GetEquipmentPrefab(), typeof(GameObject)), new Vector3(dataInv.equipPosX[i], dataInv.equipPosY[i], dataInv.equipPosZ[i]), new Quaternion()) as GameObject;
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

    private void LoadItemData()
    {

        Item item = null;

        if (dataItemsManager != null)
        {
            itemsManager.SetLastEquipID(dataItemsManager.lastIDEquip);
            itemsManager.SetLastItemID(dataItemsManager.lastIDItem);

            //Debug.Log("item size load: " + dataItemsManager.itemSize);

            for (int i = 0; i < dataItemsManager.itemSize; i++)
            {
                item = CreateItemByType(dataItemsManager.itemType[i], new Vector3(dataItemsManager.itemPosX[i], dataItemsManager.itemPosY[i], dataItemsManager.itemPosZ[i]));

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

    private void LoadPlayerData()
    {
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
            currentPlayer.SetSoundLevel(dataPly.volume, true);
            if (dataPly.user_name != null)
                currentPlayer.SetUserName(dataPly.user_name);
            else
                FindObjectOfType<WelcomeScript>().ToggleWindowGroup();

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
    }

    public Item CreateItemByType(int i,Vector3 vec)
    {
        GameObject gmObject = null;
        Item item = null;

        switch (i)
        {
            case 0:
                gmObject = Instantiate(Resources.Load("Items/Models/potionHP", typeof(GameObject)), vec, new Quaternion()) as GameObject;
                item = gmObject.GetComponent<HealthItem>();
                break;
            case 1:
                gmObject = Instantiate(Resources.Load("Items/Models/bigPotionHP", typeof(GameObject)), vec, new Quaternion()) as GameObject;
                item = gmObject.GetComponent<BigHealthItem>();
                break;
            case 2:
                gmObject = Instantiate(Resources.Load("Items/Models/CaptureRangeUP", typeof(GameObject)), vec, new Quaternion()) as GameObject;
                item = gmObject.GetComponent<ExtendCaptureItem>();
                break;
            case 3:
                gmObject = Instantiate(Resources.Load("Items/Models/Key", typeof(GameObject)), vec, new Quaternion()) as GameObject;
                item = gmObject.GetComponent<XPMultiplierItem>();
                break;
            case 4:
                gmObject = Instantiate(Resources.Load("Items/Models/DurabilityUP", typeof(GameObject)), vec, new Quaternion()) as GameObject;
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
        if (playerPuzzle != null || playerFight != null) { 
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
    public string user_name;
    public float volume;
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
    public float[] itemPosX;
    public float[] itemPosY;
    public float[] itemPosZ;
    public int itemsSize;

    public int[] equipIDs;
    public int[] equipQuality;
    public int[] equipType;
    public int[] equipAttack;
    public int[] equipDefense;
    public int[] equipSpeed;
    public int[] equipDurability;
    public bool[] equipActive;
    public float[] equipPosX;
    public float[] equipPosY;
    public float[] equipPosZ;
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
    public float[] itemPosX;
    public float[] itemPosY;
    public float[] itemPosZ;
    public int itemSize;
}

[Serializable]
class PuzzleData
{
    public string selected_location;
    public string[] disabled_locations;
    public string[] time;
    public bool win;
    public string[] badges;
    public int badges_size;
}
