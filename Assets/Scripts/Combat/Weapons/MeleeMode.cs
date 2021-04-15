using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class MeleeMode : Weapon
{
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 1;
        fireRate = 1/*0.3f*/;
        bulletSpeed = 0.1f;
        range = 0.01f;
        barrelLen = 0.1f;
        spriteName = "MeleeMode32x32";
        bulletName = "BareHandsBullet";
        base.Init(character, sockets);
    }
}
