using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class FishHeadBullet : Bullet
{
    public float lifeTime = 6.0f;
    public float speed;
    float range;

    float distY;
    float distX;
    bool isAttractedBybullet;
    bool isGoingToDirection;
    GameObject target;
    Vector3 direction;
    Vector3 directionTarget;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public override void Init(ICharacter owner, Vector3 direction, float speed, int dmg, float range, Colors color, GameObject target = null)
    {
        Owner = owner;
        Damage = dmg;
        BulletColor = color;
        this.speed = speed;
        this.range = range;
        lifeTime = range / speed;
        this.direction = direction.normalized;
        sprite = GetComponentInChildren<SpriteRenderer>();
        if (Static.bulletColors.ContainsKey(color))
        {
            sprite.color = Static.bulletColors[color];
        }
        base.Init();

        Owner = owner;
        Damage = dmg;
        BulletColor = color;
        this.direction = direction.normalized;
        isGoingToDirection = true;
    }

    void Update()
    {
        if (target != null)
        {
            directionTarget = transform.position - target.transform.position;
        }
        if (speed > 0)
        {
            speed -= Time.deltaTime * 2;
        }
        if (isGoingToDirection == true)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
        if (isAttractedBybullet == true)
        {
            target.transform.position += directionTarget * 2 * Time.deltaTime;
        }
    }

    protected override void OnHit(ICharacter character)
    {
        //direction = new Vector3(0,0,0);
        target = character.GetGameObject();
        isAttractedBybullet = true;
        isGoingToDirection = false;
        Log.log("entter");

        //Destroy(gameObject);
    }
}
