using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteIfGetOutAreaInteractable : Interactable
{
    bool isItOut=false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
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
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            isItOut = true;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (isItOut == true)
        {
            collision.gameObject.SetActive(false);
        }
    }
}
