using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class DualBossGreenInput : MonoBehaviour,IInputModule
{
    // Events
    public event Action<int> OnColorSwitch;
    public event Action<ICharacter> OnAimLock;
    public event Action<GameObject> OnTap;

    // Variables
    public bool isFrozen { get; set; }
    public int phase;
    public bool onPosition;
    public bool isUper;
    public bool isPhase4Attacking;

    float time;
    float timeRunbetweenAttack;
    float timeAttackPhase2;
    float timePhase0;
    float timeBeforeTeleportAttack;
    float timeBeforeTeleport;
    float phaseDuration;
    float dist;
    float posY;
    float targetPosY;
    float speed;

    bool isAttacking;
    bool isChangingPhase;

    Vector2 direction;
    Vector3 targetLastPos;
    Vector3 originPos;
    GameObject target;

    public void Init()
    {
        target = GameObject.Find("Player");
        speed = GetComponent<Ranged1>().GetStats().Speed;
        phase = 0;
        originPos = transform.position;
        time = 0;
        phaseDuration = 10;
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
        dist = Vector3.Distance(transform.position, target.transform.position);
        ChooseMovement();
    }

    void ChooseMovement()
    {
        CheckDistanceToChangeMovement();
        if (phase == 0)
        {
            PlayIntroScene();
        }

        else if (phase == 1)
        {
            OnRunAwayFromPlayer();
        }

        else if (phase == 2)
        {
            timeRunbetweenAttack += Time.deltaTime;
            if (timeRunbetweenAttack > 6)
            {
                OnAttack();
            }
            else
            {
                OnRunBetweenAttack();
            }
        }
    }
    void OnAttack()
    {
        direction = Vector3.zero;
        timeAttackPhase2 += Time.deltaTime;
        if (timeAttackPhase2 > 6)
        {
            isChangingPhase = true;

            timeRunbetweenAttack = 0;
            timeAttackPhase2 = 0;
        }
    }
    void OnRunAwayFromPlayer()
    {
        if (dist < 6)
        {
            direction = -(target.transform.position - transform.position)
                * Time.deltaTime * speed * 3;
        }
        else
        {
            phase = 2;
        }
    }
    void OnRunBetweenAttack()
    {
        time += Time.deltaTime;
        if (time >= 2)
        {
            direction *= -1;
            time = 0;
        }
    }

    void CheckDistanceToChangeMovement()
    {
        if (dist >= 7)
        {
            if (isChangingPhase == true)
            {
                direction = new Vector2(2, 0);
                phase = 2;
                isChangingPhase = false;
            }
        }
        else
        {
            phase = 1;
        }
    }

    void PlayIntroScene()
    {
        //do it with animation
    }
}
