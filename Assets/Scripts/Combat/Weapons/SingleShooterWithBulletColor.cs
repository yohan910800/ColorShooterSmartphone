using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShooterWithBulletColor : Weapon
{
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 4;
        fireRate = 0.3f;
        bulletSpeed = 10.0f;
        range = 30f;
        barrelLen = 0.7f;
        spriteName = "SingleShooter32x32";
        bulletName = "StraightShotMWithColor";
        base.Init(character, sockets);
    }
}
