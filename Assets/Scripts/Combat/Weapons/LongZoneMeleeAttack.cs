using UnityEngine;

public class LongZoneMeleeAttack : Weapon
{
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 1;
        fireRate = 10;
        bulletSpeed = 0.01f;
        range = 0.2f;
        barrelLen = 2f;
        spriteName = "MeleeMode";
        bulletName = "BazookaShot";
        base.Init(character, sockets);
    }
}
