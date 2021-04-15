using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System.Linq;

public class DamageZone : Bullet {

    public float damageInterval = 1f;
    public float lifeTime = 5f;

    Dictionary<ICharacter,float> targets = new Dictionary<ICharacter,float>();

    void Start() {
        Invoke("Terminate",lifeTime);
    }

    public override void Init(ICharacter owner, Vector3 direction, float speed, int dmg, float range, Colors color, GameObject target = null){
        Owner = owner;
        Damage = dmg;
        BulletColor = color;
        sprite = GetComponentInChildren<SpriteRenderer>();
        if(Static.bulletColors.ContainsKey(color)){
            sprite.color = Static.bulletColors[color];
        }
        base.Init();
    }

    void Update() {
        ICharacter[] chars = targets.Keys.ToArray();
        foreach(ICharacter character in chars){
            targets[character] -= Time.deltaTime;
            if(targets[character]<=0){
                targets[character] = damageInterval;
                HitCheck(character);
            }
        }
    }

    void HitCheck(ICharacter character){
        bool isHit = character.HitCheck(this);
        if(isHit){
            OnHit(character);
        }
    }

    protected override void OnHit(ICharacter character){
        // Hit feedback here (like sounds or efects)
    }

    protected override void Terminate(){
        foreach (ICharacter character in targets.Keys){
            character.OnDeathEvent-=OnTargetDeath;
        }
        base.Terminate();
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        ICharacter character = other.gameObject.GetComponent<ICharacter>();
        if(character != null){
            targets.Add(character,damageInterval);
            character.OnDeathEvent+=OnTargetDeath;
            HitCheck(character);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        ICharacter character = other.gameObject.GetComponent<ICharacter>();
        if(character != null && targets.ContainsKey(character)){
            targets.Remove(character);
            character.OnDeathEvent-=OnTargetDeath;
        }
    }

    void OnTargetDeath(ICharacter character){
        character.OnDeathEvent-=OnTargetDeath;
        targets.Remove(character);
    }
}
