using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class FourHandsBossInput : MonoBehaviour,IInputModule
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
    float speed;

    bool isChangingPhase;


    Vector2 direction;
    Vector3 targetLastPos;
    Vector3 originPos;
    GameObject target;
    Stats getEnemyStats;
    ICharacter character;
    public void Init()
    {
        target = GameObject.Find("Player");
        getEnemyStats = GetComponent<Ranged1>().GetStats();
        speed = getEnemyStats.Speed;
        character = GetComponent<ICharacter>();
        
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
        ChooseMovement();

    }
    void ChooseMovement()
    {
        ChoosePhaseTimer();
        if (character.phase == 0)
        {
            PlayIntroScene();
        }

        else if (character.phase == 1)
        {

            OnFollowPlayer();
        }

        else if (character.phase == 2)
        {
            OnBurstAttack();
        }
    }
    void ChoosePhaseTimer()
    {
        timeChoosePhase += Time.deltaTime;
        if (timeChoosePhase > 5)
        {
            rnd = UnityEngine.Random.Range(1, 4);

            switch (rnd)
            {
                case 1:
                    character.phase = 1;
                    break;
                case 2:
                    character.phase = 1;
                    break;
                case 3:
                    character.phase = 2;
                    break;
            }

            timeChoosePhase = 0;
        }
        if (character.phase == 1)
        {
            getEnemyStats.SetSpeed(2);
        }
        else if (character.phase == 2)
        {
            getEnemyStats.SetSpeed(15);
        }
    }
    void OnBurstAttack()
    {
        time += Time.deltaTime;
        if (time >= 0.5f)
        {
            character.phase = 1;
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
