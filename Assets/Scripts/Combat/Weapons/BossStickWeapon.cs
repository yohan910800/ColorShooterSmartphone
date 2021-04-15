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

        //Vector3 target1 = owner.GetGameObject().GetComponent<BRRoom1BossCombat>().
        //    weaponTarget.transform.position;



        Vector3 direction = sockets[0].transform.position;
        direction = direction.normalized;

        GameObject bulletObj = MonoBehaviour.Instantiate(bulletPrefab, sockets[0].position
        , /*Quaternion.identity*/sockets[0].rotation) as GameObject;
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        StraightShotBullet speedBullet = bulletObj.GetComponent<StraightShotBullet>();
        //speedBullet.speed = 0.0f;
        //speedBullet.lifeTime = 800f;

        float rndScaleX = Random.Range(3, 8);
        //float rndScaleY = Random.Range(1, 3);
        //bulletObj.GetComponent<CircleCollider2D>().radius = 0.4f * rndScaleX;//to change


        bulletObj.transform./*GetChild(0).gameObject.transform.*/localScale =
        new Vector3(rndScaleX, rndScaleX, 1f);
        bullet.Init(owner, direction,bulletSpeed, damage,range, bulletColor);

        return true;
    }

}
