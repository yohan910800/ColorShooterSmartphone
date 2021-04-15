using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
/*
Auto aim at player and shoot in sync with a squad.
Only works for Characters that use the base class Character.
TODO:
- FOV checks so they don't shoot at walls
*/
public class SquadMemberCombat : MonoBehaviour, ICombat {
    
    
    GameObject target;
    Character character;
    Weapon weapon;
    Vector3 aimDir;
    int shotCount;
    float reloadCountdown;

    public void Init(ICharacter character){
        target = GameObject.FindWithTag("Player");
        weapon = character.GetActiveWeapon();
        character.OnWeaponChange += OnWeaponChange;
        this.character = character as Character;
        aimDir = Vector3.down;
        foreach (Colors c in Static.bulletColors.Keys){
            this.character.AddColor(c);
        }
    }

    public void Update(){
        if(target==null) return;
        Aim();
        if(weapon.IsReloading){
            reloadCountdown -= Time.deltaTime;
            if(reloadCountdown<=0){
                weapon.IsReloading=false;
            }else{
                return;
            }
        }
    }

    public void SetColor(Colors color){
        character.SetBulletColor(color);
    }

    public bool Shoot(){
        if(target==null) return false;
        bool shot = weapon.Shoot(target,aimDir);
        if(shot) shotCount++;
        if( weapon.MagSize > 0 && shotCount >= weapon.MagSize){
            weapon.IsReloading = true;
            shotCount = 0;
            reloadCountdown = weapon.ReloadTime;
            return false;
        }
        return shot;
    }

    void Aim(){
        Vector3 direction;
        if(target != null){
            direction = target.transform.position - gameObject.transform.position;
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

    void OnWeaponChange(Weapon weapon){
        this.weapon = weapon;
    }
    
    public void Terminate(){

    }

    public Vector3 GetAimDirection(){
        return aimDir;
    }
}