using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
/*
	Simple enemy that moves left and right in time intervals
*/
public class EnemyInputSample1 : MonoBehaviour, IInputModule {

	// Events
    public event Action<int> OnColorSwitch;
	public event Action<ICharacter> OnAimLock;
	public event Action<GameObject> OnTap;

	// Variables
	Vector2 direction;
	float time;

    public void Init(){
		// Randomly choose left or right
		float startDir = UnityEngine.Random.Range(1,100) % 2 == 0? 1 : -1;
		direction = new Vector2(startDir,0);
		// Since it will be switching directions every 2 seconds
		// I'll start the timer at half so it stays near the spawn point
		time = 1;
    }

    public void Update(){
		if(Static.GamePaused) return;
		UpdateDirection();
    }

    public Vector2 GetDirection(){
		return direction;
	}

	void UpdateDirection(){
		// Invert direction every 2 seconds
		time+=Time.deltaTime;
		if(time>= 2){
			time = 0;
			direction *= -1;
		}
	}
}