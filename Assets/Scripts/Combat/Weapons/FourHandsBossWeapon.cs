using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class FourHandsBossWeapon : Weapon
{
    float separation = 0.2f;
    int burstCount = 3;

    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 3;
        fireRate = 10f;
        bulletSpeed = 30f;
        range = 30f;
        barrelLen = 1.7f;
        spriteName = "TheEraser";
        bulletName = "EraserBullet";
        base.Init(character, sockets);
    }
    //public override bool FourHandsBossTheEraserShoot(GameObject target,
    //    Vector3 aimDir, Transform socket)
    //{
    //    //bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullets/EraserBullet");
    //    barrelLen = 1.7f;
    //    bulletSpeed = 30f;
    //    range = 30f;

    //    Vector3 direction = target.transform.position - socket.position;
    //    direction = direction.normalized;
    //    if (gm.WorldColor == bulletColor)
    //    {
    //        MonoBehaviour.Instantiate(blankPrefab, socket.position +
    //            (direction * barrelLen), socket.rotation);
    //    }
    //    else
    //    {
    //        for (int i = 0; i < 3; i++)
    //        {
    //            GameObject bulletObj = MonoBehaviour.Instantiate(bulletPrefab,
    //                socket.position + (direction * barrelLen) +
    //                (Vector3.Cross(direction, Vector3.forward) * (i * 0.2f - 0.2f)),
    //                 socket.rotation) as GameObject;
    //            Bullet bullet = bulletObj.GetComponent<Bullet>();
    //            bullet.Init(owner, direction, bulletSpeed, damage, range, bulletColor);
    //        }
    //    }
    //    return true;
    //}
}

