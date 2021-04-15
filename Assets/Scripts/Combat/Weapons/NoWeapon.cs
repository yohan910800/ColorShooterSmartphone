using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoWeapon : Weapon
{
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 00;
        fireRate = 0;
        bulletSpeed = 0;
        range = 0f;
        barrelLen = 0f;
        spriteName = "MeleeMode";
        bulletName = null;
        base.Init(character, sockets);
    }
}
