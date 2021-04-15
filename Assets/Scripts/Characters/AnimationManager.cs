using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MankindGames;

public class AnimationManager : MonoBehaviour {

    public Sprite bodySprite;
    public Sprite bodyTiltedSprite;
    public Sprite faceSprite;
    public Sprite faceTiltedSprite;
    public Sprite handSprite;
    public Sprite hairSprite;
    public Sprite meleeHandSprite;

    public Animator animator;
    ICharacter character;
    IInputModule input;

    SpriteRenderer bodyRenderer;
    SpriteRenderer faceRenderer;
    SpriteRenderer foot1Renderer;
    SpriteRenderer foot2Renderer;
    SpriteRenderer hairRenderer;
    SpriteRenderer meleeHandRenderer;

    public Hand hand1;
    public Hand hand2;
    Hand[] hands;
    

    Vector3 dir;
    float testangle;
    bool isInitialized;

    
    Orientation orientation;
    Weapon weapon;

    

    public void Init() {
        animator = GetComponent<Animator>();
        character = GetComponent<ICharacter>();
        bodyRenderer = transform.Find("Body").GetComponent<SpriteRenderer>();
        faceRenderer = transform.Find("Face").GetComponent<SpriteRenderer>();
        foot1Renderer = transform.Find("Foot1").GetComponent<SpriteRenderer>();
        foot2Renderer = transform.Find("Foot2").GetComponent<SpriteRenderer>();
        hairRenderer = transform.Find("Hair").GetComponent<SpriteRenderer>();
        if (gameObject.tag == "FourHandsBoss")
        {
            hand1 = new Hand(transform.Find("TurnObject1").Find("Hand1"), handSprite, true);//for FourHandsBoss
            hand2 = new Hand(transform.Find("TurnObject1").Find("Hand2").transform, handSprite);//for FourHandsBoss
            hands = new Hand[] { hand1, hand2 };

        }
        else if (gameObject.tag == "OneHandBoss")
        {
            hand1 = new Hand(transform.Find("TurnObject1").Find("Hand1"), handSprite, true);//for for OneHandBoss
            hand2 = new Hand(transform.Find("Hand2").transform, handSprite);//for OneHandBoss
            hands = new Hand[] { hand1, hand2 };
        }
        else
        {
            hand1 = new Hand(transform.Find("Hand1"), handSprite, true);
            hand2 = new Hand(transform.Find("Hand2"), handSprite);
            hands = new Hand[] { hand1, hand2 };
        }
        input = character.GetInputModule();
        isInitialized = true;
        orientation = Orientation.Down;
        character.OnInputModuleChange += OnInputModuleChange;
        character.OnWeaponChange += OnWeaponChange;
        SetSpriteColors(character.GetColor());

        
    }

    void SetSpriteColors(Colors color){
        if(!Static.characterColors.ContainsKey(color)){
            //Log.log("This character color does not exist! " + color.ToString());
            return;
        }
        bodyRenderer.color = Static.characterColors[color];
        foot1Renderer.color = Static.characterColors[color];
        foot2Renderer.color = Static.characterColors[color];
        hairRenderer.color = Static.characterColors[color];
        if(hand1.isEmpty) hand1.spriteRenderer.color = Static.characterColors[color];
        if(hand2.isEmpty) hand2.spriteRenderer.color = Static.characterColors[color];
    }

    void OnDestroy(){
        character.OnInputModuleChange -= OnInputModuleChange;
        character.OnWeaponChange -= OnWeaponChange;
    }

    void OnInputModuleChange(){
        input = character.GetInputModule();
    }

    void OnWeaponChange(Weapon weapon){
        foreach (Hand hand in hands){
            if(!Array.Exists(weapon.sockets, t => t == hand.transform)){
                hand.SetToEmpty(character.GetColor());
            }else{
                hand.isEmpty=false;
                hand.spriteRenderer.color = Color.white;
            }
        }
        SpriteUpdate(true);
    }

    void Update() {
        if (isInitialized)
        {
            SpriteUpdate();
            AnimationUpdate();
            //hand2.isEmpty = false;
            //Log.log("aim dir "+dir);
            //Log.log("is enpty " + hand2.isEmpty);
        }
    }

    

    void AnimationUpdate(){
        dir = input.GetDirection();
        if(dir.magnitude == 0){
            animator.SetBool("isWalking",false);
        }else{
            animator.SetBool("isWalking",true);
        }
    }


    Orientation GetOrientation(){
        
        dir = character.GetAimDirection();
        
        float angle = Vector3.Angle(Vector3.right, dir);
        testangle = angle;
        if(dir.y <= 0) {
            angle = 360.0f - angle;
        }
        if(angle < 135 && angle > 45){
            return Orientation.Up;
        }else if(angle <= 45 && angle > 10){
            return Orientation.UpRight;
        }else if(angle <= 360 && angle >= 315 || angle <= 10 && angle >= 0){
            return Orientation.DownRight;
        }else if(angle < 315 && angle > 225){
            return Orientation.Down;
        }else if(angle <= 225 && angle >= 170){
            return Orientation.DownLeft;

        }
        else
        {
            return Orientation.UpLeft;
        }
        
    }

    void SpriteUpdate(bool isForced = false){
        Orientation or = GetOrientation();
        if(orientation == or && !isForced) return;
        orientation = or;

        switch (orientation){
            case Orientation.Up:
                bodyRenderer.sprite = bodySprite;
                faceRenderer.enabled = false;
                //hair
                hairRenderer.enabled = true;
                bodyRenderer.enabled = false;
                //
                //put hairs here


                if (!hand1.isEmpty){
                    hand1.spriteRenderer.sortingOrder = -2;
                    hand1.spriteRenderer.flipY = true;
                }
                if(!hand2.isEmpty){
                    hand2.spriteRenderer.sortingOrder = -2;
                    hand2.spriteRenderer.flipY = false;  
                }
                break;
            case Orientation.UpRight:
                bodyRenderer.sprite = bodyTiltedSprite;
                bodyRenderer.flipX = true;
                faceRenderer.enabled = false;
                //hair
                hairRenderer.enabled = true;
                bodyRenderer.enabled = false;

                //
                if (!hand1.isEmpty){
                    hand1.spriteRenderer.sortingOrder = -2;
                    hand1.spriteRenderer.flipY = false;
                }
                if(!hand2.isEmpty){
                    hand2.spriteRenderer.sortingOrder = -2;
                    hand2.spriteRenderer.flipY = false;  
                }
                break;
            case Orientation.DownRight:
                bodyRenderer.sprite = bodyTiltedSprite;
                bodyRenderer.flipX = false;
                faceRenderer.enabled = true;
                hairRenderer.enabled = false;
                faceRenderer.sprite = faceTiltedSprite;
                faceRenderer.flipX = false;

                bodyRenderer.enabled = true;

                //
                if (!hand1.isEmpty){
                    hand1.spriteRenderer.sortingOrder = 2;
                    hand1.spriteRenderer.flipY = false;
                }
                if(!hand2.isEmpty){
                    hand2.spriteRenderer.sortingOrder = 2;
                    hand2.spriteRenderer.flipY = false;  
                }
                break;
            case Orientation.Down:
                bodyRenderer.sprite = bodySprite;
                faceRenderer.enabled = true;
                hairRenderer.enabled = false;
                faceRenderer.sprite = faceSprite;
                faceRenderer.flipX = false;

                bodyRenderer.enabled = true;

                //
                if (!hand1.isEmpty){
                    hand1.spriteRenderer.sortingOrder = 2;
                    hand1.spriteRenderer.flipY = false;
                }
                if(!hand2.isEmpty){
                    hand2.spriteRenderer.sortingOrder = 2;
                    hand2.spriteRenderer.flipY = true;  
                }
                break;
            case Orientation.DownLeft:
                bodyRenderer.sprite = bodyTiltedSprite;
                bodyRenderer.flipX = true;
                faceRenderer.enabled = true;
                //hair
                bodyRenderer.enabled = true;

                //
                hairRenderer.enabled = false;
                //
                faceRenderer.sprite = faceTiltedSprite;
                faceRenderer.flipX = true;

                if(!hand1.isEmpty){
                    hand1.spriteRenderer.sortingOrder = 2;
                    hand1.spriteRenderer.flipY = true;
                }
                if(!hand2.isEmpty){
                    hand2.spriteRenderer.sortingOrder = 2;
                    hand2.spriteRenderer.flipY = true;  
                }
                break;
            case Orientation.UpLeft:
                bodyRenderer.sprite = bodyTiltedSprite;
                bodyRenderer.flipX = false;
                faceRenderer.enabled = false;

                //hair
                bodyRenderer.enabled = false;
                hairRenderer.enabled = true;
                //hair here
                if (!hand1.isEmpty){
                    hand1.spriteRenderer.sortingOrder = -2;
                    hand1.spriteRenderer.flipY = true;
                }
                if(!hand2.isEmpty){
                    hand2.spriteRenderer.sortingOrder = -2;
                    hand2.spriteRenderer.flipY = true;  
                }
                break;
           
        }
    }
}


public class Hand {
    public Transform transform;
    public SpriteRenderer spriteRenderer;
    public bool isEmpty;
    public bool flipDefault;
    Sprite deafultSprite;

    public Hand(Transform t, Sprite sprite, bool flip=false){
        transform = t;
        spriteRenderer = t.GetComponent<SpriteRenderer>();
        isEmpty = true;
        flipDefault = flip;
        deafultSprite = sprite;
    }

    public void SetToEmpty(Colors color){
        isEmpty = true;
        transform.localEulerAngles = new Vector3(0,0,-90);
        spriteRenderer.flipY = flipDefault;
        spriteRenderer.sprite = deafultSprite;
        if(Static.characterColors.ContainsKey(color)){
            spriteRenderer.color = Static.characterColors[color];
        }
    }
}