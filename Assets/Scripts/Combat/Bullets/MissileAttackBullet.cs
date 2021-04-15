using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class MissileAttackBullet : Bullet

{
    GameObject getPlayer;
    GameObject bulletPrefab;

    float timer;
    float timerDestroy;
    float speed;
    float range;
    bool ActiveDestroyTimer = false;
    Vector3 direction;

    void Start()
    {
        Destroy(gameObject, 6);
        GetComponent<CircleCollider2D>().enabled = false;

        getPlayer = GameObject.FindGameObjectWithTag("Player");
        transform.position = getPlayer.transform.position;

        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullets/MissileAttackBullet");
    }

    public override void Init(ICharacter owner, Vector3 direction, float speed, int dmg, float range, Colors color, GameObject target=null)
    {
        Owner = owner;
        Damage = dmg;
        BulletColor = color;
        this.speed = speed;
        this.range = range;
        this.direction = direction.normalized;
        sprite = GetComponentInChildren<SpriteRenderer>();
        if (Static.bulletColors.ContainsKey(color))
        {
            sprite.color = Static.bulletColors[color];
        }sprite.color = Color.white;
        base.Init();
    }

    void Update()
    {
        //Log.log("owner " +Owner.GetGameObject().name);
        if (Owner!= null)
        { 
            if (!isDissapearing)
                transform.position += direction * speed * Time.deltaTime;

            if (ActiveDestroyTimer == false)
            {
                timer += Time.deltaTime;

                if (timer > 2)

                {
                    OnSpawnMissile();

                    ActiveDestroyTimer = true;
                }
            }

            if (ActiveDestroyTimer == true)
            {
                timerDestroy += Time.deltaTime;
                if (timerDestroy > 2)
                {
                    Terminate();
                }
            }
        }
    }

    void OnSpawnMissile()
    {
        if (Owner != null)
        {
            GameObject bulletObj = MonoBehaviour.Instantiate(bulletPrefab, transform.position
             , Quaternion.identity);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bullet.Init(Owner, new Vector3(0.0f, 0.0f, 0.0f), 0.1f, Damage, 0.2f, Colors.Red);
        }
    }
}
