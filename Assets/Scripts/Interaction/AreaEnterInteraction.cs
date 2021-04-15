using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using TMPro;
public class AreaEnterInteraction : Interactable {
    
    
    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == ("Player")){
            //OnTriggered();
            //GetInteractiblePosition();
            //mapManager.ActivatePhase();
            //SetAimingState();

            if (gameObject.tag == "NPCDialogZone")
            {
                //SetDialogToNPC();
                //gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.
                //    GetComponent<TextMeshPro>().SetText("試し");

                gameObject.SetActive(false);
            }
            OnTriggered();
        }
    }
}
