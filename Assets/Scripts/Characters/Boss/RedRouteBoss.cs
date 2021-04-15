using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System;
public class RedRouteBoss: Character {
public int socketNo;
    public BossWeapon weapon;
    public enum BossWeapon {
        RedRouteBossPattern1Weapon1,
        RedRouteBossPattern1Weapon2,
        RedRouteBossPattern2Weapon1
    }
     Transform[][] sockeColl;
  public override void Init(){
         
        base.Init();
        var weaponSocket1 = transform.Find("Hand1");
        var weaponSocket2 = transform.Find("Hand2");
        sockeColl = new Transform[][]{
            new Transform[]{weaponSocket2},
            new Transform[]{weaponSocket1,weaponSocket2}
        };
        Weapon weap = Activator.CreateInstance(Type.GetType(weapon.ToString())) as Weapon;
        weap.Init(this, sockeColl[socketNo-1]);
        AddWeapon(weap);        
        ActivateWeapon(inventory.Weapons[0]);
    }
    /*
    public override void Init() {
        base.Init();
        var weaponSocket1 = transform.Find("Hand1");
        var weaponSocket2 = transform.Find("Hand2");
        dualWieldSockets = new Transform[]{weaponSocket1, weaponSocket2};
        singleSocket = new Transform[]{weaponSocket2};

    Weapon weap1 = new RedRouteBossPattern1Weapon1();
    weap1.Init(this, singleSocket);
    AddWeapon(weap1);

    Weapon weap2 = new RedRouteBossPattern1Weapon2();
    weap2.Init(this, singleSocket);
    AddWeapon(weap2);

    Weapon weap3 = new RedRouteBossPattern2Weapon1();
    weap3.Init(this, singleSocket);
    AddWeapon(weap3);

    ActivateWeapon(inventory.Weapons[0]);
}*/

public void ActivateWeaponP1W1() {
    this.ActivateWeapon(inventory.Weapons[0]);

}
public void ActivateWeaponP1W2() {
    this.ActivateWeapon(inventory.Weapons[1]);

}
public void ActivateWeaponP2W1() {
    this.ActivateWeapon(inventory.Weapons[2]);

}
protected override void InitInput() {
    base.InitInput();
}

 void GetHit(int dmg){
        stats.LowerHP(dmg);
        stateUI.Refresh();
        if(stats.HP == 0 && IsAlive){
            OnDeath();
        }
        if(gm.showDamageText){
            var gObj = Instantiate(damageTextPrefab,transform.position,Quaternion.identity) as GameObject;
            DamageText dt = gObj.GetComponent<DamageText>();
            dt.Init(dmg, Color.black);
        }
    }

    public override bool HitCheck(Bullet bullet){
        ICharacter character = bullet.Owner;
        if(character.GetGameObject().tag == "Player"){
            int damage  = color == bullet.BulletColor ? bullet.Damage : bullet.Damage * Static.colorDmgMultiplier;
            GetHit(damage);
            return true;
        }
        return false;
    }

    protected override void OnDeath(){
        IsAlive = false;
        stateUI.OnDeath();
        base.OnDeath();
        Destroy(gameObject);
    }

}
