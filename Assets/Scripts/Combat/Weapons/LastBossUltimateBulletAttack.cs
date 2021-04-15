using UnityEngine;

public class LastBossUltimateBulletAttack : Weapon
{
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 10;
        fireRate = 1;
        bulletSpeed = 0.001f;
        range = 10f;
        barrelLen = 0.7f;
        spriteName = "MeleeMode";
        bulletName = "LastBossUltimateBullet";
        base.Init(character, sockets);
    }
}
