using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class TestEnemy : Character {

    Transform[] singleSocket;
    Transform[] dualWieldSockets;

    public override void Init(){
        base.Init();
        var weaponSocket1 = transform.Find("Hand1");
        var weaponSocket2 = transform.Find("Hand2");
        dualWieldSockets = new Transform[]{weaponSocket1,weaponSocket2};
        singleSocket = new Transform[]{weaponSocket2};

        // TEMP // Hard coded weapons atm 
        // this changes when we get a save/load system
        AddColor(Colors.Red);
        SetBulletColor(Colors.Red);
        Weapon weap = new SingleShooter();
        weap.Init(this, singleSocket);
        AddWeapon(weap);
        weap = new BurstShooter();
        weap.Init(this, dualWieldSockets);
        AddWeapon(weap);
        
        ActivateWeapon(inventory.Weapons[0]);
    }

    protected override void InitInput(){
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
