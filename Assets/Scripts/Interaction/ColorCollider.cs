using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class ColorObject : MonoBehaviour{

    public Colors color;

    GameManager gm;
    Collider2D coll;
    SpriteRenderer sprite;

    void Start() {
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        gm.WorldColorChange += OnWorldColorChange;
        sprite.color = Static.worldColors[color];
        OnWorldColorChange(gm.WorldColor);
    }

    void Update() {
        
    }

    void OnWorldColorChange(Colors color){
        if(color == this.color){
            coll.enabled = false;
        }else{
            coll.enabled = true;
        }
    }

    void OnDestroy(){
        gm.WorldColorChange -= OnWorldColorChange;
    }
}
