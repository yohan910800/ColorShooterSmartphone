using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseControllerInteraction : Interactable
{
    MapManager mapManager;
    //public SaveSystemManager saveSystemManager;
    protected override void Start()
    {
        base.Start();
        if (GameObject.Find("MapManager").GetComponent<MapManager>() != null)
        {
            mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        //player.GetHit(100);
        if (collision.name == "Player")
        {
            //saveSystemManager.OnHitSaveData(collision);

            mapManager.ActivatePhase(mapManager.phase);
            mapManager.phase++;
        }
        
    }
}
