using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEraser : Weapon
{

    float separation = 0.2f;
    int burstCount = 3;

    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 1;
        fireRate = 10f;
        bulletSpeed = 30f;
        range = 30f;
        barrelLen = 0.7f;
        spriteName = "TheEraser64x64";
        bulletName = "EraserBullet";
        base.Init(character, sockets);
    }

    public override bool Shoot(GameObject target, Vector3 aimDir)
    {
        if (owner.GetInputModule().GetDirection() == Vector2.zero)
        {
            foreach (Transform t in sockets)
            {
                Vector3 direction = target.transform.position - t.position;
                direction = direction.normalized;
                if (gm.WorldColor == bulletColor)
                {
                    MonoBehaviour.Instantiate(blankPrefab, t.position + (direction * barrelLen), t.rotation);
                }
                else
                {
                    for (int i = 0; i < burstCount; i++)
                    {
                        GameObject bulletObj = MonoBehaviour.Instantiate(bulletPrefab,
                            t.position + (direction * barrelLen) + (Vector3.Cross
                            (direction, Vector3.forward) * (i * separation - 0.2f)),
                            t.rotation) as GameObject;
                        Bullet bullet = bulletObj.GetComponent<Bullet>();
                        bullet.Init(owner, direction, bulletSpeed, DamageCalculation(damage), range, bulletColor);
                    }
                }
            }
            return true;
        }
        else if (owner.GetGameObject().tag == "FourHandsBoss")
        {
            foreach (Transform t in sockets)
            {
                Vector3 direction = target.transform.position - t.position;
                direction = direction.normalized;
                if (gm.WorldColor == bulletColor)
                {
                    MonoBehaviour.Instantiate(blankPrefab, t.position + (direction * barrelLen), t.rotation);
                }
                else
                {
                    for (int i = 0; i < burstCount; i++)
                    {
                        GameObject bulletObj = MonoBehaviour.Instantiate(bulletPrefab,
                            t.position + (direction * barrelLen) + (Vector3.Cross
                            (direction, Vector3.forward) * (i * separation - 0.2f)),
                            t.rotation) as GameObject;
                        Bullet bullet = bulletObj.GetComponent<Bullet>();
                        bullet.Init(owner, direction, bulletSpeed, DamageCalculation(damage), range, bulletColor);
                    }
                }
            }
            return true;
        }
        return false;
    }
}
