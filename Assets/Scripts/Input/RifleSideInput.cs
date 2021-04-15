using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleSideInput : MonoBehaviour, IInputModule
{
    // Events
    public event Action<int> OnColorSwitch;
    public event Action<ICharacter> OnAimLock;
    public event Action<GameObject> OnTap;

    // Variables
    Vector2 direction;
    //Level3MapManager mapManager;
    public void Init()
    {
        //mapManager = GameObject.Find("MapManager").GetComponent<Level3MapManager>();
        direction = Vector2.zero;
    }

    public void Update() {

        //if (mapManager.phase >= 10)
        //{
        //    GetComponent<PathFinderAI>().enabled = true;
        //    GetComponent<NoMovementAI>().enabled = false;
        //}

    }

    public Vector2 GetDirection()
    {
        return direction;
    }
}
