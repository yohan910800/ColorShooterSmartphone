using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class Bullet : MonoBehaviour {

    public ICharacter Owner {get; protected set;}
    public int Damage {get; protected set;}
    public Colors BulletColor {get; protected set;}
    //bonus 
    public bool doesPoisonedBulletIsActivate { get; set; }

    //
    protected GameManager gm;
    protected Animator anim;
    protected Collider2D coll;
    protected bool isDissapearing;
    protected SpriteRenderer sprite;


    public virtual void Init(ICharacter owner, Vector3 direction, float speed,
        int dmg, float range, Colors color, GameObject target = null){
        Log.log("This bullet has no Init implementation ("+gameObject.name+")");
    }

    protected virtual void Init(){
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        //gm.WorldColorChange += OnWorldColorChange;
        Owner.OnDeathEvent += OnOwnerDeath;
        doesPoisonedBulletIsActivate = false;
    }

    protected virtual void OnHit(ICharacter character){
        Terminate();
    }

    protected virtual void OnHit(){

        Terminate();
    }

    protected virtual void Terminate(){
        //gm.WorldColorChange -= OnWorldColorChange;
        Owner.OnDeathEvent -= OnOwnerDeath;
            Destroy(gameObject);
    }

    //protected virtual void DisableIfSameColor()
    //{
    //    gm.WorldColorChange -= OnWorldColorChange;
    //}

    //protected virtual void OnWorldColorChange(Colors color){
    //    if(color == BulletColor){
    //        coll.enabled = false;
    //        sprite.color = Color.white;
    //        anim.SetTrigger("disappear");
    //        isDissapearing = true;
    //        Invoke("Terminate",0.5f);
    //    }
    //}
    
      
    


    //yohan added
    //protected virtual void OnWorldColorChange(Colors color)
    //{
    //    if (color == BulletColor)
    //    {
    //        coll.enabled = false;
    //        //anim.SetTrigger("disappear");
    //        isDissapearing = true;
    //        //Invoke("Terminate", 0.5f);
    //    }
    //    else
    //    {
    //        coll.enabled = true;
    //        isDissapearing = false;
    //    }
    //}

    protected virtual void OnOwnerDeath(ICharacter character){
        Terminate();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        ICharacter character = other.gameObject.GetComponent<ICharacter>();
        
        if (other.gameObject.layer == 10|| other.gameObject.layer == 12)
        {
            OnHit();
        }
        if(character != null){
            bool isHit = character.HitCheck(this);
            if (isHit){
                
                OnHit(character);
                
                if (doesPoisonedBulletIsActivate == true)
                {
                    character.isPoisoned = true;
                }
                //character.GetGameObject().GetComponent<Rigidbody2D>().
                //    AddForce(Vector2.Reflect(character.GetAimDirection(),
                //                     -character.GetAimDirection()
                //                      ) * 0.009f, ForceMode2D.Impulse);
            }
        }else{
            //OnHit();//yohan added
        }
    }
}
