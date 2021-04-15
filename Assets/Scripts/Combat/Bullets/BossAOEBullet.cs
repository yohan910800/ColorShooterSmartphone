using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class BossAOEBullet : Bullet
{
    public float lifeTime = 9f;
    public float speed = 0f;
    public GameObject stopPos;
    public GameObject enemy;

    CircleCollider2D col;
    LineRenderer lr;
    Vector2 originPos;
    GameObject player;


    bool isOnPlayerPos;
    float scaleSpeed;
    float timer;
    float timerDmg;
  

    public override void Init(ICharacter owner, Vector3 direction, float speed, int dmg, float range, Colors color, GameObject target = null)
    {
        enemy = GameObject.FindGameObjectWithTag("Boss");
        player = GameObject.FindGameObjectWithTag("Player");

        lr = GetComponent<LineRenderer>();
        originPos = transform.position;

        //Destroy(gameObject,lifeTime);
        Owner = owner;
        Damage = dmg;
        BulletColor = color;
        this.speed = speed;
        sprite = GetComponentInChildren<SpriteRenderer>();
        if (Static.bulletColors.ContainsKey(color))
        {
            sprite.color = Static.bulletColors[color];
        }
        base.Init();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer>= lifeTime)
        {
            Terminate();
        }
        if (Owner != null)
        {
            AttackSpread();
        }
    }

    void AttackSpread()
    {
        scaleSpeed = speed/2 * Time.deltaTime;
        /*transform.GetChild(0).*/gameObject.transform.localScale += new Vector3
            (scaleSpeed, scaleSpeed, scaleSpeed);
        if (isOnPlayerPos == true)
        {

        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ICharacter character = other.gameObject.GetComponent<ICharacter>();
            isOnPlayerPos = true;
        }
    }
    protected void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            timerDmg += Time.deltaTime;
            if (timerDmg >= 0.7f)
            {
                ICharacter character = other.gameObject.GetComponent<ICharacter>();
                character.HitCheck(this);
                timerDmg=0;
            }
        }
    }

    protected override void OnHit(ICharacter character)
    {
        return;
    }
    protected override void OnHit()
    {
        return;
    }


}
