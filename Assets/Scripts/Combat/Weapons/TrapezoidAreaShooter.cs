using UnityEngine;

public class TrapezoidAreaShooter : Weapon {
    
    public override void Init(ICharacter character, Transform[] sockets){
        damage = 10;
        fireRate = 1;
        bulletSpeed = 0;
        range = 0;
        barrelLen = 1.5f;
        MagSize = 1;
        ReloadTime = 7f;
        spriteName = "TheEraser";
        bulletName = "TrapezoidZone";
        base.Init(character, sockets);
    }

    public override bool Shoot(GameObject target, Vector3 aimDir){
        foreach (Transform t in sockets){
            Vector3 direction = aimDir.normalized;
            if(gm.WorldColor == bulletColor){
                MonoBehaviour.Instantiate(blankPrefab, t.position + (direction*barrelLen), t.rotation);
            }else{
                GameObject bulletObj = MonoBehaviour.Instantiate(bulletPrefab, t.position + (direction*barrelLen), t.rotation) as GameObject;
                Bullet bullet = bulletObj.GetComponent<Bullet>();
                bullet.Init(owner, direction, bulletSpeed, damage, range, bulletColor);
            }
        }
        return true;
    }

}
