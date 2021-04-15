using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System;
using UnityEngine.SceneManagement;

public class Ranged1 : Character {

    public string jsonFileDialogName;
    public FloatingDialogManager floatingDialog;
    public GameObject kindOfBlood;//yohan added

    GameObject gObj;

    [Range(1,3)]
    public int socketNo;
    public GruntWeapon weapon;
    public bool isAimedByPlayer;// yohan added

     float flashTime=0.1f;//yohan added

    bool activateGotHitTimer;//yohan added
    float gotHitTimer;//yohan added

    SpriteRenderer bodySprite;// yohan added
    Color originalColor;//yohan added

    //GameObject poisonZone;
    GameObject deadBody;
    //GameObject dropEnergyEffect;

    //bonus
    bool justOncePoison;

    GameObject dropCoinsObj;

    public enum GruntWeapon{
        SingleShooter,
        SingleBigShot,
        AutomaticRifle,
        Bazooka,
        TrapezoidAreaShooter,
        LifeStealer,
        MissileShooter,
        Tentacle,
        ChargeWeapon,
        NoWeapon,
        MeleeMode,
        BlackBossAttack,
        QuickBazooka,// green boss 1
        BigAOEAttack,//green fusined boss
        LongZoneMeleeAttack,// green fusioned boss
        LaseSurgeryGun,
        TheMiddleFingerGun,
        ShotGun,
        TheEraser,
        NoHandBossUltimateAttack,
        LastBossMeleeMode,
        SingleShooterWithBulletColor,
        FireBulletShooter,
        FireCharge


    }
    Transform[][] sockeColl;

    public override void Init(){
        base.Init();

        
        if (gameObject.tag != "FourHandsBoss" && gameObject.tag!="OneHandBoss"&& gameObject.tag != "DualYellowBoss")
        {
            var weaponSocket1 = transform.Find("Hand1");
            var weaponSocket2 = transform.Find("Hand2");
            var weaponSocket3 = transform.Find("Mouth");//add mouth
            sockeColl = new Transform[][]{

                new Transform[]{weaponSocket2},
                new Transform[]{weaponSocket1,weaponSocket2},
                new Transform[] { weaponSocket3 },// add new weapon socket
            };
            Weapon weap = Activator.CreateInstance(Type.GetType(weapon.ToString())) as Weapon;
            weap.Init(this, sockeColl[socketNo - 1]);
            AddWeapon(weap);
            ActivateWeapon(inventory.Weapons[0]);
        }

        bodySprite = transform.Find("Body").GetComponent<SpriteRenderer>();//yohan added
        originalColor = bodySprite.color;



        // bonus

        //poisonZone = Resources.Load<GameObject>("Prefabs/Bonus/poisonZone");
        kindOfBlood = Resources.Load<GameObject>("Prefabs/Effects/KindOfBloodEffect");
        InitDeadBody();
        //dropEnergyEffect= Resources.Load<GameObject>("Prefabs/Effects/DropEnergyEffect");
        //DeadBody.transform.Find("Body").GetComponent<SpriteRenderer>().color = originalColor;
        //deadBody.GetComponentInChildren<SpriteRenderer>().color = originalColor;
        //foreach(SpriteRenderer sprite in deadBody.GetComponentsInChildren<SpriteRenderer>())
        //{
        //    sprite.color = originalColor;
        //}
        dropCoinsObj= Resources.Load<GameObject>("Prefabs/CoinGroup");

    }

    void InitDeadBody()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "EnemyTest":
                deadBody = Resources.Load<GameObject>("Prefabs/DeadBodyCharacter/RedDeadBody");
                break;
            case "Tutorial":
                deadBody = Resources.Load<GameObject>("Prefabs/DeadBodyCharacter/RedDeadBody");
                break;
            case "Stage1":
                deadBody = Resources.Load<GameObject>("Prefabs/DeadBodyCharacter/PinkDeadBody");
                break;

            case "Stage2":
                break;
            case "Stage3":
                break;
            case "Stage4":
                break;
            case "Stage5":
                break;

        }

    }

    protected override void InitInput(){
        base.InitInput();
    }
    //yohan added
    protected override void Update()
    {
        
        base.Update();

        OnNotMovingWhenHitTimer();
        //bonus 
        OnPoisonCharacter();
    }


    void FlashRed()
    {
        bodySprite.color = Color.red;

        Color tmp = bodySprite.color;

        tmp.r = 0.4f;
        tmp.g = 0.4f;

        tmp.b = 0.4f;

        tmp.a = 1.0f;

        bodySprite.color = tmp;
        Invoke("ResetColor", flashTime);
    }
    void ResetColor()
    {
        bodySprite.color = originalColor;
    }

    

    //yohan added
    void OnNotMovingWhenHitTimer()
    {
        //if (gameObject.tag != "Boss" || gameObject.tag != "MiniBoss")
        //{

            if (activateGotHitTimer == true)
            {
                
                gotHitTimer += Time.deltaTime;
            //stats.ModifySpeed(-2.0f);
                if (gotHitTimer >= 0.5f)
                {
                    stats.ResetSpeed();
                
                activateGotHitTimer = false;
                    gotHitTimer = 0.0f;
                }
            }
        //}
        //else
        //{
        //    return;
        //}
       
    }

    public void GetHit(int dmg){

        activateGotHitTimer = true;//yohan added
        stats.LowerHP(dmg);
        stateUI.Refresh();

        if (stats.HP == 0 && IsAlive){
            OnDeath();
        }
        if(gm.showDamageText){
            var gObj = Instantiate(damageTextPrefab,transform.position,Quaternion.identity) as GameObject;
            DamageText dt = gObj.GetComponent<DamageText>();
            
            dt.Init(dmg, Color.white);

        }

        FlashRed();//yohan added
        OnInstantiateKindOfBlood();//yohan added
        PlayGetHitSound();

    }
    void PlayGetHitSound()
    {
        FindObjectOfType<AudioManager>().Play("Impact4");
    }

    //yohan added
    void OnInstantiateKindOfBlood()
    {
        GameObject obj=Instantiate(kindOfBlood, transform.position, Quaternion.identity);
        Destroy(obj, 1.0f);
    }
    

    public void GetHeal(int heal)
    {
        stats.Heal(heal);
        stateUI.Refresh();
        if (stats.HP == 0 && IsAlive)
        {
            OnDeath();
        }
        if (gm.showDamageText)
        {
            var gObj = Instantiate(damageTextPrefab, transform.position, Quaternion.identity) as GameObject;
            DamageText dt = gObj.GetComponent<DamageText>();
            dt.Init(heal, Color.green);
        }
        
        audioManager.Play("Heal1");
    }

    //imported from testdialogmanager
    void ActiveDialogWithoutTrigger()
     {
        floatingDialog.NewDialog(jsonFileDialogName);
        floatingDialog.OnDialogEnded += OnDialogEnd;
     }

    //instantiate the dialog text
    public void ShowDialogText()
    {
         float dist = Vector3.Distance(transform.position, gm.player.transform.position);

        if (gm.showDialogText == true)
        {
                  gObj = Instantiate(dialogTextPrefab, transform.position, Quaternion.identity) as GameObject;
                floatingDialog = gObj.GetComponent<FloatingDialogManager>();
                //dialog.Init("Test", Color.white);
        }
    }

    public void TextFollowEnemy()
    {
        if (gObj != null)
        {
            gObj.transform.position = transform.position;
        }
    }
    public void ActivateDialogFromMapManager()
    {
        ShowDialogText();
        ActiveDialogWithoutTrigger();
    }

    void OnDialogEnd()
    {
        if (floatingDialog != null)
        {
            floatingDialog.OnDialogEnded -= OnDialogEnd;
        }

    }
    void DisableAttackOnTag()
    {
        if (gameObject.tag == "NotAimable")
        {

        }
        else
        {

        }
    }
    //bonus


    void OnPoisonCharacter()
    {
        if (isPoisoned == true)
        {
            if (justOncePoison == false)
            {

                //GameObject poisonZoneObj = MonoBehaviour.Instantiate(poisonZone, transform.position,
                //            Quaternion.identity);
                //poisonZoneObj.transform.parent = gameObject.transform;

                //Log.log("name " + poisonZone.name);
                StartCoroutine(PoisonCoroutine(4.0f));
                justOncePoison = true;

            }
        }
    } 

    void GetHitByDot(int dmg, Color dotColor)
    {
        stats.LowerHP(dmg);
        stateUI.Refresh();
        if (stats.HP == 0 && IsAlive)
        {
            OnDeath();
        }
        if (gm.showDamageText)
        {
            var gObj = Instantiate(damageTextPrefab, transform.position, Quaternion.identity) as GameObject;
            DamageText dt = gObj.GetComponent<DamageText>();

            dt.Init(dmg, dotColor);

        }
    }

    private IEnumerator PoisonCoroutine(float duration)
    {
        float time = 0.0f;
        float time2 = 0.0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            time2 += Time.deltaTime;
            if (time2 > 0.5f)
            {
                GetHitByDot(2,Color.magenta);

                time2 = 0;

            }

            // transform.position = currentpos;
            
            yield return null;
        }
    }


    public override bool HitCheck(Bullet bullet){
        ICharacter character = bullet.Owner;
        if (character.GetGameObject().name == "Player")
        {
            // int damage  = color == bullet.BulletColor ? bullet.Damage : bullet.Damage * Static.colorDmgMultiplier;
            int damage = bullet.Damage;
            GetHit(damage);//yohan added

            return true;
        }
        return false;
    }

    void CalculateHowManyyCoinsShouldBeDroped()
    {
        if (GetStats().maxHP < 100)
        {
            Instantiate(dropCoinsObj, transform.position, Quaternion.identity);
        }
            int coinAmountIndex = GetStats().maxHP / 100;
        for (int i = 0; i < coinAmountIndex; i++)
        {
            GameObject coinGroupObj=Instantiate(dropCoinsObj, transform.position, Quaternion.identity);
            var rotationVector = coinGroupObj.transform.rotation.eulerAngles;
            rotationVector.z = i*90;
            coinGroupObj.transform.rotation = Quaternion.Euler(rotationVector);

            coinGroupObj.transform.position += new Vector3(i, -i, i);
        }
    }

    protected override void OnDeath(){
        
        if (isAimedByPlayer == true)
        {
            Destroy(GameObject.Find("TargetCircle(Clone)"));
        }
        Instantiate(deadBody, transform.position,Quaternion.identity);
        CalculateHowManyyCoinsShouldBeDroped();
        //Instantiate(dropEnergyEffect, transform.position,Quaternion.identity);
        IsAlive = false;
        stateUI.OnDeath();
        base.OnDeath();
        
        if (floatingDialog != null)
        {
            floatingDialog.OnDialogEnded -= OnDialogEnd;
        }

        audioManager.Play("EnemyDie");
        Destroy(gameObject);
    }

}
