using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRifle : Weapon {

    public override void Init(ICharacter character, Transform[] sockets){
        damage = 1;
        fireRate = 6f;
        bulletSpeed = 10f;
        range = 30f;
        barrelLen = 1.3f;
        MagSize = 15;
        ReloadTime = 2f;
        spriteName = "Rifle";
        bulletName = "StraightShotS";
        base.Init(character, sockets);
    }

}
