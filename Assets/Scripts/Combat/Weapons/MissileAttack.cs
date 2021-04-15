using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttack : Weapon
{
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 0;
        fireRate = 0.7f;
        range = 10f;
        bulletSpeed = 0f;
        barrelLen = 0.9f;
        spriteName = "SingleShooter";
        bulletName = "GRP1Zone";
        //zoneName = "GRP1Zone";
        base.Init(character, sockets);
        //owner.SetBulletColor(Colors.Red);
        //zone = Resources.Load<GameObject>("Prefabs/Bullets/" + zoneName);
        //i++;
    }
}
