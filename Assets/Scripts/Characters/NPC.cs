using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class NPC : Character {

    Transform[] singleSocket;
    Transform[] dualWieldSockets;

    SimpleNPCMovement SimpleNPCMovement;
    SimpleNPCMovementAI simpleNPCMovementAI;
    PathFinderAI pathFinderAI;
    //NPCFollowMovement nPCFollowMovement;
    public bool FollowNPC;

    public override void Init(){   
      
        if(gameObject.name=="LostBoi")FollowNPC = true;
        stats = new Stats(0,0,0,0,0,0);
        InitInput();
        InitInventory();
        movement = GetComponent<IMovement>();   
        movement.Init(this);
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        damageTextPrefab = Resources.Load<GameObject>("Prefabs/DamageText");
      
        IsAlive = true;  
        drops = new Drops(0,0);
        gm.WorldColorChange+=OnWorldColorChange;
        var weaponSocket1 = transform.Find("Hand1");
        var weaponSocket2 = transform.Find("Hand2");
        dualWieldSockets = new Transform[]{weaponSocket1,weaponSocket2};
        singleSocket = new Transform[]{weaponSocket2};

        // TEMP // Hard coded weapons atm 
        // this changes when we get a save/load system
        AddColor(Colors.Red);
        SetBulletColor(Colors.Red);

        if (FollowNPC){
            //pathFinderAI = GetComponent<PathFinderAI>();
            //nPCFollowMovement = GetComponent<NPCFollowMovement>();


        }

        else

        {
            SimpleNPCMovement = GetComponent<SimpleNPCMovement>();
            simpleNPCMovementAI = GetComponent<SimpleNPCMovementAI>();
        }

        animationManager = GetComponent<AnimationManager>();
       
        if(animationManager != null) animationManager.Init();

    }

    protected override void InitInput(){
        base.InitInput();
    }

    protected override void OnDeath(){
        IsAlive = false;
        stateUI.OnDeath();
        base.OnDeath();
        Destroy(gameObject);
    }


    public override IInputModule GetInputModule()
    {
        if (FollowNPC)
        {
            return pathFinderAI;
        }
        else
            return simpleNPCMovementAI;
    }
    public override Vector3 GetAimDirection()
    {//{if (FollowNPC) {
    //        return nPCFollowMovement.getDirection();

    //    }
    //else
        return SimpleNPCMovement.getDirection();
    }

   protected override void OnDestroy(){
        input.OnColorSwitch -= BroadcastColorSwitch;
        gm.WorldColorChange -= OnWorldColorChange;
        movement.Terminate();
       
    }
}
