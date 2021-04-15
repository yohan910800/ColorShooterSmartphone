using UnityEngine;

public class FishHeadGun : Weapon
{
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 10;
        fireRate = 1;
        bulletSpeed = 8;
        range = 10f;
        barrelLen = 0.7f;
        spriteName = "SingleShooter";
        bulletName = "FishHeadBullet";
        base.Init(character, sockets);
    }
}
