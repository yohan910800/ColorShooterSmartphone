using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class LastBossUltimateBullet : Bullet
{
    float lifeTime;
    float speed;
    float range;

    public bool hitInARow;//yohan added , probably tmp
    ICharacter player;//yohan added , probably tmp


    Vector3 direction;

    void Start()
    {
        Invoke("Terminate", lifeTime);
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
    }

    void Update()
    {
        if (Owner.phase != 6)
        {
            Destroy(gameObject);
        }
        if (!isDissapearing)
            transform.position += direction * speed * Time.deltaTime;

        if (hitInARow == true)//for lastBoss
        {
            player.GetGameObject().GetComponent<Player>().GetHit(1);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")//for lastBoss
        {
            //player = character;
            player = collision.GetComponent<ICharacter>();
            hitInARow = true;

        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")//for lastBoss
        {
            //player = character;
            player = collision.GetComponent<ICharacter>();
            hitInARow = false;

        }
    }
}
