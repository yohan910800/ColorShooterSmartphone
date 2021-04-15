using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
	AI that stands still
*/
public class NoMovementAI : MonoBehaviour, IInputModule {
    // Events
    public event Action<int> OnColorSwitch;
    public event Action<ICharacter> OnAimLock;
    public event Action<GameObject> OnTap;

    // Variables
    Vector2 direction;

    public void Init() {

        direction = Vector2.zero;
    }

    public void Update() {

        
    }

    public Vector2 GetDirection() {
        return direction;
    }
}