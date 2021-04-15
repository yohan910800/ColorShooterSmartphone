using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System;

public class OneHandBossInput : MonoBehaviour,IInputModule
{
    // Events
    public event Action<int> OnColorSwitch;
    public event Action<ICharacter> OnAimLock;
    public event Action<GameObject> OnTap;


    // Variables
    //public bool isFrozen { get; set; }
    public int phase;
    public bool onPosition;

    int rnd;
    float time;
    float timeChoosePhase;

    float phaseDuration;
    float dist;
    float speed;

    bool isChangingPhase;


    Vector2 direction;
    Vector3 targetLastPos;
    Vector3 originPos;
    GameObject target;
    Stats getEnemyStats;

    public void Init()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        getEnemyStats = GetComponent<Ranged1>().GetStats();
        speed = getEnemyStats.Speed;
        getEnemyStats.SetSpeed(3f);
        phase = 1;
        originPos = transform.position;
        time = 0;
        isChangingPhase = true;
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
        if (gameObject.GetComponent<OneHandBossCombat>().isHiting == false)
        {
            //Log.log("speed " + getEnemyStats.Speed);
            //dist = Vector3.Distance(transform.position, target.transform.position);
            ChooseMovement();
            //Log.log("dist " + dist);
            Log.log("RND " +rnd);
        }
        else
        {
            direction = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
    void ChooseMovement()
    {
        ChoosePhaseTimer();
        if (phase == 0)
        {
            PlayIntroScene();
        }

        else if (phase == 1)
        {

            OnFollowPlayer();
        }

        else if (phase == 2)
        {
            OnBurstAttack();
        }
    }
    void ChoosePhaseTimer()
    {
        timeChoosePhase += Time.deltaTime;
        if (timeChoosePhase > 5)
        {
            rnd = UnityEngine.Random.Range(1, 3);
            while (rnd == phase)
            {
                rnd = UnityEngine.Random.Range(1, 3);

            }
            switch (rnd)
            {
                case 1:
                    phase = 1;
                    break;
                case 2:
                    phase = 2;
                    break;
                //case 3:
                //    phase = 2;
                //    break;
            }

            timeChoosePhase = 0;
        }
        if (phase == 1)
        {
            getEnemyStats.SetSpeed(3f);
        }
        else if (phase == 2)
        {
            getEnemyStats.SetSpeed(15f);
        }
    }
    void OnBurstAttack()
    {
        time += Time.deltaTime;
        if (time >= 0.5f)
        {
            phase = 1;
            time = 0;
        }
    }
    void OnFollowPlayer()
    {
        direction = target.transform.position - transform.position;
    }

    void PlayIntroScene()
    {
        //do it with animation
    }
}
