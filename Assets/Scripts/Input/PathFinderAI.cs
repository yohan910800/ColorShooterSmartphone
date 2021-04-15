using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using Pathfinding;
/*
	Moves left and right relative to his aiming direction
*/
public class PathFinderAI : MonoBehaviour,
IInputModule
{
    // Events
    public event Action<int> OnColorSwitch;
    public event Action<ICharacter> OnAimLock;
    public event Action<GameObject> OnTap;

    // Variables
    Vector2 direction;
    float time;
    bool inverse;
    ICombat combat;
    public GameObject Player;
    float nextWapointDistance = 3f;

    Transform target;
    Transform t;

    Vector3 TargetPosition;
    Player playerscript;

    Path path;
    int currentWaypoint = 0;
    bool PathComplete;

    bool justOnce = true;
    GameManager gm;
    Seeker seeker;
    public void Init()
    {
        combat = GetComponent<ICombat>();
        seeker = GetComponent<Seeker>();
        direction = Vector2.zero;

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Player = GameObject.Find("Player");

        //playerscript = Player.GetComponent<Player>();

        //Debug.Log(Player.transform.position);
        //Debug.Log(transform.position);
        //Debug.Log("StartPathFinding");
        InvokeRepeating("UpdatePath", 0f, 0.5f);

        //Debug.Log("PathFindingStarted");
    }

    public void EnemyScanPath()
    {
        AstarPath.active.Scan();

    }

    public void Update()
    {
        //if (gm.WorldColor == Colors.Orange)
        //{
        //    if (justOnce == true)
        //    {
        //        AstarPath.active.Scan();
        //        Log.log("updated");
        //        justOnce = false;
        //    }
        //}
        if (Static.GamePaused)
            return;
        UpdateDirection();
    }

    void OnPathComplete(Path p)
    {

        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            //Debug.Log("Process Complete");
        }

    }
    public Vector2 GetDirection()
    {
        return direction;
    }

    void UpdatePath()
    {
        if (seeker.IsDone()&&Player!=null)
        {
            seeker.StartPath(transform.position, Player.transform.position, OnPathComplete);
        }

    }
    void UpdateDirection()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            PathComplete = true;
            return;
        }
        else
        {
            PathComplete = false;
        }

        direction = (path.vectorPath[1] - path.vectorPath[0]).normalized;

        float distance = Vector2.Distance(
            transform.position,
            path.vectorPath[currentWaypoint]
        );

        if (distance < nextWapointDistance)
        {
            currentWaypoint++;
        }
        //Debug.Log("direction=" + direction);
        //Debug.Log("currentwaypoint=" + currentWaypoint);




    }
}