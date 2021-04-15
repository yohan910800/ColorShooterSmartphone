using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System;
/*
Simple auto aim at player combat module.
Only works for Characters that use the base class Character.
TODO:
- FOV checks so they don't shoot at walls
*/
public class EnemyCombatV1 : MonoBehaviour, ICombat {

    protected Vector2 fixedAimDir;// protected yohan added

    protected bool fixedAim;// protected yohan added 
    protected float fullCharge = 1.0f;
    protected float charge;
    protected GameObject target;
    protected Character character;
    protected Weapon weapon;
    protected Vector3 aimDir;
    protected int shotCount;
    protected float reloadCountdown;
    MeleeMode meleeMode = new MeleeMode();//tmp

    public virtual void Init(ICharacter character){
        target = GameObject.Find("Player");
        weapon = character.GetActiveWeapon();
        character.OnWeaponChange += OnWeaponChange;
        this.character = character as Character;
        aimDir = /*new Vector3(0.0f,0.0f,0.0f)*/Vector3.down;//tmp
    }
    public virtual void Update(){

        if (target==null) return;
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
                if (Shoot()) charge = 0;
            }
        }
    }

    protected virtual bool Shoot(){
        bool shot = weapon.Shoot(target, aimDir);
        if(shot) shotCount++;
        if( weapon.MagSize > 0 && shotCount >= weapon.MagSize){
            weapon.IsReloading = true;
            shotCount = 0;
            reloadCountdown = weapon.ReloadTime;
            return false;
        }
        return shot;
    }

    protected virtual void Aim(){
        Vector3 direction;
        if(target != null && !fixedAim){
            direction = target.transform.position - gameObject.transform.position;
        }else if(fixedAim){
            direction = fixedAimDir;
        }else{
            direction = character.GetInputModule().GetDirection();
        }
        if(direction.magnitude > 0){
            foreach (Transform t in weapon.sockets){
                t.right = direction;
                if(t.localEulerAngles.y != 0){
                    t.localEulerAngles = new Vector3(0,0,180.0f);
                }
            }
            aimDir = direction;
        }
    }

    protected virtual void OnWeaponChange(Weapon weapon){
        this.weapon = weapon;
    }
    
    public virtual void Terminate(){

    }

    public virtual Vector3 GetAimDirection(){
        return aimDir;
    }

    public void SetFixedAim(bool val, Vector2 dir){
        fixedAim = val;
        fixedAimDir = dir;
    }
}