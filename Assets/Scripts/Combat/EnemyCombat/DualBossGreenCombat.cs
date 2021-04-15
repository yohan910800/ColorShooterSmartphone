using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class DualBossGreenCombat : EnemyCombatV1
{
    public Colors[] colors;
    public int[] ratios;

    float charge2;

    Transform[][] socketColl;
    Weapon weap1;
    Weapon weap2;

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
    }

    void CreateNewWeapon()
    {
        var weaponSocket1 = transform.Find("Hand1");
        var weaponSocket2 = transform.Find("Hand2");
        socketColl = new Transform[][]{

            new Transform[]{weaponSocket1},
            new Transform[]{weaponSocket2},
            new Transform[]{weaponSocket1,weaponSocket2},
        };

        if (gameObject.tag == "DualYellowBoss")
        {
            weap1 = new TheEraser() as Weapon;
            weap1.Init(character, socketColl[0]);
            character.AddWeapon(weap1);

            weap2 = new LaserSurgeryGun() as Weapon;
            weap2.Init(character, socketColl[1]);
            character.AddWeapon(weap2);
        }
        else if(gameObject.tag == "DualGreenBoss")
        {
            weap1 = new FishHeadGun() as Weapon;
            weap1.Init(character, socketColl[0]);
            character.AddWeapon(weap1);

            weap2 = new Shotgun() as Weapon;
            weap2.Init(character, socketColl[1]);
            character.AddWeapon(weap2);
        }
    }

    void EquipeAllWeapons()
    {
        OnWeaponChange(character.GetInventory().Weapons[0]);
        character.ActivateAllWeapons(character.GetInventory().Weapons[0]);

        OnWeaponChange(character.GetInventory().Weapons[1]);
        character.ActivateAllWeapons(character.GetInventory().Weapons[1]);
    }

    public override void Update()
    {
        if (target == null) return;
        if (gameObject.tag == "DualYellowBoss")
        {
            charge += Time.deltaTime;
            if (charge >= fullCharge)
            {
                charge = 0;
                LaserSurgeryGunShoot();
            }
            charge2 += Time.deltaTime * 10;
            if (charge2 >= fullCharge)
            {
                charge2 = 0;
                TheEraserShoot();
            }
        }
        else if(gameObject.tag == "DualGreenBoss")
        {
            charge += Time.deltaTime;
            if (charge >= fullCharge)
            {
                charge = 0;
                FishHeadGunShoot();
            }
            charge2 += Time.deltaTime * 2;
            if (charge2 >= fullCharge)
            {
                charge2 = 0;
                ShotgunShoot();
            }
        }
        
    }
    bool FishHeadGunShoot()
    {
        bool shot = weap1.Shoot(target, aimDir);
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
    bool ShotgunShoot()
    {
        bool shot = weap2.Shoot(target, aimDir);
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

    bool LaserSurgeryGunShoot()
    {
        bool shot = weap2.Shoot(target, aimDir);
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

    bool TheEraserShoot()
    {
        bool shot = weap1.Shoot(target, aimDir);
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
