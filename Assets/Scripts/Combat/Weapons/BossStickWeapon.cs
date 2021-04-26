using UnityEngine;

public class BossStickWeapon : Weapon
{
    float separation = 0.3f;
    int burstCount = 3;

    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 3;
        fireRate = 1f;
        bulletSpeed = 0.001f;
        range = 1;
        spriteName = "SingleShooter";
        bulletName = "StraightShotM";
        base.Init(character, sockets);
    }

    public override bool Shoot(GameObject target,Vector3 aimDir)
    {
        Vector3 direction = sockets[0].transform.position;
        direction = direction.normalized;

        GameObject bulletObj = MonoBehaviour.Instantiate(bulletPrefab, sockets[0].position
        , sockets[0].rotation) as GameObject;
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        StraightShotBullet speedBullet = bulletObj.GetComponent<StraightShotBullet>();

        float rndScaleX = Random.Range(3, 8);

        bulletObj.transform./*GetChild(0).gameObject.transform.*/localScale =
        new Vector3(rndScaleX, rndScaleX, 1f);
        bullet.Init(owner, direction,bulletSpeed, damage,range, bulletColor);

        return true;
    }

}
