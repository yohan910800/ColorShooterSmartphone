using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;



public class BlackBossAttack : Weapon
{
    Transform[] muzzles;
    BossTentacle[] tips;
    int phase;
   

    public override void Init(ICharacter character, Transform[] sockets)
    {
        
        damage = 10;
        fireRate = 1f;
        spriteName = "MeleeMode"/*"SingleShooter"*/;
        bulletName = "BossTentacle"; /*BareHandsBullet*/
         //bulletName = "ORBossLongBullet";
         barrelLen = 0.1f;
        bulletSpeed =5f;
        range = 10f;
        base.Init(character, sockets);

        muzzles = new Transform[sockets.Length];
        tips = new BossTentacle[sockets.Length];
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
            //Vector3 direction = target.transform.position - t.position;
            //direction = direction.normalized;
            if (gm.WorldColor == bulletColor)
            {
                MonoBehaviour.Instantiate(blankPrefab, t.position , t.rotation);
            }
            else
            {
                GameObject bulletObj = MonoBehaviour.Instantiate(bulletPrefab, t.position, t.rotation) as GameObject;
                BossTentacle tip = bulletObj.GetComponent<BossTentacle>();
                tip.Init(owner, new Vector3(0.0f,0.0f,0.0f), bulletSpeed, damage, range, bulletColor, target);
                tip.AttachAnchor(t);
                tips[i] = tip;
            }
            i++;
            shotCount++;
        }
        return shotCount > 0;
    }

    //public /*override */void Shoot(GameObject target)
    //{
    //    Log.log("HERE");
    //    ChoosePhase(target);
    //}

    //void ChoosePhase(GameObject target)
    //{
    //    phase = owner.GetGameObject().GetComponent<BlackBossCombat>().phase;
    //    if (phase == 1)
    //    {
    //        MeleeAttack(target);
    //    }
    //    else if (phase == 2)
    //    {
    //        TentacleAttack(target);
    //    }
    //    else if (phase == 3)
    //    {
    //        SpawnEnemyAttack();
    //    }
    //}

    // void MeleeAttack(GameObject target)
    //{
    //    bulletName = "StraightShotM";
    //    bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullets/" + bulletName);
    //    fireRate = 1;
    //    foreach (Transform t in sockets)
    //    {
    //        Vector3 direction = target.transform.position - t.transform.position;
    //        direction = direction.normalized;
    //        GameObject bulletObj = MonoBehaviour.Instantiate(bulletPrefab, t.position +
    //            (direction * barrelLen), Quaternion.identity) as GameObject;
    //        Bullet bullet = bulletObj.GetComponent<Bullet>();
    //        bulletObj.transform.localScale = new Vector3(5, 5, 1);
    //        //bullet.GetComponent<StraightShotBullet>().lifeTime = 0.3f;
    //        range = 3;
    //        bulletSpeed = 10;
    //        bulletObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
    //        bullet.Init(owner, direction, bulletSpeed, damage,range, bulletColor,target);
    //    }
    //}
    //public void TentacleAttack(GameObject target)
    //{
    //    bulletName = "BulletTentacle";
    //    bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullets/" + bulletName);
    //    fireRate = 2;

    //    foreach (Transform t in sockets)
    //    {
    //        Vector3 direction = new Vector3(0, 0, 0);
    //        direction = direction.normalized;
    //        GameObject bulletObj = MonoBehaviour.Instantiate(bulletPrefab, t.position +
    //            (direction * barrelLen), Quaternion.identity) as GameObject;
    //        Bullet bullet = bulletObj.GetComponent<Bullet>();
           
    //        bullet.Init(owner, direction, bulletSpeed, damage, range, bulletColor, target);
    //    }
    //}
    //void SpawnEnemyAttack()
    //{
    //    fireRate = 2f;

    //    Vector3 direction = new Vector3(0, 0, 0);
    //        direction = direction.normalized;
    //        GameObject spawnedEnemyObj = MonoBehaviour.Instantiate(or1EnemyPhase1, sockets[0].position +
    //            (direction * barrelLen), Quaternion.identity) as GameObject;
    //    //spawnedEnemyObj.GetComponent<TestEnemy>().Deactivate();

    //    //spawnedEnemyObj.GetComponent<TestEnemy>().tempCondition=true;//Temp coment, this line is needed
    //    //spawnedEnemyObj.GetComponent<TestEnemy>().ActivateWeapon(
    //    //       spawnedEnemyObj.GetComponent<Character>().GetInventory().Weapons[0]);

    //}
    
   
}
