using UnityEngine;

public class QuickBazooka : Weapon
{
    bool isUper;
    public override void Init(ICharacter character, Transform[] sockets)
    {
        damage = 1;
        fireRate = 2f;
        bulletSpeed = 10;
        range = 30f;
        barrelLen = 2f;
        spriteName = "SingleShooter";
        bulletName = "BazookaShot";

        base.Init(character, sockets);
        
    }
    public override bool Shoot(GameObject target, Vector3 aimDir)
    {
        isUper = owner.GetGameObject().GetComponent<GRP3BossInput>().CheckIFUper();

        foreach (Transform t in sockets)
        {
            Vector3 direction = target.transform.position - t.position;
            direction = direction.normalized;
            if (gm.WorldColor == bulletColor)
            {
                MonoBehaviour.Instantiate(blankPrefab, t.position /*+ (direction * barrelLen)*/, t.rotation);
            }
            else
            {
                GameObject bulletObj = MonoBehaviour.Instantiate(bulletPrefab, t.position + (direction * barrelLen), t.rotation) as GameObject;
                Bullet bullet = bulletObj.GetComponent<Bullet>();
                if (isUper==false)
                {
                    bullet.Init(owner, new Vector3(0.0f, -5.0f, 0.0f), bulletSpeed, damage, range, bulletColor);
                }
                else
                {
                    bullet.Init(owner, new Vector3(0.0f, 5.0f, 0.0f), bulletSpeed, damage, range, bulletColor);

                }
            }
        }
        return true;
    }
}
