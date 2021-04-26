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

    Transform directionIndication;
    const float directionIndicationDistance = 4;
    Transform sprite;

    SpriteRenderer directionIndicationSprite;
    Color dIColor;
    GameObject gainEnergyEffect;
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
        justOnceSmoke = new bool[2];

        //7. etc 

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //8. initiated! (needed for cameraControl. )
        initiated = true;
        stateUII = GameObject.Find("PlayerStateUI(Clone)");
        stateUII.GetComponent<StateUI>().Refresh();
        //bonus
        justOnceSmoke = new bool[2];
        
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
    protected override void Update() {

        input.Update();
        DirectionIndication();
        PlayRunSoundAndSmokeEffect();
        DisableActiveInvisible();
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

    public void DirectionIndication()
    {
        var sprite = directionIndication.transform.Find("DirectionSprite");
        Vector3 direction = input.GetDirection();
        Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);
        direction = direction.normalized;

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
    
    public override bool HitCheck(Bullet bullet){
        if (isInvincible == false)
        {
            ICharacter character = bullet.Owner;
            if (character != null && character.IsAlive && character.GetGameObject().name!="Player")
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
    }

    public IEnumerator PlayerDieAnim(float duration)
    {
        float time = 0.0f;
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
        stateUI.Refresh();
        stateUIFixe.Refresh();//addedYohan
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
}
