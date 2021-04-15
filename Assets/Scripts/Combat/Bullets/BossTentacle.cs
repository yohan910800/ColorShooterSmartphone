using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class BossTentacle : Bullet
{

    public bool IsAttached { get; private set; }
    public bool OnPlayerPos { get; private set; }
    public GameObject[] directionPoints = new GameObject[4];

    Transform anchor;
    LineRenderer lr;
    Transform targetTr;
    ICharacter targetChar;
    BoxCollider2D boxCol;
    CircleCollider2D circleCol;
    List<CircleCollider2D> circleColArr = new List<CircleCollider2D>();

    float dmgInterval;
    float time;
    float lifeTime;
    float speed;
    float range;
    //Vector3 direction;
    Vector3 playerLastPos;


    Vector3 direction;
    Vector3[] multipleTarget = new Vector3[4];
    Vector3[] velocity = new Vector3[4];
    float[] dist = new float[4];
    GameObject turnObject;
    Vector3 originPos;

    float c;
    int num;
    public override void Init(ICharacter owner, Vector3 direction, float speed,
        int dmg, float range, Colors color, GameObject target)
    {

        directionPoints[0] = GameObject.Find("Up");
        directionPoints[1] = GameObject.Find("Down");
        directionPoints[2] = GameObject.Find("Right");
        directionPoints[3] = GameObject.Find("Left");
        Owner = owner;
        Damage = dmg;
        BulletColor = color;
        this.speed = speed;
        this.range = range;

       
        num = Owner.GetGameObject().GetComponent<BlackBossCombat>().GetShotCount() ;
        if (num >= 4)
        {
            num = 3;
        }
        direction = directionPoints[num].transform.position;
        this.direction = direction;
        Log.log("direciton " +direction);
        
        sprite = GetComponentInChildren<SpriteRenderer>();
        lr = GetComponent<LineRenderer>();
        if (Static.bulletColors.ContainsKey(color))
        {
            sprite.color = Static.bulletColors[color];
            lr.endColor = Static.bulletColors[color];
        }
        
        lifeTime = 10/*range / speed*/;
        //this.direction = direction;
        //targetTrLastPos.position = targetTr.position;
        //direction = direction
        //    [Owner.GetGameObject().GetComponent<BlackBossCombat>().GetShotCount() - 1];


        



        base.Init();
        playerLastPos = target.transform.position;


        turnObject = GameObject.Find("TurnObject");
        transform.parent = turnObject.transform;
        multipleTarget[0] = directionPoints[0].transform.position;//up
        multipleTarget[1] = directionPoints[1].transform.position;//down
        multipleTarget[2] = directionPoints[2].transform.position;//right
        multipleTarget[3] = directionPoints[3].transform.position;//left

        originPos = transform.position;

        //direction[0] = multipleTarget[0] - originPos;
        //direction[1] = multipleTarget[1] - originPos;
        //direction[2] = multipleTarget[2] - originPos;
        //direction[3] = multipleTarget[3] - originPos;
        //boxCol = (BoxCollider2D)coll;
        //circleCol = (CircleCollider2D)coll;
    }
    private void Start()
    {
        //num = Owner.GetGameObject().GetComponent<BlackBossCombat>().GetShotCount() - 1;
        //turnObject = GameObject.Find("TurnObject");
        //transform.parent = turnObject.transform;
        //multipleTarget[0] = directionPoints[0].transform.position;//up
        //multipleTarget[1] = directionPoints[1].transform.position;//down
        //multipleTarget[2] = directionPoints[2].transform.position;//right
        //multipleTarget[3] = directionPoints[3].transform.position;//left

        //originPos = transform.position;

        //direction[0] = multipleTarget[0] - originPos;
        //direction[1] = multipleTarget[1] - originPos;
        //direction[2] = multipleTarget[2] - originPos;
        //direction[3] = multipleTarget[3] - originPos;
        
        
        //for (int j = 0; j < multipleTarget.Length; j++)
        //{
        //    direction[j] = direction[j].normalized;
        //    velocity[j] = direction[j] * speed;
        //}
    }
    public void AttachAnchor(Transform anchor)
    {
        this.anchor = anchor;
    }

    void Update()
    {

        //time += Time.deltaTime;
        //if (!IsAttached && time >= lifeTime)
        //{
        //    Terminate();
        //    return;
        //}
        if (Owner == null)
        {
            Terminate();
        }
        if (Owner != null)
        {
            UpdatePosition();
            UpdateLine();
        }

    }

    void UpdateLine()
    {
        //GetComponent<CircleCollider2D>().offset = lr.transform.position;
        lr.SetPosition(0, anchor.position);
        lr.SetPosition(1, transform.position);
        float distance = Vector3.Distance(anchor.position, transform.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -lr.transform.position +
           Owner.GetGameObject().transform.position);

        Debug.DrawRay(transform.position, -lr.transform.position +
           Owner.GetGameObject().transform.position);

        dmgInterval += Time.deltaTime;
        if (dmgInterval > 0.2f)
        {

            //collison doesn't work if it is colliding with another object in the same time
            //if (hit.collider != null)
            //{
            ICharacter character = hit.collider.GetComponent<ICharacter>();
                if (character != null)

                {
                    bool isHit = character.HitCheck(this);
                    if (isHit)
                    {
                        OnHit(character);
                        
                    }
                }
                //else
                //{
                //    OnHit();
                //}
                if (hit.collider.name == "Player")
                {
                    Debug.Log(hit.transform.name + " was hit");
                }
            //}
            dmgInterval = 0;
        }

    }

    void UpdatePosition()
    {


        Log.log("Shot count " + num);

        float dist =
      Vector3.Distance(direction/*[num]*/,transform.position
      );
        //Log.log("DIst" +
        //            dist
        //    );
        //Log.log("dist " + dist);
        if (/*Mathf.Abs(dist) <0.5f*/Mathf.Abs(transform.position.y)>10|| 
            Mathf.Abs(transform.position.x) > 10)
        {
            OnPlayerPos = true;
        }

        if (!isDissapearing)
        {
            if (OnPlayerPos==true)
        {
                if (Owner.GetGameObject().
                    GetComponent<BlackBossCombat>().isTurning == false)
                {
                    transform.position =
                    direction/*[num]*/;
                }
                else
                {
                    return;
                }
        }

        else
        {
            transform.position +=
        direction/*[num]*/
            /**speed */* Time.deltaTime;
        }
    }

}

    protected override void OnHit(ICharacter character)
    {
        targetTr = character.GetGameObject().transform;
        targetChar = character;
        IsAttached = true;
        time = 0;
    }

    protected override void Terminate()
    {
        if (targetChar != null)
        {
            targetChar.GetStats().ResetSpeed();
        }
        base.Terminate();
    }
}