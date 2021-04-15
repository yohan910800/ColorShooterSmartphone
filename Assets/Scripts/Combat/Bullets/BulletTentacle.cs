using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class BulletTentacle : Bullet
{

    public bool IsAttached { get; private set; }
    public bool OnPlayerPos { get; private set; }
    public bool OnWallPosition;
    public bool Done = true;
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
    Vector3 direction;
    Vector3 playerLastPos;

    CircleCollider2D childCol;
    float c;
    int i;
    float distWall;

    bool justOnce;


    [SerializeField]
    LayerMask layerMask ;

    public override void Init(ICharacter owner, Vector3 direction, float speed, 
        int dmg, float range, Colors color, GameObject target)
    {
        childCol = transform.GetChild(0).GetComponent<CircleCollider2D>();
        Owner = owner;
        Damage = dmg;
        BulletColor = color;
        sprite = GetComponentInChildren<SpriteRenderer>();
        lr = GetComponent<LineRenderer>();
        if (Static.bulletColors.ContainsKey(color))
        {
            sprite.color = Static.bulletColors[color];

            lr.endColor = Static.bulletColors[color];
        }
        this.speed = speed;
        this.range = range;
        lifeTime = 10/*range / speed*/;
        this.direction = direction;
        //targetTrLastPos.position = targetTr.position;

        base.Init();
        playerLastPos = target.transform.position;

        //boxCol = (BoxCollider2D)coll;
        //circleCol = (CircleCollider2D)coll;
    }
    
    public void AttachAnchor(Transform anchor)
    {
        this.anchor = anchor;
    }

    void Update()
    {
        UpdatePosition();
        UpdateLine();
       
        
        time += Time.deltaTime;
        if (IsAttached==false && time >= lifeTime)
        {
            Terminate();
            //gameObject.SetActive(false);
            //return;
        }
    }

    void UpdateLine()
    {
        //GetComponent<CircleCollider2D>().offset = lr.transform.position;
        if (Owner != null)
        {
            lr.SetPosition(0, anchor.position);
            lr.SetPosition(1, transform.position);
            float distance = Vector3.Distance(anchor.position, transform.position);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, -lr.transform.position +
               Owner.GetGameObject().transform.position/*, Vector3.Distance(transform.position,
               -lr.transform.position +
               Owner.GetGameObject().transform.position), layerMask*/);

            Debug.DrawRay(transform.position, -lr.transform.position +
               Owner.GetGameObject().transform.position);


            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == 10|| hit.collider.gameObject.layer == 12)
                {
                    OnWallPosition = true;
                }
                else
                {
                    //Log.log("hit " + hit.collider.name);
                    OnHurtThePlayer(hit);
                }
            }
        }
    }
    void OnHurtThePlayer(RaycastHit2D hit)
    {
        dmgInterval += Time.deltaTime;
        if (dmgInterval > 0.3f)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                ICharacter character = hit.collider.GetComponent<ICharacter>();

                Log.log("chara " + character);

                if (character != null)
                {
                    bool isHit =    character.HitCheck(this);

                    if (isHit)
                    {
                        OnHit(character);

                        Log.log("hit " + hit.collider.name + "tentacle position " + transform.position
                        );
                    }
                }
            }
            dmgInterval = 0;
        }
        
    }

    void UpdatePosition()
    {
            float dist = Vector3.Distance(transform.position, playerLastPos);
        
        if (Mathf.Abs(dist) < 0.3f)
        {
                OnPlayerPos = true;
        }

        if (!isDissapearing)
        {
            if (OnWallPosition == true)
                {
                    transform.position = transform.position;
                }

            if (OnPlayerPos)
            {
                transform.position = playerLastPos;
            }


            else if(OnPlayerPos==false&&OnWallPosition==false)
            {
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }

    protected override void OnHit(ICharacter character)
    {
        //targetTr = character.GetGameObject().transform;
        //targetChar = character;
        //IsAttached = true;
        //time = 0;
    }

    protected override void Terminate()
    {
        //if (targetChar != null)
        //{
        //    targetChar.GetStats().ResetSpeed();
        //}
        base.Terminate();
    }
}