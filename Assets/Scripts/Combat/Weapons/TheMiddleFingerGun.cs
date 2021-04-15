using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class TheMiddleFingerGun : Weapon
{

    float rndDirectionX;
    float rndDirectionY;

    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 90;
        fireRate = 2f;
        bulletSpeed = 1f;
        range = 30f;
        barrelLen = 0.7f;
        spriteName = "TheMiddleFingerGun64x64";
        bulletName = "MiddleFingerGunBullet";
        base.Init(character, sockets);
    }

    public override bool Shoot(GameObject target, Vector3 aimDir)
    {

        foreach (Transform t in sockets)
        {

            rndDirectionX = Random.Range(-360.0f, 360.0f);
            rndDirectionY = Random.Range(-360.0f, 360.0f);
            Vector3 Rdirection = new Vector3(rndDirectionX, rndDirectionY, 0);
            Vector3 direction = Rdirection - t.position;
            direction = direction.normalized;
            if (gm.WorldColor == bulletColor)
            {
                MonoBehaviour.Instantiate(blankPrefab, t.position + (direction * barrelLen), t.rotation);
            }
            else
            {
                GameObject bulletObj = MonoBehaviour.Instantiate(bulletPrefab, t.position +
                    (/*aimDir.normalized*/direction * barrelLen), Quaternion.identity) as GameObject;
                Bullet bullet = bulletObj.GetComponent<Bullet>();
                bullet.Init(owner, direction, bulletSpeed, DamageCalculation(damage), range, bulletColor);
            }
        }
        return true;
    }
}
