using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
/*
Simple auto aim at player combat module.
*/
public class BossCombat : MonoBehaviour, ICombat
{

    bool fixedAim;
    Vector2 fixedAimDir;

    protected float fullCharge = 1.0f;
    protected float charge;
    protected GameObject target;
    protected Character character;
    protected Weapon weapon;
    protected Vector3 aimDir;
    protected int shotCount;
    protected float reloadCountdown;

protected state CurrentState;
 public enum state
    {
        Idle,
        Phase1Enter,
        Phase1Pattern1,
        Phase1Pattern2,
        Phase2Enter,
        Phase2Pattern1,
        Phase2Pattern2,
        Phase3Enter,
        Phase3Pattern1,
        Phase3Pattern2,
        Phase4Enter,
        Phase4Pattern1,
        Phase4Pattern2,
        EndPattern,
        Dead
    }

    public virtual void Init(ICharacter character){
        target = GameObject.Find("Player");
        weapon = character.GetActiveWeapon();
        character.OnWeaponChange += OnWeaponChange;
        this.character = character as Character;
        aimDir = Vector3.down;
        CurrentState= state.Phase1Enter;
    }

    public virtual void Update(){
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
        charge += Time.deltaTime * weapon.FireRate;
        if(charge >= fullCharge){
            charge=0;
            if(Shoot()) charge = 0;
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
    protected void ChangeState(state s)
    {
        Log.log("State change: " + s.ToString());
        CurrentState = s;
    }

    void UpdatePattern()
    {
        switch (CurrentState)
        {
            case state.Phase1Enter:
                Phase1Enter();
                return;
            case state.Phase1Pattern1:
                Phase1Pattern1();
                return;
            case state.Phase1Pattern2:
                Phase1Pattern2();
                return;
            case state.Phase2Enter:
                Phase2Enter();
                return;
            case state.Phase2Pattern1:
                Phase2Pattern1();
                return;
            case state.Phase2Pattern2:
                Phase2Pattern2();
                return;
            case state.EndPattern:
                EndPattern();
                return;
        }
    }
    protected virtual void Phase1Enter()
    {
        Log.log(name + " Phase 1 enter not implemented");
    }
    protected virtual void Phase1Pattern1()
    {
        Log.log(name + " Phase 1 pattern 1 not implemented");
    }
    protected virtual void Phase1Pattern2()
    {
        Log.log(name + " Phase 1 pattern 2 not implemented");
    }
    protected virtual void Phase2Enter()
    {
        Log.log(name + " Phase 2 enter not implemented");
    }
    protected virtual void Phase2Pattern1()
    {
        Log.log(name + " Phase 2 pattern 1 not implemented");
    }
    protected virtual void Phase2Pattern2()
    {
        Log.log(name + " Phase 2 pattern 2 not implemented");
    }
    protected virtual void EndPattern()
    {
        Log.log(name + " End pattern not implemented");
    }
}