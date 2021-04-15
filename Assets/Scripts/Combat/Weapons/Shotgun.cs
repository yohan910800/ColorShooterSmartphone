using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon {

    float separation = 0.3f;
    int a;
    public override void Init(ICharacter character, Transform[] sockets){
        damage = 30;//tmp
        fireRate = 1f;
        bulletSpeed = 40f;
        range = 15f;
        barrelLen = 0.7f;
        spriteName = "Shotgun64x64";
        bulletName = "ShotGunBullet";
        base.Init(character, sockets);
    }

    //yohan added
    //public override bool Shoot(GameObject target, Vector3 aimDir)
    //{
    //    foreach (Transform t in sockets)
    //    {
    //        Vector3 direction = target.transform.position - t.position;
    //        direction = direction.normalized;
    //        if (gm.WorldColor == bulletColor)
    //        {
    //            MonoBehaviour.Instantiate(blankPrefab, t.position + (direction * barrelLen), t.rotation);
    //        }
    //        else
    //        {
    //            for(int i = 0; i < 3; i++)
    //            {
    //                a = i;
    //                if (a == 2)
    //                {
    //                    a = -1;
    //                }
    //                GameObject bulletObj3 = MonoBehaviour.Instantiate(bulletPrefab,
    //                   t.position + (direction * barrelLen) + (Vector3.Cross
    //                       (direction, Vector3.forward) * (a * separation - 0.2f)),
    //                   t.rotation) as GameObject;
    //                Bullet bullet3 = bulletObj3.GetComponent<Bullet>();
    //                if (i == 0)
    //                {
    //                    bullet3.Init(owner, direction, bulletSpeed, damage, range, bulletColor);
    //                }
    //                else
    //                {
    //                    bullet3.Init(owner, direction, 10, damage, range, bulletColor);

    //                }
    //            }
    //        }   
    //    }
    //    return true;
    //}

}
