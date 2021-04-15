using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstShooter : Weapon {
    
    float separation = 0.3f;
    int burstCount = 3;

    public override void Init(ICharacter character, Transform[] sockets){
        damage = 5;
        fireRate = 1.5f;
        bulletSpeed = 3f;
        range = 10f;
        barrelLen = 1.3f;
        spriteName = "BurstShooter";
        bulletName = "StraightShotM";
        base.Init(character, sockets);
    }

    public override bool Shoot(GameObject target, Vector3 aimDir){
        foreach (Transform t in sockets){
            Vector3 direction = target.transform.position - t.position;
            direction = direction.normalized;
            if(gm.WorldColor == bulletColor){
                MonoBehaviour.Instantiate(blankPrefab, t.position + (direction*barrelLen), t.rotation);
            }else{
                for(int i=0; i<burstCount; i++){
                    GameObject bulletObj = MonoBehaviour.Instantiate(bulletPrefab,
                        t.position + direction*(barrelLen+i*separation), Quaternion.identity) as GameObject;
                    Bullet bullet = bulletObj.GetComponent<Bullet>();
                    bullet.Init(owner, direction, bulletSpeed, damage, range, bulletColor);
                }
            }
        }
        return true;
    }
}
