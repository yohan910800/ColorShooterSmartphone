using UnityEngine;

public class LastBossMeleeMode : Weapon
{
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 10;
        fireRate = 1;
        bulletSpeed = 0.1f;
        range = 0.2f;
        barrelLen = 0.7f;
        spriteName = "MeleeMode";
        bulletName = "LastBossMeleeBullet";
        base.Init(character, sockets);
    }
}
