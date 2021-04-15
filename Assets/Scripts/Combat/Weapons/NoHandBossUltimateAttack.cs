using UnityEngine;

public class NoHandBossUltimateAttack : Weapon
{
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 100;
        fireRate = 1;
        bulletSpeed = 4;
        range = 100f;
        barrelLen = 0.7f;
        spriteName = "MeleeMode";
        bulletName = "NoHandBossUltimateAttackBullet";
        base.Init(character, sockets);
    }
}
