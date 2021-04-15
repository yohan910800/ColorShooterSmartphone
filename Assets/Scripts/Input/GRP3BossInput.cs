using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System;

public class GRP3BossInput : MonoBehaviour,IInputModule
{
    // Events
    
    public event Action<int> OnColorSwitch;
    public event Action<ICharacter> OnAimLock;
    public event Action<GameObject> OnTap;


    // Variables
    //public bool isFrozen { get; set; }
    public  int phase;
    public bool onPosition;
    public bool isUper;

    float time;
    float timePhase3;
    float timePhase0;
    float timePhase1;
    float dist;
    float posY;
    float targetPosY;

    bool isAttacking=true;
    bool onTheRightSide;
    bool changeDirectionJustOnce=true;
    public Transform limitRight;
    public Transform limitLeft;

    public Vector2 direction;
    public Vector3 targetLastPos;
    public Vector3 originPos;
    public GameObject target;
    private IEnumerator coroutine;
    ICharacter character;

    Ranged1 getRanged1;

    public void Init()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        getRanged1 = GetComponent<Ranged1>();
        //isFrozen = true;
        
        originPos = transform.position;

        coroutine = WaitAndAttack(0.5f, 1f);
        StartCoroutine(coroutine);

        character = GetComponent<ICharacter>();
        character.phase = phase;
        phase = 0;
        limitLeft = GameObject.Find("limitLeftSide").transform;
        limitRight = GameObject.Find("limitRightSide").transform;
        direction = limitLeft.position- originPos ;
        character.GetStats().SetSpeed(10);
        
    }
    void SetPhase()
    {
        character.phase = phase;
    }
    public void Update()
    {
        UpdateDirection();
        SetPhase();
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    void UpdateDirection()
    {
        //Log.log("tr x " +transform.position.x+"  "+ phase);

        CheckIFUper();

        if (phase == 1)
        {

            GoOnPositionBeforeAttackIfNotOnPos();
            StartBeforeTimerPhase0();
        }

        else if (phase == 0 /*&& onPosition == true*/)
        {
            character.GetStats().SetSpeed(10);
            MoveRightToLeft();
            StartBeforeTimerPhase3();
        }

        else if(phase==3/*&&onPosition==true*/)
        {
            character.GetStats().SetSpeed(15);
            BurstAttack();
            StartBeforeTimerPhase1();
        }
    }
    public bool CheckIFUper()
    {
        posY = transform.position.y;
        targetPosY = target.transform.position.y;
        if (targetPosY > posY)
        {
            isUper = true;
        }
        else
        {
            isUper = false;
        }
        return isUper;
    }
    
    void GoOnPositionBeforeAttackIfNotOnPos()
    {
            if (onPosition == false)
            {
                direction = (transform.position-originPos)*-1 ;
                CheckIfOnposition();
            }
    }

    void CheckIfOnposition()
    {
        dist = Vector3.Distance(transform.position, originPos);

        if (dist <= 2.0f)
        {
            onPosition = true;
            character.GetStats().SetSpeed(0);
        }
        else
        {
            character.GetStats().SetSpeed(10);
        }
    }

    void BurstAttack()
    {
        if (isAttacking == true)
        {
            direction = (targetLastPos - transform.position) ;
        }
        else if (isAttacking == false)
        {
            character.GetStats().SetSpeed(0);
        }
    }

    void MoveRightToLeft()
    {
        //if (transform.position.x < limitRight.position.x)
        //{
        //        direction = transform.position-limitLeft.position;
        //}

        if (Mathf.Abs( transform.position.x )> Mathf.Abs(limitLeft.position.x))
        {
                direction *=-1 /*transform.position-limitLeft.position*/;
        }
    }

    void StartBeforeTimerPhase3()
    {
        timePhase3 += Time.deltaTime;
        if (timePhase3 >= 10)
        {
            phase=3;//temp
            timePhase3 = 0;
        }
    }
    void StartBeforeTimerPhase1()
    {
        timePhase1 += Time.deltaTime;
        if (timePhase1 >= 10)
        {
            phase = 1;//temp
            timePhase1 = 0;
        }
    }

    void StartBeforeTimerPhase0()
    {
        timePhase0 += Time.deltaTime;
        if (timePhase0 >= 5)
        {
            direction = limitLeft.position - originPos;
            phase = 0;//temp
            timePhase0 = 0;
        }
    }

    private IEnumerator WaitAndAttack(float attackTime, float restTime)//0.5
    {
        while (true)
        {
            yield return new WaitForSeconds(attackTime);
            isAttacking = false;
            yield return new WaitForSeconds(restTime);
            targetLastPos = target.transform.position;
            isAttacking = true;
        }
    }
}
