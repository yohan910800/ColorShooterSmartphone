using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class BlackBossInput : MonoBehaviour, IInputModule
{
    // Events
    public event Action<int> OnColorSwitch;
    public event Action<ICharacter> OnAimLock;
    public event Action<GameObject> OnTap;


    // Variables

    public bool isFrozen { get; set; }

    public Vector3 originPos;
    Vector2 direction;
    public GameObject player;
    Stats getStats;
    private IEnumerator coroutine;

    int phase;
    bool isResting;
    bool isAttacking;
    float speed = 5;
    float timerPlayerLastPos;
    Vector3 playerLastPos;
    ICharacter character;
    public void Init()
    {

        originPos = transform.position;
        player = GameObject.Find("Player");
        //phase = GetComponent<BlackBossCombat>().phase;
        getStats = GetComponent<Ranged1>().GetStats();
        
        coroutine = WaitAndAttack(0.6f);
        StartCoroutine(coroutine);

        character = GetComponent<ICharacter>();
        phase = character.phase;

        //print("Before WaitAndPrint Finishes " + Time.time);
    }


    public void Update()
    {
        phase = character.phase; 

        UpdateDirection();
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    void UpdateDirection()
    {
        float dist = Vector3.Distance(transform.position,playerLastPos);
        if (phase == 2)
        {
            timerPlayerLastPos += Time.deltaTime;
            if (timerPlayerLastPos > 1.6f)
            {

                playerLastPos = player.transform.position;

                timerPlayerLastPos = 0;
            }

            if (isResting == false)
            {
                direction = playerLastPos - transform.position;
                if (dist <= 0.3f)
                {
                    getStats.SetSpeed(0);
                }
                else
                {

                    getStats.SetSpeed(18);
                }

            }
            else if (isResting == false)
            {
                getStats.SetSpeed(0);
            }
            else if (isAttacking == false)
            {
                getStats.SetSpeed(0);
            }
            //direction = new Vector3(0, 0, 0);
        }
        else if(phase==1)
        {
            getStats.SetSpeed(3);
            transform.position = originPos;
        }
        else if (phase == 3)
        {

            transform.position = originPos;
        }
        else
        {
            return;
        }
    }

    private IEnumerator WaitAndAttack(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            isResting = true;
            isAttacking = false;
            yield return new WaitForSeconds(1f);
            isAttacking = true;
            isResting = false;
        }
    }

}
