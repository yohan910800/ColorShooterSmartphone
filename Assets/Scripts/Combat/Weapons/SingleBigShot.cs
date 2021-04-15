using UnityEngine;

public class SingleBigShot : Weapon {
    
    public override void Init(ICharacter character, Transform[] sockets){
        damage = 30;
        fireRate = 1;
        bulletSpeed = 5;
        range = 10f;
        barrelLen = 0.9f;
        spriteName = "SingleShooter";
        bulletName = "StraightShotL";
        base.Init(character, sockets);
    }

}
