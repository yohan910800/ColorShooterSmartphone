using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System;

public class Interactable : MonoBehaviour {

    public event Action OnTrigger;

    protected Player player;
    
    protected TestInteractionManager interactionManager;

    //protected Vector2 interactiblePos;
    //protected bool activateAutoAim;
    //protected bool triggerCondition;
    

    protected virtual void Start(){
        //added_dohan--------------------------------------------
        player = GameObject.Find("Player").GetComponent<Player>();
        //-------------------------------------------------------
        //if (GameObject.Find("MapManager").GetComponent<MapManager>() != null)// edited_dohan- caused an error in redCityMap. This would fix it while un-effecting other scenes
        //{
        //    mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        //}
        //mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        //interactionManager = GetComponent<TestInteractionManager>();

        Init();
    }

    protected virtual void Init() { }

    protected virtual void OnTriggered(){
        if(OnTrigger != null) OnTrigger();
        Log.log("Triggered " + name);
        
        //mapManager.prefabs[0].GetComponent<Ranged1>().ShowDialogText();
        
    }
    //protected virtual void GetInteractiblePosition()
    //{
    //        interactiblePos = transform.position;
    //        mapManager.interactPos = interactiblePos;
    //}
    
    //protected virtual void GetPlayer()
    //{
    //    player = mapManager.player;
    //}

    //protected virtual void SetAimingState()
    //{
    //    GetPlayer();
    //    player.ActivateAutoAim = mapManager.isItAutoAiming;
    //}





    //protected virtual void SetDialogToNPC()
    //{
    //    if (mapManager.dialogZoneCounter == 2)
    //    {
    //        mapManager.dialogZoneCounter = 3;
    //    }

    //    interactionManager.jsonFileDialogName = gameObject.name;

    //    Log.log("enemy name" + mapManager.enemyArray[mapManager.dialogZoneCounter].
    //        GetGameObject().name);
    //    mapManager.enemyArray[mapManager.dialogZoneCounter].GetGameObject()
    //        .GetComponent<Ranged1>().ShowDialogText();

        

    //    interactionManager.dialog = mapManager.
    //        enemyArray[mapManager.dialogZoneCounter].
    //        GetGameObject().GetComponent<Ranged1>().floatingDialog;

    //    mapManager.dialogZoneCounter++;
    //}

}
