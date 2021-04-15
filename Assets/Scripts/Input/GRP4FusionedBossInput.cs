using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System;

public class GRP4FusionedBossInput : MonoBehaviour, IInputModule
{
    // Events
    public event Action<int> OnColorSwitch;
    public event Action<ICharacter> OnAimLock;
    public event Action<GameObject> OnTap;



    // Variables
    public bool isFrozen { get; set; }
    public bool isUper;

    float time;
    float timePhase1;
    float timePhase2;
    float timePhase0;
    float timeBeforeTeleportAttack;
    float timeBeforeTeleport;
    float phaseDuration;
    float dist;
    float posY;
    float targetPosY;

    int phase;

    bool isAttacking;
    bool isDifferentThanPrevious;
    bool isItAllowToAttack;

    Vector2 direction;
    Vector3 targetLastPos;
    Vector3 originPos;
    GameObject target;
    ICharacter character;
    
    public void Init()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        character = GetComponent<ICharacter>();
        character.phase=phase;
        phase = 1;
        
        float startDir = 2;
        direction = new Vector2(startDir, 0);
        originPos = transform.position;
        time = 1;
        phaseDuration = 10;
    }
    
    void SetPhase()
    {
        character.phase = phase;
    }

    public void Update()
    {
        //phase = 2;
        SetPhase(); 
        UpdateDirection();
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    void UpdateDirection()
    {
        Log.log("isDifferentThanPrevious  " + isDifferentThanPrevious);
        ChooseMovement();
    }
    void ChooseMovement()
    {
        TimerBeforeChangePhase();
        if (phase == 0)
        {
            PlayIntroScene();
        }

        else if (phase == 1)//Basic movement phase
        {
            
            TransitionMovement();
        }

        else if (phase == 2)
        {
            OnPositionBeforeAttack();
            if (character.isItAllowToAttack == true)
            {
                //isFrozen =true;

                //Log.log("Ready to attack");
            }
        }
        else if (phase == 3)
        {
            time += Time.deltaTime;
            if (time > 2)
            {
                direction *= -1;
                time = 0;
            }
        }
        else if (phase == 4)
        {
            timeBeforeTeleport += Time.deltaTime;
            if (timeBeforeTeleport > 1)
            {
                OnTeleport();
                //reset in WaitBeforeAttack()
            }
        }
    }
    void OnTeleport()
    {
        if (isFrozen == false)
        {
            transform.position = new Vector3
                (target.transform.position.x - 1, target.transform.position.y - 1);
            isFrozen = true;
        }
        else if (isFrozen == true)
        {
            WaitBeforeAttack();
        }
    }
    void WaitBeforeAttack()
    {
        direction = new Vector2(0, 0);
        timeBeforeTeleportAttack += Time.deltaTime;

        if (timeBeforeTeleportAttack >= 1.0f)
        {
            character.isItAllowToAttack = true;

            isFrozen = false;
            timeBeforeTeleportAttack = 0.0f;

            timeBeforeTeleport = 0.0f;
        }
    }
    void TimerBeforeChangePhase()
    {
        timePhase1 += Time.deltaTime;
        if (timePhase1 >= phaseDuration)
        {
            int rndPhase = UnityEngine.Random.Range(2, 5);
            character.isItAllowToAttack = false;//reset allow to attack
            if (rndPhase != phase)
            {
                phase = rndPhase;
                isDifferentThanPrevious = true;
            }
            else
            {
                 rndPhase = UnityEngine.Random.Range(2, 5);
                isDifferentThanPrevious = false;
            }
            if (isDifferentThanPrevious == true)
            {
                phase = rndPhase;
                if (phase == 3)
                {
                    phaseDuration = 5;
                    direction = new Vector2(10, 0);
                }
                else if (phase == 4)
                {
                    phaseDuration = 20f;
                }
                else if (phase == 2)
                {
                    phaseDuration = 18;

                }
            }
            timePhase1 = 0;
        }
        //if (phase == 1)//temp
        //{
        //    timePhase2 += Time.deltaTime;
        //    if (timePhase2 >= 100)
        //    {
        //        //isFrozen = true;
        //        phase = 2;//temp
        //        //timePhase2 = 0;
        //    }
        //}
        //if (phase == 2)
        //{
        //    OnPositionBeforeAttack();

        //}
        
    }

    void OnPositionBeforeAttack()
    {
        //if (onPosition == false)
        //{
            CheckIfOnposition();
        direction = (transform.position - originPos) * -1;
        //}
    }

    void CheckIfOnposition()
    {
        dist = Vector3.Distance(transform.position, originPos);

        if (Mathf.Abs(dist) <= 2.0f)
        {
            character.GetStats().SetSpeed(0);
            character.isItAllowToAttack = true;
        }
        else
        {
            character.GetStats().SetSpeed(5);

            character.isItAllowToAttack = false;
        }
    }
    void TransitionMovement()
    {
        time += Time.deltaTime;
        if (time >= 1)
        {
            time = 0;
            direction *= -1;
        }
    }
    void PlayIntroScene()
    {
        //do it with animation
    }

}
