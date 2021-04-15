using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class LifeStealProjectile : Bullet {

    public bool IsAttached{get; private set;}

    Transform anchor;
    LineRenderer lr;
    Transform targetTr;
    ICharacter targetChar;
    BoxCollider2D boxCol;
    float dmgInterval = 0.5f;
    float time;
    
    float lifeTime;
    float speed;
    float range;
    Vector3 direction;

    public override void Init(ICharacter owner, Vector3 direction, float speed, int dmg, float range, Colors color, GameObject target = null) {
        Owner = owner;
        Damage = dmg;
        BulletColor = color;
        sprite = GetComponentInChildren<SpriteRenderer>();
        lr = GetComponent<LineRenderer>();
        if (Static.bulletColors.ContainsKey(color)){
            sprite.color = Static.bulletColors[color];
            lr.endColor = Static.bulletColors[color];
        }
        this.speed = speed;
        this.range = range;
        lifeTime = range/speed;
        this.direction = direction;
        base.Init();
        boxCol = (BoxCollider2D)coll;
    }

    public void AttachAnchor(Transform anchor){
        this.anchor = anchor;
    }

    void Update(){
        time += Time.deltaTime;
        if(!IsAttached && time >= lifeTime){
            Terminate();
            return;
        }
        UpdatePosition();
        UpdateLine();
        if (IsAttached) UpdateEffects();
    }

    void UpdateLine(){
        lr.SetPosition(0, anchor.position);
        lr.SetPosition(1, transform.position);
        float distance = Vector3.Distance(anchor.position,transform.position);
        boxCol.size = new Vector2(distance,boxCol.size.y);
        boxCol.offset = new Vector2(-distance/2,0);
    }

    void UpdatePosition(){
        if(!isDissapearing){
            if (IsAttached){
                transform.position = targetTr.position;
            }else{
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }

    void UpdateEffects(){
        if(time>=dmgInterval){
            time = 0;
            targetChar.HitCheck(this);
            targetChar.GetStats().ModifySpeed(-0.5f);
            Owner.GetHeal(Damage/2);
        }
    }
    
    protected override void OnHit(ICharacter character){
        targetTr = character.GetGameObject().transform;
        targetChar = character;
        IsAttached = true;
        time = 0;
    }

    protected override void Terminate(){
        if(targetChar!=null){
            targetChar.GetStats().ResetSpeed();
        }
        base.Terminate();
    }
}