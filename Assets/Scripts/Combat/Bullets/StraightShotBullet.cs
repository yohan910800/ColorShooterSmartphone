using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
/*
Simple bullet that moves in one direction and dissapears after a time or on hit
*/
public class StraightShotBullet : Bullet {
    float lifeTime;
    float speed;
    float range;

    Vector3 direction;

    void Start(){
            Invoke("Terminate", lifeTime);
    }

    public override void Init(ICharacter owner, Vector3 direction, float speed, int dmg, float range, Colors color, GameObject target = null){
        Owner = owner;
        Damage = dmg;
        BulletColor = color;
        this.speed = speed;
        this.range = range;
        lifeTime = range/speed;
        this.direction = direction.normalized;
        sprite = GetComponentInChildren<SpriteRenderer>();
        if(Static.bulletColors.ContainsKey(color)){
            sprite.color = Static.bulletColors[color];
        }

        base.Init();
    }

    void Update(){

        //Log.log("direction " +direction);

        if (Owner != null)
        {
            if (!isDissapearing)
            {
                transform.position += direction * speed * Time.deltaTime;
            }
            //else
            //{
            //    gameObject.SetActive(false);
            //}

            //yohan added
            if (BulletColor == gm.WorldColor)
            {
                coll.enabled = false;
                //anim.SetTrigger("disappear");
                isDissapearing = true;
                //Invoke("Terminate", 0.5f);
            }
            else
            {
                coll.enabled = true;
                isDissapearing = false;

            }
        }
    }
}
