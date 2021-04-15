using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
public class Laser : Bullet
{
    public float lifeTime = 60.0f;
    float speed = 20;
    float timer;
    Vector3 direction;
    BoxCollider2D bxCollider;
    Transform lasereffect;
    Rigidbody2D rb;

   
    public override void Init(ICharacter owner, Vector3 direction, float speed, int dmg,
        float range, Colors color, GameObject target = null)
    {
        Owner = owner;
        Damage = dmg;
        this.speed = speed;
        BulletColor = color;
        this.direction = direction.normalized;
        sprite = GetComponentInChildren<SpriteRenderer>();
        bxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        if (Static.bulletColors.ContainsKey(color))
        {
            sprite.color = Static.bulletColors[color];
        }

        base.Init();

       // Physics2D.IgnoreLayerCollision(8, 8);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifeTime)
        {
            Terminate();
        }
        transform.position += direction * speed * Time.deltaTime;
        transform.right = direction.normalized;
    }
    protected override void OnHit(ICharacter character)
    {
        return;
    }
    protected override void OnHit()
    {
        return;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 12)
        {
            Vector3 originalDirection = direction;
            direction = Vector2.Reflect(originalDirection, collision.contacts[0].normal);
        }
        else
        {
            Physics2D.IgnoreCollision(bxCollider, collision.collider, true);
        }
        ICharacter character = collision.gameObject.GetComponent<ICharacter>();
        if (character != null)
        {
            character.HitCheck(this);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 12)
        {
            Vector3 originalDirection = direction;
            direction = Vector2.Reflect(originalDirection, collision.contacts[0].normal);
        }
        else
        {
            Physics2D.IgnoreCollision(bxCollider, collision.collider, true);
        }
        ICharacter character = collision.gameObject.GetComponent<ICharacter>();
        if (character != null)
        {
            character.HitCheck(this);
        }
    }
}
