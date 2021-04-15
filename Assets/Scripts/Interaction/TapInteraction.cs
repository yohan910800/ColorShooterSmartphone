using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class TapInteraction : Interactable {

    public bool hasActiveArea = true;
    public GameObject target;
    bool isMoving;
    bool inArea;
    IInputModule input;

    protected override void Init(){
        if(player.GetInputModule() != null) OnInputModuleChange();
        player.OnInputModuleChange += OnInputModuleChange;
        if (target == null) target = gameObject;
    }

    void OnInputModuleChange(){
        if(input != null)
            input.OnTap-=OnTap;
        input = player.GetInputModule();
        input.OnTap+=OnTap;
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            inArea = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other){
        if (other.tag == "Player"){
            inArea = false;
        }
    }

    void OnTap(GameObject gObj){
        isMoving = input.GetDirection() != Vector2.zero;
        inArea = hasActiveArea? inArea : true;
        if(inArea && !isMoving && gObj == target){
            OnTriggered();
        }
    }

    void OnDestroy(){
        input.OnTap-=OnTap;
    }

}
