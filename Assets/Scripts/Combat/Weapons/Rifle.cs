using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon {

    public override void Init(ICharacter character, Transform[] sockets){
        damage = 1;
        fireRate = 8f;
        bulletSpeed = 30f;
        range = 30f;
        barrelLen = 0.7f;
        spriteName = "Rifle32x32";
        bulletName = "RifleShot";

        base.Init(character, sockets);
    }

}
