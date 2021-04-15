using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
/*
	Moves left and right relative to his aiming direction
*/
public class SideStepAI : MonoBehaviour, IInputModule {

	// Events
    public event Action<int> OnColorSwitch;
	public event Action<ICharacter> OnAimLock;
	public event Action<GameObject> OnTap;

	// Variables
	Vector2 direction;
	float time;
	bool inverse;
	ICombat combat;

    public void Init(){
		direction = Vector2.zero;
		// Since it will be switching directions every 2 seconds
		// I'll start the timer at half so it stays near the spawn point
		time = 1;
		combat = GetComponent<ICombat>();
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
			inverse = !inverse;
		}
		if(inverse){
			direction = Vector3.Cross(combat.GetAimDirection(),Vector3.forward);
		}else{
			direction = Vector3.Cross(Vector3.forward,combat.GetAimDirection());
		}
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<BoxCollider2D>());

        }
        if (collision.gameObject.layer == 12)
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<BoxCollider2D>());
        }
    }
}