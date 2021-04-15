using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class TripleSingleShooter : Weapon
{
    int shotCount=0;
    int rechargeCount=0;
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 1;
        fireRate = 2f;
        bulletSpeed = 10f;
        range = 30f;
        barrelLen = 0.7f;
        spriteName = "TripleSingleShooter64x64";
        bulletName = "TripleStraightShotM";

        base.Init(character, sockets);
    }



    public override bool Shoot(GameObject target, Vector3 aimDir)
    {
        //if (shotCount < 3)
        //{
        foreach (Transform t in sockets)
        {
            if (target != null)//yohan added
            {
                Vector3 direction = target.transform.position - t.position;
                direction = direction.normalized;
                if (gm.WorldColor == bulletColor)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        MonoBehaviour.Instantiate(particlePrefab, t.position +
                            (direction * barrelLen), t.rotation);

                        MonoBehaviour.Instantiate(blankPrefab, t.position + (direction * barrelLen),
                            t.rotation);
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        MonoBehaviour.Instantiate(particlePrefab, t.position + (direction * barrelLen), t.rotation);

                        GameObject bulletObj = MonoBehaviour.Instantiate(bulletPrefab, t.position + direction
                            * (barrelLen + i * 1.3f), t.rotation) as GameObject;
                        Bullet bullet = bulletObj.GetComponent<Bullet>();
                        bullet.Init(owner, direction, bulletSpeed, DamageCalculation(damage), range, bulletColor);
                        if (doesItHaveDoubleShot == true)
                        {
                            MonoBehaviour.Instantiate(particlePrefab, t.position + (direction * barrelLen), t.rotation);

                            GameObject bulletObj2 = MonoBehaviour.Instantiate
                                (bulletPrefab, t.position + direction
                            * (barrelLen + i * 1.3f) + new Vector3(0.5f, 0.0f, 0.0f),
                            t.rotation) as GameObject;

                            Bullet bullet2 = bulletObj2.GetComponent<Bullet>();
                            bullet2.Init(owner, direction, bulletSpeed, DamageCalculation(damage), range, bulletColor);

                        }
                        if (doesItHavePoisonBullet == true)
                        {
                            bullet.doesPoisonedBulletIsActivate = true;
                        }
                    }

                }
            }
        }
        return true;
        //    if (owner.GetInputModule().GetDirection() != Vector2.zero)
        //    {
        //        ResetShot();
        //    }
        //    shotCount++;
        //    return true;
        //}
        //else
        //{
        //    RefreshOnThreeShot();
        //    Log.log("recharge count " +rechargeCount);
        //    return false;
        //}
    }

    //void RefreshOnThreeShot()
    //{
    //    rechargeCount++;
    //    if (rechargeCount == 15)
    //    {
    //        shotCount = 0;
    //        rechargeCount = 0;
    //    }
    //}
    //void ResetShot()
    //{
    //    shotCount = 0;
    //    rechargeCount = 0;
    //}
}
