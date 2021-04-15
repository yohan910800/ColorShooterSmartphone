using UnityEngine;

public class SingleShooter : Weapon {
    
    public override void Init(ICharacter character, Transform[] sockets){
        damage = 3;
        fireRate = 1.5f;
        bulletSpeed = 30.0f;
        range = 30f;
        barrelLen = 0.7f;
        spriteName = "SingleShooter32x32";
        bulletName = "StraightShotM";
        base.Init(character, sockets);
    }
}
