using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class NoMovementInteraction : Interactable {

    protected bool isTriggered;

    void OnTriggerStay2D(Collider2D other){
        if (other.tag == "Player"){
            if (player.GetInputModule().GetDirection() == Vector2.zero){
                if (!isTriggered){
                    OnTriggered();
                    isTriggered = true;
                }
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D other){
        if (other.tag == "Player"){
            isTriggered = false;
        }
    }

}
