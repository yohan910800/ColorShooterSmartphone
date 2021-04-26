using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class FireBrunchInput : MonoBehaviour, IInputModule
{
    // Events
    public event Action<int> OnColorSwitch;
    public event Action<ICharacter> OnAimLock;
    public event Action<GameObject> OnTap;
    // Variables
    public bool isFrozen { get; set; }
    public int phase;
    float dist;
    float timerBurst;
    float timerRest;
    bool pause = false;
    public bool isAttacking;
    public bool stopBurst = false;
    bool startCoroutineJustOnce = false;
    GameObject target;
    Vector2 direction;
    Vector3 targetLastPos;
    Vector2 originPos;
    ICharacter character;

    public void Init()
    {
        target = GameObject.Find("Player");
        originPos= transform.position;
        targetLastPos = target.transform.position;
        isFrozen = true;
        character = GetComponent<ICharacter>();
        phase = character.phase;
    }

    public void Update()
    {
        Log.log("phase " +phase);
        UpdatePhase();
        UpdateDirection();
    }
    void UpdatePhase()
    {
        phase = character.phase;
    }
    public Vector2 GetDirection()
    {
        return direction;

    }
    void UpdateDirection()
    {
        switch (phase)
        {
            case 0:

                break;
            case 1:
                Burst();
                break;
            case 2:
                character.GetStats().SetSpeed(3);
                GoOnOriginPosition();
                break;
            case 3:
                break;

        }

    }

    void GoOnOriginPosition()
    {
        direction = (Vector3)originPos - transform.position;
        dist = Vector2.Distance(originPos, transform.position);
        if (dist <= 0.5f)
        {
            direction = new Vector3(0.0f, 0.0f, 0.0f);
            character.phase = 3;
        }
    }
    void Burst()
    {
        if (stopBurst == false)
        {
            if (isAttacking == false)
            {
                gameObject.GetComponent<Character>().GetStats().SetSpeed(0);
                timerRest += Time.deltaTime;
                if (timerRest > 2)
                {
                    targetLastPos = target.transform.position;
                    isAttacking = true;
                    timerRest = 0;
                }
            }

            if (isAttacking == true)
            {

                direction = (targetLastPos - transform.position);
                gameObject.GetComponent<Character>().GetStats().SetSpeed(15);

                timerBurst += Time.deltaTime;

                if (timerBurst > 0.5f)
                {
                    isAttacking = false;
                    timerBurst = 0;
                }
            }
        }
        else
        {
            return;
        }
    }
}
