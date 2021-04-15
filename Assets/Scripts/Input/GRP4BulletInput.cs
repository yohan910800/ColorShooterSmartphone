using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System;
public class GRP4BulletInput : MonoBehaviour,IInputModule
{
    // Events
    public event Action<int> OnColorSwitch;
    public event Action<ICharacter> OnAimLock;
    public event Action<GameObject> OnTap;

    // Variables
    Vector2 direction;
    Vector3 directionTarget;
    Vector3 directionTargetLastPos;
    Vector3 enemyLastPos;
    float time;
    public bool isFrozen { get; set; }
    float turnSpeed=5;
    GameObject enemy;
    float dist;
    float dist2;
    public void Init()
    {
        directionTarget = GameObject.FindGameObjectWithTag("Player").transform.position;

        enemy = GameObject.FindGameObjectWithTag("Boss");
        if (enemy == null)
        {
            enemy = GameObject.Find("LastBoss(Clone)");//tmp

        }
        enemyLastPos = enemy.transform.position;
        directionTargetLastPos = directionTarget;
        time = 1;
    }

    public void Update()
    {
        UpdateDirection();
        StopIFOnPosition();
    }

    void StopIFOnPosition()
    {
        dist = Vector3.Distance(transform.position, enemyLastPos);
        if (Mathf.Abs(dist) <= 10)
        {
            direction = directionTarget-transform.position  ;
            /*transform.position += directionTargetLastPos * Time.deltaTime * 2 * 2*/;
            dist2 = Vector3.Distance(transform.position, directionTargetLastPos);
            if (Mathf.Abs(dist2) <= 0.8f)
            {
                transform.position = directionTargetLastPos;
            }
        }
        else
        {
            //direction = new Vector3(0.0f, 0.0f, 0.0f);
            //transform.position = directionTargetLastPos;
            //transform.position -= directionTargetLastPos * Time.deltaTime* 2/2;
            dist2 = Vector3.Distance(transform.position, directionTargetLastPos);
            if (Mathf.Abs(dist2) <= 0.8f)
            {
                transform.position = directionTargetLastPos;
            }
        }
    }

    public Vector2 GetDirection()
    {
        return direction;
    }
    

    void UpdateDirection()
    {

        // Invert direction every 2 seconds
        //time += Time.deltaTime;
        //if (time >= 3)
        //{
        //    time = 0;
        //    direction =new Vector2(0,0);
        //}
        //transform.localRotation += new Vector3(Time.deltaTime*2,0,0);
        transform.Rotate(0, 0, 20 * Time.deltaTime * turnSpeed);

    }
}