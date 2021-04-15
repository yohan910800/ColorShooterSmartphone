using UnityEngine;

public class LaserSurgeryGun : Weapon
{
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 10;
        fireRate = 1;
        bulletSpeed = 10;
        range = 20f;
        barrelLen = 0.7f;
        spriteName = "Laser64x64";
        bulletName = "Laser";

        base.Init(character, sockets);
    }
}
