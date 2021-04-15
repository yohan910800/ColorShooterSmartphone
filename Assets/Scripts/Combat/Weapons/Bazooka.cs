using UnityEngine;

public class Bazooka : Weapon {
    
    public override void Init(ICharacter character, Transform[] sockets){
        damage = 50;
        fireRate = 0.5f;
        bulletSpeed = 10;
        range = 10f;
        barrelLen = 2f;
        spriteName = "Shotgun";
        bulletName = "BazookaShot";
        base.Init(character, sockets);
    }

}
