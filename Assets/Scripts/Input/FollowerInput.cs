using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class FollowerInput : MonoBehaviour, IInputModule
{
    public event Action<int> OnColorSwitch;
    public event Action<ICharacter> OnAimLock;
    public event Action<GameObject> OnTap;


    // Variables

    public bool isFrozen { get; set; }

    Vector3 originPos;
    Vector2 direction;
    public GameObject player;
    Stats getStats;
    private IEnumerator coroutine;

    float speed = 5;

    ICharacter character;

    public void Init()
    {
        originPos = transform.position;
        player = GameObject.Find("Player");
        //phase = GetComponent<BlackBossCombat>().phase;
        getStats = GetComponent<Follower1>().GetStats();
        character = GetComponent<ICharacter>();

        //print("Before WaitAndPrint Finishes " + Time.time);
    }


    public void Update()
    {

        UpdateDirection();
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    void UpdateDirection()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist > 2)
        {
            getStats.SetSpeed(5);
            direction = player.transform.position - transform.position;
            if (dist > 5)
            {
                transform.position = player.transform.position;
            }
        }
        
        else
        {
            direction = new Vector3(0.0f, 0.0f, 0.0f);
            getStats.SetSpeed(0);
        }

    }

    
}
