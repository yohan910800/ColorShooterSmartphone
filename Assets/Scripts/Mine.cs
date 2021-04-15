using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class Mine : Bullet
{

    float lifeTime;
    float speed;
    float range;

    Vector3 direction;
    GameObject explosionEffect;
    AudioManager audioManager;
    
    void Start()
    {
        Invoke("Terminate", lifeTime);
        audioManager = GameObject.Find("GameManager").GetComponent<GameManager>().audioManager;
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
        owner = GameObject.Find("Player").GetComponent<ICharacter>();
        base.Init();
        explosionEffect = Resources.Load<GameObject>("Prefabs/Effects/MineExplodeEffect");

    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        
        ICharacter character = other.gameObject.GetComponent<ICharacter>();
        if (other.gameObject.layer == 10 || other.gameObject.layer == 12)
        {
            OnHit();
        }
        if (character != null)
        {
            
            bool isHit = character.HitCheck(this);
            if (isHit)
            {
                OnHit(character);
                GameObject explosionEffectObj = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(explosionEffectObj, 3.0f);
                StartCoroutine(MineExploseSound(1.0f));
                if (doesPoisonedBulletIsActivate == true)
                {
                    character.isPoisoned = true;
                }
               
            }
        }
       
    }
    IEnumerator MineExploseSound(float duration)
    {
        float timer = 0.0f;
        while (timer < duration+1.0f)
        {
            if (timer < duration)
            {
                audioManager.Play("bipBeforeExplosion");
            }
            else
            {
                audioManager.Play("MineExplosion");
            }

            timer += Time.deltaTime;

        }
        yield return null;
    }
}
