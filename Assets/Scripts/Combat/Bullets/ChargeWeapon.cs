using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class ChargeWeapon : Weapon
{
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 1;
        fireRate = 3f;
        
        
        range =0.5f;
        bulletSpeed = 0.3f;
        barrelLen = 0.9f;
        spriteName = "MeleeMode32x32";
        bulletName = "ChargeAttackBullet";

        base.Init(character, sockets);
        
    }
}
