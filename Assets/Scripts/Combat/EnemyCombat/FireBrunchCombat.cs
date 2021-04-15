using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class FireBrunchCombat : EnemyCombatV1
{
    public Colors[] colors;
    public int[] ratios;

    public float phaseTimer;
    float shootFireRate;
    float meleeFireRate;

    Transform[][] sockeColl;// take the weapon socket

    public override void Init(ICharacter character)
    {
        base.Init(character);
        foreach (Colors c in colors)
        {
            this.character.AddColor(c);
        }
        character.SetBulletColor(colors[0]);
        CreateNewWeapon();

        character.phase = 0;
        phaseTimer = 0.0f;
        
    }

   
    void CreateNewWeapon()
    {
        weapon = new FireBulletShooter();

        var weaponSocket2 = transform.Find("Hand2");
        sockeColl = new Transform[][]{
            new Transform[]{weaponSocket2},
        };
        Weapon weap = new FireBulletShooter() as Weapon;
        weap.Init(character, sockeColl[0]);
        character.AddWeapon(weap);
    }
    void OnEquipeShootFire()// change weapon 
    {
        OnWeaponChange(character.GetInventory().Weapons[0]);
        character.ActivateWeapon(character.GetInventory().Weapons[0]);
    }

    void OnEquipeMeleeMode()
    {
        OnWeaponChange(character.GetInventory().Weapons[1]);
        character.ActivateWeapon(character.GetInventory().Weapons[1]);
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
   
    public  override void Update()
    {
        ChoosePhase();
        ChooseBehavior();
    }

    void ChoosePhase()
    {
        phaseTimer += Time.deltaTime;
        if (phaseTimer >= 5)
        {
            if (character.phase == 0)
            {
                OnEquipeMeleeMode();
                character.phase = 1;
            }
            else if (character.phase == 1)
            {
                OnEquipeShootFire();
                character.phase = 2;
            }

            else if (character.phase == 3)
            {
                OnEquipeMeleeMode();
                character.phase = 1;
            }
            phaseTimer = 0;
        }
    }

    void ChooseBehavior()
    {
        switch (character.phase)
        {
            case 0:
                OnEquipeMeleeMode();
                //play intro animation with music
                break;
            case 1:
                Aim();
                meleeFireRate += Time.deltaTime;
                if (meleeFireRate >= 0.1f)
                {
                    Shoot();
                    meleeFireRate = 0.0f;
                }
                break;
            case 2:
                
                break;
            case 3:
                Aim();
                shootFireRate += Time.deltaTime;
                if (shootFireRate >= 1.0f)
                {
                    ShootFire();
                    shootFireRate = 0.0f;
                }
                break;
        }
    }
    

    protected override bool Shoot()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        //Log.log("dist " + dist);
        //Log.log("phase " + GetComponent<EnemyChargeInput>().phase);
        if (dist > 3 && GetComponent<FireBrunchInput>().isAttacking == false
            || GetComponent<FireBrunchInput>().stopBurst == true)
        {
            return false;
        }

        else
        {
            if (colors.Length > 1) SetRandomBulletColor();
            return base.Shoot();
        }
    }
    bool ShootFire()
    {
        bool shot = weapon.Shoot(target, aimDir);
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