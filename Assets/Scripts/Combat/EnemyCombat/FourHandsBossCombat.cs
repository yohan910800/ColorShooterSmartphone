using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class FourHandsBossCombat : EnemyCombatV1
{
    public Colors[] colors;
    public int[] ratios;

    float charge2;
    float charge3;
    float charge4;

    float timerChangeColor;
    float phaseTimer;

    Vector3 curentPos;

    public GameObject weaponTarget;
    public GameObject weaponTarget2;
    public GameObject weaponTarget3;
    public GameObject weaponTarget4;

    public GameObject handsContainer;
    Transform[][] sockeColl;
    Transform[] Hand1;
    Transform[] Hand2;
    Transform[] Hand3;
    Transform[] Hand4;
    Transform[] Hand5;
    Transform[] FourHands;

    Weapon weap3;
    Weapon weap;
    Weapon weap2;
    Weapon weap4;

    int phase;
    public override void Init(ICharacter character)
    {
        base.Init(character);
        foreach (Colors c in colors)
        {
            this.character.AddColor(c);
        }
        character.SetBulletColor(colors[0]);
        

        weapon = character.GetActiveWeapon();
        character.OnWeaponChange += OnWeaponChange;
        aimDir = Vector3.down;

        CreateNewWeapon();
        EquipeAllWeapons();
        phase = 0;
        StartCoroutine(TimeBeforeBegin(5.0f));
    }
    void CreateNewWeapon()
    {
        //weapon = new LaserSurgeryGun();
        var weaponSocket1 = transform.Find("TurnObject1").Find("Hand1");
        var weaponSocket2 = transform.Find("TurnObject1").Find("Hand2");
        var weaponSocket3 = transform.Find("TurnObject1").Find("Hand3");//add mouth
        var weaponSocket4 = transform.Find("TurnObject1").Find("Hand4");//add mouth
        var weaponSocket5 = transform.Find("Mouth");//add mouth
        sockeColl = new Transform[][]{

            Hand2=new Transform[]{weaponSocket2},
            Hand1=new Transform[]{weaponSocket1},
            Hand3=new Transform[]{weaponSocket3},
            Hand4=new Transform[]{weaponSocket4},

            new Transform[]{weaponSocket1,weaponSocket2},
            new Transform[] { weaponSocket3 },// add new weapon socket
            FourHands=new Transform[] { weaponSocket1, weaponSocket2,weaponSocket3,
                weaponSocket4 }// add new weapon socket
        };

        weap = new LaserSurgeryGun() as Weapon;
        weap.Init(character, Hand1);
        character.AddWeapon(weap);

        weap2 = new Shotgun() as Weapon;
        weap2.Init(character, Hand2);
        character.AddWeapon(weap2);

        weap3 = new TheEraser() as Weapon;
        weap3.Init(character, Hand3);
        character.AddWeapon(weap3);

        weap4 = new TheMiddleFingerGun() as Weapon;
        weap4.Init(character, Hand4);
        character.AddWeapon(weap4);
    }

    void EquipeAllWeapons()
    {
        OnWeaponChange(character.GetInventory().Weapons[0]);
        character.ActivateAllWeapons(character.GetInventory().Weapons[0]);

        OnWeaponChange(character.GetInventory().Weapons[1]);
        character.ActivateAllWeapons(character.GetInventory().Weapons[1]);

        OnWeaponChange(character.GetInventory().Weapons[2]);
        character.ActivateAllWeapons(character.GetInventory().Weapons[2]);

        OnWeaponChange(character.GetInventory().Weapons[3]);
        character.ActivateAllWeapons(character.GetInventory().Weapons[3]);
    }
    IEnumerator TimeBeforeBegin(float duration)
    {
        float phaseTimer=0.0f;
        while (phaseTimer<=duration + 2)
        {
            if(phaseTimer <= duration)
            {
                yield return null;
            }
            else
            {
                phase = 1;
            }
            phaseTimer += Time.deltaTime;
            yield return null;
        }
    }
    public override void Update()
    {
        if (phase == 1)
        {
            Log.log("tag" + gameObject.tag);
            //Log.log("speed " + playerSpeed);
            //target1.transform.RotateAround(transform.position
            //    , new Vector3(0.0f,0.0f,0.5f), 30 * Time.deltaTime);
            TimerChangeColor();

            handsContainer.transform.Rotate(new Vector3(0, 0, 0.5f));
            handsContainer.transform.position = transform.position /*+ new Vector3(-0.7f, -0.1f, 0.0f)*/;
            if (target == null) return;
            charge += Time.deltaTime * weapon.FireRate;
            if (charge >= fullCharge)
            {
                charge = 0;
                FourHandsBossTheMiddleFingerGun();
            }
            charge2 += Time.deltaTime * weapon.FireRate * 10;
            if (charge2 >= fullCharge)
            {
                charge2 = 0;
                FourHandsBossShootTheEraser();
            }
            charge3 += Time.deltaTime * weapon.FireRate * 0.7f;
            if (charge3 >= fullCharge)
            {
                charge3 = 0;
                FourHandsBossShootShotGun();
            }
            charge4 += Time.deltaTime * weapon.FireRate;//laser
            if (charge4 >= fullCharge)
            {
                charge4 = 0;
                FourHandsBossShootLaser();
            }
        }
    }

    void SetRandomBulletColor()
    {
        int rnd = UnityEngine.Random.Range(0, 100);
        int lowLimit = 0;
        for (int i = 0; i < ratios.Length; i++)
        {
            int limit = lowLimit + ratios[i];
            if (rnd >= lowLimit && rnd < limit)
            {
                if (i < 1||i<2)
                {
                    weap3.ChangeColor(colors[i + 1]);
                }
                if (i > 0)
                {
                    weap.ChangeColor(colors[i-1]);
                }
                else if(i>0&&i<2)
                {
                    weap.ChangeColor(colors[i +1]);

                }


                weap2.ChangeColor(colors[i]);
                weap4.ChangeColor(colors[i]);
                return;
            }
            lowLimit = limit;
        }
    }

    void TimerChangeColor()
    {
        timerChangeColor += Time.deltaTime;
        if (timerChangeColor > 5.0f)
        {
            if (colors.Length > 1) SetRandomBulletColor();
            timerChangeColor = 0;
        }
    }


    bool FourHandsBossShootTheEraser()// the eraser
    {
        bool shot = weap3.Shoot(weaponTarget3, aimDir);
        if (shot) shotCount++;
        if (weapon.MagSize > 0 && shotCount >= weapon.MagSize)
        {
            weapon.IsReloading = true;
            shotCount = 0;
            reloadCountdown = weapon.ReloadTime;
            return false;
        }
        return shot;
    }

    bool FourHandsBossTheMiddleFingerGun()
    {
        bool shot = weap4.Shoot(weaponTarget4, aimDir);
        if (shot) shotCount++;
        if (weapon.MagSize > 0 && shotCount >= weapon.MagSize)
        {
            weapon.IsReloading = true;
            shotCount = 0;
            reloadCountdown = weapon.ReloadTime;
            return false;
        }
        
        return shot;
    }

    bool FourHandsBossShootLaser()
    {
        bool shot = weap.Shoot(weaponTarget, aimDir);
        if (shot) shotCount++;
        if (weapon.MagSize > 0 && shotCount >= weapon.MagSize)
        {
            weapon.IsReloading = true;
            shotCount = 0;
            reloadCountdown = weapon.ReloadTime;
            return false;
        }
        return shot;
    }
    bool FourHandsBossShootShotGun()
    {
        bool shot = weap2.Shoot(weaponTarget2, aimDir);
        if (shot) shotCount++;
        if (weapon.MagSize > 0 && shotCount >= weapon.MagSize)
        {
            weapon.IsReloading = true;
            shotCount = 0;
            reloadCountdown = weapon.ReloadTime;
            return false;
        }
        return shot;
    }
}
