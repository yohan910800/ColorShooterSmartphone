using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class ColorCollider : MonoBehaviour{

    public Colors color;
    public bool touchedByTantacle=false;
    GameManager gm;
    Collider2D coll;

    SpriteRenderer sprite;
    void Start() {
        coll = GetComponent<Collider2D>();
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        gm.WorldColorChange += OnWorldColorChange;

        sprite = GetComponent<SpriteRenderer>();
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
