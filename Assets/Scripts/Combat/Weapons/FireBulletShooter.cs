
using UnityEngine;

public class FireBulletShooter :Weapon
{
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 3;
        fireRate = 1.5f;
        bulletSpeed = 5.0f;
        range = 30f;
        barrelLen = 0.7f;
        spriteName = "Spatula64x64";
        bulletName = "FireBullet";
        base.Init(character, sockets);
    }
}
