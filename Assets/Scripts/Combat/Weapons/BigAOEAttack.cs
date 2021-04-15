using UnityEngine;

public class BigAOEAttack : Weapon
{
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 1;
        fireRate = 1;
        bulletSpeed = 3;
        range = 10f;
        barrelLen = 0.7f;
        spriteName = "MeleeMode";
        bulletName = "BigAOEBullet";
        base.Init(character, sockets);
    }
}
