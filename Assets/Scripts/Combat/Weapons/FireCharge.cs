using UnityEngine;

public class FireCharge : Weapon
{
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 1;
        fireRate = 3f;
        range = 0.5f;
        bulletSpeed = 0.03f;
        barrelLen = 0.9f;
        spriteName = "Spatula64x64";
        bulletName = "FireBulletCharge";

        base.Init(character, sockets);

    }
}
