using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class SimpleNPCMovementAI : MonoBehaviour, IInputModule {

	// Events
    public event Action<int> OnColorSwitch;
	public event Action<ICharacter> OnAimLock;
	public event Action<GameObject> OnTap;

	// Variables
	Vector2 direction;
	float time;
	bool inverse;
	ICombat combat;
SimpleNPCMovement simpleNPCMovement;
    public void Init(){
direction = Vector3.zero;
    }

    public void Update(){

    }

    public Vector2 GetDirection(){
        simpleNPCMovement = GetComponent<SimpleNPCMovement>();
         Debug.Log("NPC inputmodule GetDirection Executed");
		return simpleNPCMovement.getDirection();
       
    }

}