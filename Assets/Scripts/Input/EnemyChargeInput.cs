using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class EnemyChargeInput : MonoBehaviour, IInputModule
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
   public  bool isAttacking;
    public bool stopBurst = false;
    bool startCoroutineJustOnce = false;
    GameObject target;
    Vector2 direction;
    Vector3 targetLastPos;
    
    [SerializeField]
    LayerMask layerMask;

    public void Init()
    {
        target = GameObject.Find("Player");
        targetLastPos = target.transform.position;
        //direction = Vector2.zero;
        isFrozen = true;
        phase = 1;
    }

    public void Update() {
        UpdateDirection();
            }

    public Vector2 GetDirection()
    {
        return direction;

    }
    void UpdateDirection()
    {
        //CheckIfThereIsWallFrontOf();
        Burst();
        FollowPlayer();
    }

    void FollowPlayer()
    {

        //phase = 1;
        dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist <= 20)
        {
            phase = 2;
        }
        else
        {
            phase = 1;
        }

        if (phase == 1)
        {
            gameObject.GetComponent<Character>().GetStats().SetSpeed(3);
            //gameObject.GetComponent<Character>().GetStats().SetBaseSpeed(3);
            direction = target.transform.position - transform.position;

        }
    }

    void Burst()
    {
        if (phase == 2)
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
                    direction = targetLastPos - transform.position;
                    gameObject.GetComponent<Character>().GetStats().SetSpeed(15);
                    //gameObject.GetComponent<Character>().GetStats().SetBaseSpeed(15);

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

    void CheckIfThereIsWallFrontOf()
    {
        Vector2 origin = transform.position; // starting point of raycast
        Vector2 direction = /*transform.up*/ (target.transform.position-transform.position)/3; // direction to raycast from origin

        RaycastHit2D hit = Physics2D.Raycast(origin,
            direction, Vector3.Distance(origin,direction),layerMask);
        Debug.DrawRay(origin, direction,Color.green);
       
        if (hit.collider != null)
        {
            
            stopBurst = true;
        }
        else
        {
            stopBurst = false;
        }
    }
}
