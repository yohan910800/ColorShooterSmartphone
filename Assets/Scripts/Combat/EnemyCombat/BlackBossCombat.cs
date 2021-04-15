using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using Random = UnityEngine.Random;

public class BlackBossCombat : EnemyCombatV1
{

    //to do 
    //nned to set a specific timer for the phase1

    public Colors[] colors;
    public int[] ratios;

    public int velocityCounter = -1;
    public int phase;
    public bool isTurning;

    float phaseDuration;

    float speedAccelerator;

    private IEnumerator coroutine;
    private IEnumerator coroutine2;

   public  GameObject turnObject;// used to turn tentacle when activated
     
   Quaternion turnObjOriginRot;

    Transform[][] sockeColl;// take the weapon socket
    float timerTurn;
    float timerSpawnMinienemy;
    int rnd;
    GameManager gm;
    GameObject miniEnemy;

    public override void Init(ICharacter character)
    {
        base.Init(character);
        isTurning = false;
        foreach (Colors c in colors)
        {
            this.character.AddColor(c);
        }
        character.SetBulletColor(colors[0]);

        phase = 1;// begin on the tentacle attack
        phaseDuration = 20; 
        turnObject = new GameObject();
        turnObject.transform.position=GetComponent<BlackBossInput>().originPos;
        turnObject.name = "TurnObject";
        turnObjOriginRot = turnObject.transform.rotation;

        //target = GameObject.FindWithTag("Player");

        weapon = character.GetActiveWeapon();
        character.OnWeaponChange += OnWeaponChange;
        //aimDir = Vector3.down;

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        CreateNewWeapon();//create a new weapon in the inventory

        miniEnemy =Resources.Load<GameObject>("Prefabs/Characters/MeleeAttackPathFinding");

        phase = character.phase;
    }
    void Start()
    {
        PatternTentacleAttack();
    }
    void CreateNewWeapon()
    {
        weapon = new BlackBossAttack();

        var weaponSocket3 = transform.Find("Mouth");
        sockeColl = new Transform[][]{
            new Transform[]{weaponSocket3},
        };
        Weapon weap = new BlackBossAttack() as Weapon;
        weap.Init(character, sockeColl[0]);
        character.AddWeapon(weap);
    }

    void OnEquipeTentacleAttack()// change weapon 
    {
        OnWeaponChange(character.GetInventory().Weapons[0]);
        character.ActivateWeapon(character.GetInventory().Weapons[0]);
    }
    void OnEquipeMeleeMode()
    {
        OnWeaponChange(character.GetInventory().Weapons[1]);
        character.ActivateWeapon(character.GetInventory().Weapons[1]);
    }

    public override void Update()
    {
        //phase = 2;
        SetPhase();
        ChoosePhase();
        AttackPhase1();
        AttackPhase2();
    }
    void SetPhase()
    {
        character.phase = phase;
    }

    void ChoosePhase()// chose a phase every 10 seconde randomely
    {
        Log.log("Phase" + phase);
        phaseDuration += Time.deltaTime;
        
        if (phaseDuration >= 20)
        {
            rnd = Random.Range(1, 4);
            if (rnd == phase)
            {
                rnd = Random.Range(1, 4);
            }
            else
            {
                phase = rnd;
                shotCount = 0;
                phaseDuration = 0;
                ResetPhase2();
                timerTurn = 0;
            }
        }
        switch (phase)
        {
            case 1 :
                PatternTentacleAttack();
                character.GetStats().SetAttack(1);
                break;
            case 2 :
                PatternMeleeAttack();
                character.GetStats().SetAttack(5);
                break;
            case 3 :
                PatternMiniEnemiesAttack();
                break;
        }
    }
    void PatternTentacleAttack()
    {
        OnEquipeTentacleAttack();
        //character.ActivateAutoAim = false;
        Log.log("Active weapon " + character.GetActiveWeapon());
        timerTurn += Time.deltaTime;
        if (timerTurn >= 5)
        {
            isTurning = true;
        }
        if (isTurning == true)
        {
            TurnTentacles();
        }
    }
    void PatternMeleeAttack()
    {
        OnEquipeMeleeMode();
        character.ActivateAutoAim = true;
        Log.log("Active weapon " + character.GetActiveWeapon());
    }
    void PatternMiniEnemiesAttack()
    {
        character.ActivateWeapon(null);
        timerSpawnMinienemy += Time.deltaTime;
        if (timerSpawnMinienemy>2)
        {
            ICharacter enemy = Instantiate
                (miniEnemy, transform.position, Quaternion.identity).
                GetComponent<ICharacter>();
            gm.AddEnemy(enemy);
            enemy.SetColor(Colors.Black);
            enemy.Init();

            enemy.GetStats().SetSpeed(4);
            enemy.GetStats().SetMaxHP(200);
            enemy.GetStats().SetHP(200);
            enemy.GetGameObject().tag = "Boss";
            timerSpawnMinienemy = 0;
        }
    }
    
    void AttackPhase1()
    {
        if (phase == 1) { 
        if (target == null) return;
            if (character.ActivateAutoAim == true)
            {
                if (shotCount < 5)
                {
                    Aim();
                    if (weapon.IsReloading)
                    {
                        reloadCountdown -= Time.deltaTime;
                        if (reloadCountdown <= 0)
                        {
                            weapon.IsReloading = false;
                        }
                        else
                        {
                            return;
                        }
                    }
                    charge += Time.deltaTime * weapon.FireRate;
                    if (charge >= fullCharge)
                    {
                        charge = 0;
                        Log.log("the enemy shot ");
                        if (Shoot()) charge = 0;
                    }
                }
            }
        }
    }
    void AttackPhase2()
    {
        if (phase == 2) { 
        if (target == null) return;
            if (character.ActivateAutoAim == true)
            {
                Aim();
                if (weapon.IsReloading)
                {
                    reloadCountdown -= Time.deltaTime;
                    if (reloadCountdown <= 0)
                    {
                        weapon.IsReloading = false;
                    }
                    else
                    {
                        return;
                    }
                }
                charge += Time.deltaTime * weapon.FireRate;
                if (charge >= fullCharge)
                {
                    charge = 0;
                    Log.log("the enemy shot ");
                    if (Shoot()) charge = 0;
                }
            }
        }
    }


    void SetRandomBulletColor()
    {
        int rnd = Random.Range(0, 100);
        int lowLimit = 0;
        for (int i = 0; i < ratios.Length; i++)
        {
            int limit = lowLimit + ratios[i];
            if (rnd >= lowLimit && rnd < limit)
            {
                character.SetBulletColor(colors[i]);
                return;
            }
            lowLimit = limit;
        }
    }

    protected override bool Shoot()
    {
        if (shotCount < 5&&phase==1)
        {
            if (shotCount == 5)
            {
                shotCount = 0;
            }
            if (colors.Length > 1) SetRandomBulletColor();
            return base.Shoot();
        }
        else
        {
            if (colors.Length > 1) SetRandomBulletColor();
            return base.Shoot();
            //return false;
        }
    }
    public int GetShotCount()
    {
        return shotCount;
    }
   
    void TurnTentacles()
    {
        speedAccelerator += 0.05f;
        turnObject.transform.Rotate(Vector3.forward * (20 + speedAccelerator) * Time.deltaTime);
    }
    
    void ResetPhase2()
    {
        foreach(Transform child in turnObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        velocityCounter = -1;
        speedAccelerator = 0;
        isTurning = false;
        turnObject.transform.rotation = turnObjOriginRot;
    }
}
