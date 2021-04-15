using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class Tentacle : Weapon
{

    Transform[] muzzles;
    BulletTentacle[] tips;
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 1;
        fireRate = 0.3f;
        bulletSpeed = 4;
        range = 10f;
        barrelLen = 0.9f;
        spriteName = "SingleShooter";
        bulletName = "BulletTentacle";
        
        base.Init(character, sockets);
        muzzles = new Transform[sockets.Length];
        tips = new BulletTentacle[sockets.Length];
        int i = 0;
        foreach (Transform t in sockets)
        {
            muzzles[i] = new GameObject("Muzzle").transform;
            muzzles[i].SetParent(t);
            muzzles[i].localPosition = new Vector3(/*barrelLen*/0, 0, 0);
            muzzles[i].localRotation = Quaternion.identity;
            i++;
        }
    }

    public override bool Shoot(GameObject target, Vector3 aimDir)
    {
     
        int i = 0;
        int shotCount = 0;
        foreach (Transform t in muzzles)
        {
            //if (tips[i] != null)
            //{
            //    i++;
            //    continue;
            //}
            Vector3 direction = target.transform.position - t.position;
            direction = direction.normalized;
            if (gm.WorldColor == bulletColor)
            {
                MonoBehaviour.Instantiate(blankPrefab, t.position + (direction * barrelLen), t.rotation);
            }
            else
            {
                GameObject bulletObj = MonoBehaviour.Instantiate(bulletPrefab, t.position, t.rotation) as GameObject;
                BulletTentacle tip = bulletObj.GetComponent<BulletTentacle>();
                tip.Init(owner, direction, bulletSpeed, damage, range, bulletColor, target);
                tip.AttachAnchor(t);
                tips[i] = tip;
            }
            i++;
            shotCount++;
        }
        return shotCount > 0;
    }
}
