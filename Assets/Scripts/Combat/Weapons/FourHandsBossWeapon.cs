using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class FourHandsBossWeapon : Weapon
{
    float separation = 0.2f;
    int burstCount = 3;
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 3;
        fireRate = 10f;
        bulletSpeed = 30f;
        range = 30f;
        barrelLen = 1.7f;
        spriteName = "TheEraser";
        bulletName = "EraserBullet";
        base.Init(character, sockets);
    }
}

