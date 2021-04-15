using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using Random = UnityEngine.Random;

public class GRP4FusinedBossCombat : EnemyCombatV1
{
    public Colors[] colors;
    public int[] ratios;

    public List<GameObject> targetList;
    public List<GameObject> targetListFX;

    bool isTalking;
    bool isAiming;
    bool meleeMode;
    bool weaponForChangedMelee;

    GameManager gm;
    float timer;
    public int phase;
    IInputModule input;
    Weapon originWeapon;

    Inventory inventory;
    TestEnemy bossScript;

    GameObject aliveBullet;
    float timerSpawnAliveBullet;
    int numberOfShoot;
    bool shootOnce;

    Transform[] originSocket;
    Transform[][] sockeColl;// take the weapon socket

    public override void Init(ICharacter character)
    {
        base.Init(character);
        foreach (Colors c in colors)
        {
            this.character.AddColor(c);
        }
        character.SetBulletColor(colors[0]);

        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        bossScript = GetComponent<TestEnemy>();
        input = character.GetInputModule();
        weapon = character.GetActiveWeapon();
        inventory = character.GetInventory();
        character.OnWeaponChange += OnWeaponChange;
        aimDir = Vector3.down;

        phase = character.phase;

        CreateWeapon();
        shootOnce = false;
    }
    void CreateWeapon()
    {
        weapon = new BigAOEAttack();
        var weaponSocket3 = transform.Find("Hand1");
        sockeColl = new Transform[][]{
            new Transform[]{weaponSocket3},
        };

        Weapon weap = new BigAOEAttack() as Weapon;

        weap.Init(character, sockeColl[0]);
        character.AddWeapon(weap);
    }
    public override void Update()
    {
        Log.log("is allow to attack" + character.isItAllowToAttack);
        
        ResetPhase3();
        phase = character.phase;
        switch (phase)
        {
            case 0:
                break;
            case 1:

                break;
            case 2:
                if (character.isItAllowToAttack == true)
                {
                    OnBigAOEAttack();
                }
                break;
            case 3:
                //this.character.ActivateWeapon(null);
                OnShootAliveBullet();
                break;
            case 4:
                if (character.isItAllowToAttack == true)
                {
                    OnEquipeMeleeAttack();
                    Shoot();
                    character.isItAllowToAttack = false;
                }
                break;
        }
    }
    void ResetPhase3()
    {
        if (phase != 2)
        {
            shootOnce = false;
        }
    }

    void OnBigAOEAttack()
    {
        if (shootOnce == false)
        {
            OnEquipeAOEAttack();
            Shoot();
            shootOnce = true;
        }
    }

    void OnEquipeAOEAttack()
    {
        OnWeaponChange(character.GetInventory().Weapons[0]);
        this.character.ActivateWeapon(character.GetInventory().Weapons[0]);
    }
    void OnEquipeMeleeAttack()
    {
        OnWeaponChange(character.GetInventory().Weapons[1]);
        this.character.ActivateWeapon(character.GetInventory().Weapons[1]);
    }

    void OnShootAliveBullet()
    {
        //Log.log("number of shoot " +numberOfShoot);
        if (numberOfShoot < 4)
        {
            timerSpawnAliveBullet += Time.deltaTime;
            if (timerSpawnAliveBullet > 2)
            {
                numberOfShoot++;
                aliveBullet = Resources.Load<GameObject>("Prefabs/Bullets/GRAliveBullet");

                ICharacter enemy = Instantiate
                    (aliveBullet, transform.position, Quaternion.identity).GetComponent<ICharacter>();
                gm.AddEnemy(enemy);
                enemy.SetColor(Colors.Green);
                
                enemy.Init();
                enemy.GetStats().SetMaxHP(1000);
                enemy.GetStats().SetHP(1000);
                enemy.GetStats().SetSpeed(2);
                
                timerSpawnAliveBullet = 0;
            }
            
        }
    }
    void ResetShootAliveBullet()
    {
        if (numberOfShoot == 4)
        {
            numberOfShoot = 0;
        }
    }

    void AttackWithWeapon()
    {
        if (target == null) return;
        Log.log("Weapon" +character.GetActiveWeapon());

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
            if (Shoot()) charge = 0;
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
        if (colors.Length > 1) SetRandomBulletColor();
        return base.Shoot();
    }
    public int GetShotCount()
    {
        return shotCount;
    }
}
