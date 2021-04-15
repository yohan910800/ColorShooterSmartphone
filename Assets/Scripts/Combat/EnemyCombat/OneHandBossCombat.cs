using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class OneHandBossCombat : EnemyCombatV1
{
    public Colors[] colors;
    public int[] ratios;

    public bool isHiting;
    float charge2;
    float charge3;
    float charge4;
    float timerChangeColor;

    Transform[][] sockeColl;
    Transform[] Hand1;
    Transform[] Hand2;

    GameObject stick;
    GameObject stickBullet;
    Vector3 curentPos;

    public GameObject handsContainer;
    Weapon weap;


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

        weapon = character.GetActiveWeapon();
        character.OnWeaponChange += OnWeaponChange;
        aimDir = Vector3.down;
        CreateNewWeapon();
        OnWeaponChange(character.GetInventory().Weapons[0]);
        character.ActivateWeapon(character.GetInventory().Weapons[0]);


    }
    void CreateNewWeapon()
    {
        var weaponSocket1 = transform.Find("TurnObject1").Find("Hand1");
        var weaponSocket2 = transform.Find("Hand2");
        
        sockeColl = new Transform[][]{

            Hand2=new Transform[]{weaponSocket2},
            Hand1=new Transform[]{weaponSocket1},
        };

        weap = new BossStickWeapon() as Weapon;
        weap.Init(character, Hand1);
        character.AddWeapon(weap);

    }

    public override void Update()
    {
        TimerChangeColor();
        if (isHiting == false)
        {
            handsContainer.transform.Rotate(new Vector3(0, 0, 2f));
            handsContainer.transform.position = transform.position /*+ new Vector3(-0.7f, -0.1f, 0.0f)*/;

            if (target == null) return;
            //Aim();
            charge += Time.deltaTime * weapon.FireRate;
            if (charge >= fullCharge)
            {
                charge = 0;
                weap.Shoot(gameObject, aimDir);
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
                weap.ChangeColor(colors[i]);
                return;
            }
            lowLimit = limit;
        }
    }

    void TimerChangeColor()
    {
        timerChangeColor += Time.deltaTime;
        if (timerChangeColor > 1.0f)
        {
            if (colors.Length > 1) SetRandomBulletColor();
            timerChangeColor = 0;
        }
    }
    bool OneHandShoot()
    {
        bool shot = weap.Shoot(gameObject, aimDir);
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
