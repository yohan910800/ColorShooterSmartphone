using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using UnityEngine.SceneManagement;
//added_dohan------------------
using System.Linq;
//-----------------------------
public class Player : Character {
    Transform[] singleSocket;
    Transform[] singleSocket2;
    Transform[] dualWieldSockets;

    [HideInInspector]
    public InventoryUI inventoryUI;
    public StateUI stateUIFixe;
    public GameObject stateUIPrefabFixe;
    public GameObject stateUII;
    ////added_dohan------------------

    //public GameObject shopInteractionObj;
    //ShopInteraction shopInteraction;
    Transform directionIndication;
    const float directionIndicationDistance = 4;
    Transform sprite;

    SpriteRenderer directionIndicationSprite;
    Color dIColor;
    GameObject gainEnergyEffect;
    //GameObject runSmokeEffect;
    //GameObject runSmokeEffectOrange;
    public GameObject[] runSmokeEffect;
    public GameObject[] runSmokeEffectOrange;
    bool[] justOnceSmoke;
    float transparency;

    int smokeIndex;
    float timerSmoke;
    bool justOnceSound = false;

    // for tutorial
    public bool didThePlayerOverComeTheArea6 = false;
    GameObject gameFinishPanel;

    ////-----------------------------

    //public SaveSystemManager saveSystemManager;

    //AutoSaveData autoSaveData;
    //HubSaveData hubSaveData;

    //public GameObject MapManagerObj;
    //public GameObject triggerzone;

    public bool initiated = false;

    PlayerCombatV1 playerComb;

    float timerInvisible;
    
    bool jsutOnceInstantiateGameOverPanel;
    bool justOnceDisablePlayerBody;


    public bool justOnceTutorialRandomBtn;
    public int tierIndex ;// for bonus
    public bool meleeBtnPressedForTheFirsTime = false;

    BoxCollider2D weaponColl1;
    BoxCollider2D weaponColl2;


    public override void Init() {
        base.Init();
        var weaponSocket1 = transform.Find("Hand1");
        var weaponSocket2 = transform.Find("Hand2");
        dualWieldSockets = new Transform[]{weaponSocket1,weaponSocket2};
        singleSocket = new Transform[]{weaponSocket2};
        singleSocket2 = new Transform[]{weaponSocket1};
        
        weaponColl1 = weaponSocket1.GetComponent<BoxCollider2D>();
        weaponColl2 = weaponSocket2.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision
            (weaponColl2, GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision
            (weaponColl1, GetComponent<BoxCollider2D>());
        InitNewInventory();

        ActivateWeapon(inventory.Weapons[0]);
        

        // 2. check if save file exists -load if exists -make a new one if not---------------------------------------------------

        /*if (File.Exists(Application.dataPath + "/savedata/HubData.test"))
        {
            hubSaveData = SaveSystem.Loadhubsavedata();
            inventory.SetCredits(hubSaveData.credits);
            Debug.Log("HubDataLoaded" + hubSaveData.credits);
        }
        else
        {
            hubSaveData = new HubSaveData(10);
            inventory.SetCredits(hubSaveData.credits);
        }
        if (File.Exists(Application.dataPath + "/savedata/autoSaveData.test"))
        {
            LoadAndSetPlayer(SaveSystem.LoadAutoSaveData());
        }
        else { CreateAutoSaveData(); } // causing Curl error 56: Receiving data failed with unitytls error code 1048578

        */
        //3. inventory UI init (assential??) commented out - not in use.
        //inventoryUI = Instantiate(Resources.Load<GameObject>("Prefabs/InventoryUI"), Vector3.zero, Quaternion.identity).GetComponent<InventoryUI>();
        //inventoryUI.Init(this,inventory);

        //4. Direction indication 
        directionIndication = transform.Find("DirectionIndication");
        sprite = directionIndication.transform.Find("DirectionSprite");
        directionIndicationSprite = sprite.GetComponent<SpriteRenderer>();
        //dIColor = directionIndicationSprite.color;

        //5. State UI init( hp bar etc )
        GameObject stateUIInstanceFixe = Instantiate(stateUIPrefabFixe,/*Vector3.Zero*/ 
            new Vector3(0.0f,3000.0f,0.0f), Quaternion.identity) as GameObject;
        stateUIFixe = stateUIInstanceFixe.GetComponentInChildren<StateUI>();
        stateUIFixe.Init(this, true, true);// character, hasEnergy, isFixed

        //GetInventory().AddCredits(10);//tmp
        stateUIFixe.UpdateCredits();//tmp

        //6.VFX init
        gainEnergyEffect = Resources.Load<GameObject>("Prefabs/Effects/DropEnergyEffect");
        //runSmokeEffect = Resources.Load<GameObject>("Prefabs/Effects/RunEffect");
        //runSmokeEffectOrange = Resources.Load<GameObject>("Prefabs/Effects/RunEffectOrange");

        //playerComb = GetComponent<PlayerCombatV1>();
        //playerComb.weapons[0] = GetActiveWeapon();//^======
        justOnceSmoke = new bool[2];

        //7. etc 

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //8. initiated! (needed for cameraControl. )
        initiated = true;
        stateUII = GameObject.Find("PlayerStateUI(Clone)");
        stateUII.GetComponent<StateUI>().Refresh();
        //bonus
        //GetComponent<PlayerCombatV1>().weapons[0] = GetActiveWeapon();
        justOnceSmoke = new bool[2];
        //GetActiveWeapon().GetBullet().GetComponent<Bullet>().doesPoisonedBulletIsActivate = false;
        //GetStats().GainEnergy(100);
        //stateUI.Refresh();//test
        //Log.log("name " + MapManagerObj.name);
        
        //9. initate player die effect
        deadEffectObj= transform.Find("DeadEffect").gameObject;
        deadEffectAnimObj= transform.Find("DeadEffectAnim").gameObject;
        playerDeadBody = GameObject.Find("Player").transform.Find("PlayerDeadBody").gameObject;

        playerMainBody = new GameObject[6];

        playerMainBody[0] = GameObject.Find("Player").transform.Find("Body").gameObject;
        playerMainBody[1] = GameObject.Find("Player").transform.Find("Face").gameObject;
        playerMainBody[2] = GameObject.Find("Player").transform.Find("Hand1").gameObject;
        playerMainBody[3] = GameObject.Find("Player").transform.Find("Hand2").gameObject;
        playerMainBody[4] = GameObject.Find("Player").transform.Find("Foot1").gameObject;
        playerMainBody[5] = GameObject.Find("Player").transform.Find("Foot2").gameObject;


        //load and set data

        //diedAnimKindOfParticle = GameObject.Find("diedAnimation");
        //GetStats().SetAttack(5);
        //GetStats().GainEnergy(0);
        //    GetInventory().AddLife(0);
        //GetStats().SetHP(10);
        playerComb = GetComponent<PlayerCombatV1>();
        playerComb.weapons[0] = GetActiveWeapon();


        //saveSystemManager.InitSaveData();
        stateUIFixe.Refresh();


    }
    private void InitNewInventory()
    {
        AddColor(Colors.Blue);
        AddColor(Colors.Red);
        SetBulletColor(Colors.Blue);                       // Weapon Index
        Weapon weap = new SingleShooter(); //....................1,0
        weap.Init(this, singleSocket);
        AddWeapon(weap);

        AddWeapon(weap); //------------------------------------------singleShooter added twice.
        weap = new Shotgun(); //..................................2
        weap.Init(this, singleSocket);
        AddWeapon(weap);
        weap = new Rifle(); //....................................3
        weap.Init(this, singleSocket);
        AddWeapon(weap);
        weap = new TheMiddleFingerGun(); //.......................4
        weap.Init(this, singleSocket);
        AddWeapon(weap);
        weap = new TheEraser();//.................................5
        weap.Init(this, singleSocket);
        AddWeapon(weap);
        weap = new BurstShooter();//..............................6
        weap.Init(this, dualWieldSockets);
        AddWeapon(weap);

        weap = new MeleeMode();//..............................7
        weap.Init(this, dualWieldSockets);
        AddWeapon(weap);

        weap = new TripleSingleShooter();//.......................8
        weap.Init(this, singleSocket);
        AddWeapon(weap);
        weap = new LaserSurgeryGun();//...........................9
        weap.Init(this, singleSocket);
        AddWeapon(weap);
    }

    protected override void InitInput(){
        ChangeInputModule(Factory.NewInput());
        input.Init();
    }

    protected override void InitStateUI(){
        stateUI.Init(this,false,false);// character, hasEnergy, isFixed
    }


    public void SetTierNum(int tierNum)
    {
        tierIndex = tierNum;
    }

    //public void ActivateWeapon1(Weapon weapon)
    //{
    //    ActivateWeapon(weapon);
    //    weaponColl2.offset = new Vector2(weapon.barrelLen + 0.2f, 0);
    //    weaponColl2.radius = weapon.barrelLen;


    //}
    //public void ActivateWeapon2(Weapon weapon2)
    // {
    //     weapon2.Init(this, singleSocket2);
    //     AddWeapon(weapon2);
    //     ActivateAllWeapons(weapon2);
    //     weaponColl1.offset = new Vector2(weapon2.barrelLen + 0.2f, 0);
    //     weaponColl1.radius = weapon2.barrelLen;
    // }


    protected override void Update() {

        
        //GetStats().GainEnergy(100);
        //Log.log("inventory size " + inventory.Weapons.Count);
        //Log.log("life  " + GetInventory().Life);

        input.Update();
        DirectionIndication();
        PlayRunSoundAndSmokeEffect();

        DisableActiveInvisible();
        //Log.log("active panel " +stateUI.doesPlayerAlreayUseOneLife);
    }

    void PlayRunSoundAndSmokeEffect()
    {
        if (GetInputModule().GetDirection() != Vector2.zero)
        {
            ActivateSmoke();
            if (justOnceSound == false)
            {
                audioManager.Play("Run1");
                justOnceSound = true;
            }
        }
        else
        {
            audioManager.Pause("Run1");
            justOnceSound = false;
            ResetSmokeEffect();
        }
    }

    void ActivateSmoke()
    {
        for (int i = 0; i < 2; i++)
        {
            justOnceSmoke[i] = false;
        }


        timerSmoke += Time.deltaTime;
        if (timerSmoke >= 0.4f)
        {
            var effect = gm.WorldColor == Colors.Brown ? runSmokeEffect : runSmokeEffectOrange;
            effect[smokeIndex % 2].transform.position = transform.position;
            effect[smokeIndex % 2].SetActive(true);
            smokeIndex++;
            timerSmoke = 0;
            runSmokeEffect[smokeIndex % 2].SetActive(false);
            runSmokeEffectOrange[smokeIndex % 2].SetActive(false);
        }
    }
    void ResetSmokeEffect()
    {
        smokeIndex = 0;
        foreach (GameObject smoke in runSmokeEffect)
        {
            smoke.SetActive(false);
        }
        foreach (GameObject smoke in runSmokeEffectOrange)
        {
            smoke.SetActive(false);
        }
    }
        
    public void GetHit(int dmg){
        if (activeInvisible == true)
        {
            dmg = 0;
        }
        stats.LowerHP(Convert.ToInt32( dmg*0.01*(100-stats.Defence))); // added by dohan------------------------------<<< check the code and delete the comment 
        stateUI.Refresh();
        stateUIFixe.Refresh();//added yohan
        PlayGetHitSound();
        //stateUI.RefreshPlayerAlpha();
        if (stats.HP == 0 && IsAlive){
            OnDeath();
        }
        if(gm.showDamageText){
            var gObj = Instantiate(damageTextPrefab,transform.position,Quaternion.identity) as GameObject;
            DamageText dt = gObj.GetComponent<DamageText>();
            dt.Init(dmg, Color.red);
        }
    }
    //need to be remake
    void PlayGetHitSound()
    {
        FindObjectOfType<AudioManager>().Play("Impact3");
    }

    void DisableActiveInvisible()
    {
        if (activeInvisible == true)
        {
            timerInvisible += Time.deltaTime;
            if (timerInvisible >= 3)
            {

                activeInvisible = false;
                stateUI.doesPlayerAlreayUseOneLife = true;
            }
        }
    }
    //public void GetHeal(int heal)
    //{

    //    stats.Heal(heal);
    //    stateUI.Refresh();
    //    stateUIFixe.Refresh();//added yohan
    //    //stateUI.RefreshPlayerAlpha();
    //    if (stats.HP == 0 && IsAlive)
    //    {
    //        OnDeath();
    //    }
    //    if (gm.showDamageText)
    //    {
    //        var gObj = Instantiate(damageTextPrefab, transform.position, Quaternion.identity) as GameObject;
    //        DamageText dt = gObj.GetComponent<DamageText>();
    //        dt.Init(heal, Color.green);
    //    }
    //    audioManager.Play("Heal1");
    //}

    public void DirectionIndication()
    {
        var sprite = directionIndication.transform.Find("DirectionSprite");
        Vector3 direction = input.GetDirection();
        Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);
        direction = direction.normalized;

        //Debug.Log(direction);
        if (direction.magnitude == 0)
        {

            if (transparency > 0)
            {
                transparency -= 0.1f;
            }
            if (directionIndicationSprite.transform.localScale.x < 2)
            {
                directionIndicationSprite.transform.localScale += scale;
            }

        }
        else
        {
            if (transparency < 1)
            {
                transparency += 0.1f;
            }
            if (directionIndicationSprite.transform.localScale.x > 1)
            {
                directionIndicationSprite.transform.localScale -= scale;
            }
            directionIndication.right = direction;
            if (directionIndication.localEulerAngles.y != 0)
            {
                directionIndication.localEulerAngles = new Vector3(0, 0, 180);
            }

        }
        directionIndicationSprite.color = new Color(0, 0.7f, 1, transparency);
    }

    //void SmokeDirection()
    //{
    //    Vector3 direction = input.GetDirection();
    //    direction = direction.normalized;
    //    foreach (GameObject smoke in runSmokeEffect)
    //    {
    //        smoke.transform.right = direction;
    //    }
    //}
    
    public override bool HitCheck(Bullet bullet){
        if (isInvincible == false)
        {
            ICharacter character = bullet.Owner;
            if (character != null && character.IsAlive && character.GetGameObject().name!="Player"/*character.GetGameObject().tag != "Player"*/)
            {
                int damage = color == 
                    bullet.BulletColor ? bullet.Damage : bullet.Damage * Static.colorDmgMultiplier;
                GetHit(damage * bullet.Owner.GetStats().Attack);//yohan added
                return true;
            }
        }
        return false;
    }

    protected override void OnDeath(){
        justOnceDisablePlayerBody = false;
        jsutOnceInstantiateGameOverPanel = false;
        StartCoroutine(PlayerDieAnim(1.0f));
        //DisplayGameOverPanel();
        //IsAlive = false;
        //stateUI.OnDeath();
        //base.OnDeath();
        //gameObject.SetActive(false);
    }
    void DisplayGameOverPanel()
    {
        
        audioManager.Pause("MainBgm");
        audioManager.Pause("BossBgm");
        if (inventory.Life >= 1)
        {
            if (stateUI.doesPlayerAlreayUseOneLife == false)
            {
                stateUI.EnabledGameOverPanel();
                stateUI.doesPlayerAlreayUseOneLife = true;
                //GetComponent<PlayerCombatV1>().enabled = false;
            }
            else if (inventory.Life == 0)
            {
                InstantiateGameFinishPanel();
            }
        }
        else if (inventory.Life == 0)
        {
            if (stateUI.doesPlayerAlreayUseOneLife == false)
            {
                stateUI.EnableWatchAdvertissementPanel();
                GetInventory().AddLife(1);
                stateUI.doesPlayerAlreayUseOneLife = true;
                //GetComponent<PlayerCombatV1>().enabled = false;
            }
            else
            {
                InstantiateGameFinishPanel();
            }
        }
    }

    void InstantiateGameFinishPanel()
    {
        gameFinishPanel = Resources.Load<GameObject>("Prefabs/StateUIs/GameFinishPanel");
        Instantiate(gameFinishPanel, Vector3.zero, Quaternion.identity);

        //hubSaveData.credits = inventory.Credits;
        //stats.SetHP(SaveSystem.LoadStats().maxHP);
        //SaveSystem.Savehubsavedata(hubSaveData);
    }

    public IEnumerator PlayerDieAnim(float duration)
    {
        float time = 0.0f;
        //bool justOnceBossName = false;
        while (time < duration + 2)
        {
            if (time < duration)
            {
                if (justOnceDisablePlayerBody == false)
                {
                    GetComponent<MovementV1>().enabled = false;
                    GetComponent<PlayerCombatV1>().enabled = false;
                    GetComponent<BoxCollider2D>().enabled = false;
                    deadEffectObj.SetActive(true);
                    deadEffectAnimObj.SetActive(true);

                    foreach (GameObject partOfBody in playerMainBody)
                    {
                        partOfBody.SetActive(false);
                    }
                    playerDeadBody.SetActive(true);
                    justOnceDisablePlayerBody = true;
                }
            }
            else
            {
                if (jsutOnceInstantiateGameOverPanel == false)
                {
                    DisplayGameOverPanel();
                    jsutOnceInstantiateGameOverPanel = true;
                }

                //diedAnimKindOfParticle.SetActive(false);
                //GetComponent<MovementV1>().enabled = true;
                //GetComponent<PlayerCombatV1>().enabled = true;
            }
            time += Time.deltaTime;
            yield return null;
        }
    }


    public override void OnEnemyDeath(ICharacter enemy){
        if (isInvincible == false)// the player does not gain energy if he kills an enemy in the melee mode
        {
            stats.GainEnergy(enemy.GetDrops().energy);
            Instantiate(gainEnergyEffect, transform.position, Quaternion.identity);
        }
        //inventory.AddCredits(enemy.GetDrops().credits);
        stateUI.Refresh();
        stateUIFixe.Refresh();//addedYohan
        //stateUIFixe.UpdateCredits();//addedYohan
    }

    //stupid code
    public void GetUpdateCredits()
    {
        stateUIFixe.UpdateCredits();
    }

    ///////// kim added
    ///
    public bool AddWeapon(Weapon weap, int socketCount)
    {
        int[] validSocketCount = new int[] { 1, 2 };
        if (!validSocketCount.Contains(socketCount))
        {
            Log.log("Invalid number of sockets");
            return false;
        }
        var sockets = socketCount == 1 ? singleSocket : dualWieldSockets;
        weap.Init(this, sockets);
        base.AddWeapon(weap);
        inventoryUI.Init(this, inventory);
        return true;
    }



    ///////// kim added
    public void AddCredits(int amount)
    {
        amount = 100;
        inventory.AddCredits(amount);
        stateUIFixe.UpdateCredits(); //addedDohan
    }
    public void RemoveCredits(int amount)
    {
        inventory.RemoveCredits(amount);
        stateUIFixe.UpdateCredits();
    }

    public void ButtonDown()
    {

        BroadcastColorSwitch(1);
    }

 



    /*
    int getWeaponIndex(Weapon weapon)
    {

        Debug.Log("Weapon index compare start-----------------------");
        for (int x = 0; x < 9; x++)
        {
            if (weapon == inventory.Weapons[x])
            {
                Debug.Log("subject=" + weapon + "inventory weapon" + x + inventory.Weapons[x] + "Match!!!!!!!!!!!!!!!!");
                Debug.Log("Weapon index compare end-----------------------");
                return x;
            }

            Debug.Log("subject=" + weapon + "inventory weapon" + x + inventory.Weapons[x] + "No Match");
        }
        Debug.Log("can`t get weapons index");
        Debug.Log("Weapon index compare end-----------------------");
        return 0;
    }
    //kim added 
    void SetAndSaveData(Collider2D col)
    {
        // Debug.Log(col.transform.position + "" + autoSaveData + " " + stats.Energy);

        AutoSaveData data = new AutoSaveData(
            MapManagerObj.GetComponent<MapManager>().phase,
            getWeaponIndex(playerComb.weapons[0]),
            getWeaponIndex(playerComb.weapons[1]),
            playerComb.weapons[0].doesItHavePoisonBullet
            ,
            playerComb.doesBonusMineIsActive,
            playerComb.weapons[0].doesItHaveDoubleShot,
            MapManagerObj.GetComponent<MapManager>().enemyDeadCount
            , stats.Energy
            , stats.HP, stateUI.doesPlayerAlreayUseOneLife
            ,
            inventory.Life,
            inventory.Credits
            );

        SaveSystem.SaveAutoSaveData(data);
        hubSaveData.credits = inventory.Credits;//temp
        SaveSystem.Savehubsavedata(hubSaveData);//temp
       // Debug.Log("auto save point reached. autosaved. phase=" + data.phase);


    }
    void LoadAndSetPlayer(AutoSaveData data)
    {

        //1. set player position DONE

        GameObject spawnarea = GameObject.Find("playerSpawnphase" + (data.phase + 1).ToString()); ;

        transform.position = spawnarea.transform.position;

        // set enemy deadcount DONE
        
        MapManagerObj.GetComponent<MapManager>().enemyDeadCount = data.deadcount;
       // Debug.Log("EnemyDeadCount="+data.deadcount);
       // Debug.Log(MapManagerObj.GetComponent<MapManager>().enemyDeadCount);


        // set weapons DONE
        Weapon unusedparameter = inventory.Weapons[1];
        Debug.Log("weapon1index=" + data.weapon1index);
        OnChooseWeapon(data.weapon1index, unusedparameter);
        OnChooseWeapon2(data.weapon2index, unusedparameter);



        // set powerups (mostly bools) DONE
        if (data.MinePowerUp)
        {
            playerComb.doesBonusMineIsActive = true;

        }
        if (data.PoisonPowerUp)
        {
            playerComb.weapons[0].doesItHavePoisonBullet = true;
        }
        if (data.doubleShot)
        {
            playerComb.weapons[0].doesItHaveDoubleShot = true;
        }

        stats.SetHP(SaveSystem.LoadAutoSaveData().currenthp);

        // set energy DONE
        stats.setEnergy(data.eng);
        //Debug.Log("Savefile energy=" + data.eng + "current energy=" + stats.Energy);
       // Debug.Log("PlayerDataLoaded");




    }
    private void CreateAutoSaveData()
    {
        autoSaveData = new AutoSaveData(0, 0, 0, false, false,false, 0, 0, SaveSystem.LoadStats().HP,false,0,0);
        SaveSystem.SaveAutoSaveData(autoSaveData);
        //LoadAndSetPlayer(autoSaveData);
        Debug.Log("created new autosave data");
        //Debug.Log("save file doesn`t exist, created new savedata");
    }*/
    //----------------------------------------
    // these 3 methods are duplicate from playercombatv1.
    // 
    //----------------------------------------
    //void ActivateWeapon2(Weapon weapon2)
    //{
    //    weapon2.Init(this, singleSocket2);
    //    AddWeapon(weapon2);
    //    ActivateAllWeapons(weapon2);
    //}
    //public void OnChooseWeapon(int index, Weapon weapon) //note from dohan- unused parameter (Weapon weapon)
    //{
    //    switch (index)
    //    {
    //        case 8:
    //            playerComb.weapons[0] = inventory.Weapons[8];//TripleSingleShooter
    //            ActivateWeapon(inventory.Weapons[8]);
    //            break;
    //        case 3:
    //            playerComb.weapons[0] = inventory.Weapons[3];//Rifle
    //            ActivateWeapon(inventory.Weapons[3]);
    //            break;
    //        case 2:
    //            //ShotGun
    //            ActivateWeapon(inventory.Weapons[2]);
    //            break;
    //        //case 0:
    //        //    playerComb.weapons[0] = inventory.Weapons[0];//ShotGun
    //        //    ActivateWeapon(playerComb.weapons[0]);
    //        //    break;
    //        default:
    //            Debug.Log("weapon index not matching onchooseWeapon index");
    //            break;


    //    }

    //}
    //public void OnChooseWeapon2(int index, Weapon weapon)
    //{
    //    switch (index)
    //    {
    //        case 4:
    //            playerComb.weapons[1] = new TheMiddleFingerGun() /*inventory.Weapons[4]*/;//TheMiddleFingerGun
    //            ActivateWeapon2(/*playerComb.weapons[1]*/new TheMiddleFingerGun());
    //            break;
    //        case 5:
    //            playerComb.weapons[1] = new TheEraser() /*inventory.Weapons[5]*/;//TheEraser
    //            ActivateWeapon2(new TheEraser());

    //            GetStats().ModifySpeed(-1.0f);
    //            break;
    //        case 9:
    //            playerComb.weapons[1] = new LaserSurgeryGun()/*inventory.Weapons[9]*/;//LaserSurgeryGun
    //            ActivateWeapon2(new LaserSurgeryGun());
    //            break;
    //        default:
    //            Debug.Log("weapon index not matching onchooseWeapon index");
    //            break;
    //    }

    //}
    //public void setStats(Stats newStats){
    //    stats=newStats;

    //}


}
