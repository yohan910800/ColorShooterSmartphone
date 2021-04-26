using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System;

public class OneHandRoundMeleeWeaponEnemyInput : MonoBehaviour, IInputModule
{
    // Events
    public event Action<int> OnColorSwitch;
    public event Action<ICharacter> OnAimLock;
    public event Action<GameObject> OnTap;


    // Variables
    public int phase;
    public bool onPosition;
    public LayerMask layerMask;

    int rnd;
    float time;
    float timeChoosePhase;

    float phaseDuration;
    float dist;
    float speed;

    bool isChangingPhase;
    bool isAWallIsFrontOf = false;

    Vector2 direction;
    Vector3 targetLastPos;
    Vector3 originPos;
    GameObject target;
    Stats getEnemyStats;

    public void Init()
    {
        target = GameObject.Find("Player");
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
        CheckIfThereIsAWallInFront();
        ChooseMovement();
    }
    void CheckIfThereIsAWallInFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,
            (target.transform.position-transform.position)/2, Vector3.Distance(transform.position,
            (target.transform.position - transform.position) / 2), layerMask);
        Debug.DrawRay(transform.position, (target.transform.position - transform.position)/2);
        if (hit.collider != null)
        {
            if (hit.collider.transform.gameObject.layer == 10)
            {
                getEnemyStats.SetSpeed(0.0f);
                isAWallIsFrontOf = true;
            }
            else
            {
                isAWallIsFrontOf = false;
                getEnemyStats.SetSpeed(1.5f);
            }
        }
        else
        {
            isAWallIsFrontOf = false;
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
            }

            timeChoosePhase = 0;
        }
        if (phase == 1)
        {
            if (isAWallIsFrontOf == false)
            {
                getEnemyStats.SetSpeed(1.5f);
            }
        }
        else if (phase == 2)
        {
            if (isAWallIsFrontOf == false)
            {
                getEnemyStats.SetSpeed(15f);
            }
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
    }
}
