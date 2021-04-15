using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class LastBossInput : MonoBehaviour,IInputModule
{
    // to do
    //- make alove bullet having a random color. disable it if the background screen is the same
    //- give a color to the pillar cable, disable it if it has the same color as the background colors screen
    //- make phase turn more smoothly 


    // Events
    public event Action<int> OnColorSwitch;
    public event Action<ICharacter> OnAimLock;
    public event Action<GameObject> OnTap;


    // Variables
    public bool isFrozen { get; set; }
    public bool onMeleeAttackPosition;
    public int phase;
    public bool zoneShooted;
    public Vector3 bossZonePos;
    public List<Vector3> zoneContainer = new List<Vector3>();

    int i;
    int rnd;
    int phaseNumber;

    float timeTransitionMove;
    float timeChoosePhase;
    float tempTimer;
    float phaseDuration;
    float dist;
    float speed;

    bool isChangingPhase;
    


    Vector2 direction;
    Vector3 targetLastPos;
    Vector3 originPos;
    GameObject target;
    Stats getEnemyStats;
    LastBossCombat getBossCombat;
    PillarLastBoss getPillar;

    ICharacter character;
    Colors color;
    public void Init()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        getEnemyStats = GetComponent<Ranged1>().GetStats();
        getBossCombat = GetComponent<LastBossCombat>();
        speed = getEnemyStats.Speed;
        phase = 0;
        direction = new Vector3(2.0f, 0.0f, 0.0f);
        ChooseMovement();
        originPos = transform.position;
        isChangingPhase = true;
        phaseDuration = 10;
        character = GetComponent<ICharacter>();
        character.phase = phase;
    }

    public void Update()
    {
        //phase = 2;
        UpdateDirection();
        SetPhase();
        Log.log("phase "+phase);
    }
    void SetPhase()
    {
        character.phase = phase;
    }
    public Vector2 GetDirection()
    {
        return direction;
    }

    void UpdateDirection()
    {
        Log.log("PHASe" + phase/*+"direction "+direction)*/);
        //Log.log("rnd " + rnd);
        //Log.log("phaseNumber " + phaseNumber);
        //Log.log("speed " +speed );
        Log.log("phaseDuration " + phaseDuration);
        //OnMovementToTheMiddle();//test
        //MoveZoneAttack();

        ChooseMovement();

    }
    void ChooseMovement()
    {
        ChooseRndNumAfterDuration();
        if (phase == 0)
        {
            PlayIntroScene();
        }

        else if (phase == 1)
        {
            TransitionMovement();
        }

        else if (phase == 2)
        {
            MeleeAttack();
        }
        else if (phase == 3)
        {
            MoveZoneAttack();
        }
        else if (phase == 4)
        {
            OnMovementToTheMiddle();
            if (getBossCombat.numberOfAliveBullet >= 4)
            {
                timeChoosePhase = 200;
                ChooseRndNumAfterDuration();

            }
        }
        else if (phase == 5)//shoot pillar phase
        {
            OnMovementToTheMiddle();
            if (GetComponent<LastBossCombat>().didBossShotPillar == true)
            {
                Log.log("didishopillar " + GetComponent<LastBossCombat>().didBossShotPillar);
                if (GameObject.Find("PillarLastBossSprite(Clone)").
                    transform.GetChild(0).GetComponent<PillarLastBoss>().isAttached == true)
                {
                    timeChoosePhase = 200;

                    ChooseRndNumAfterDuration();
                }
            }
        }
        else if (phase == 6)
        {
            OnMovementToTheMiddle();
        }
    }
    void ChooseRndNumAfterDuration()
    {
        timeChoosePhase += Time.deltaTime;
        if (timeChoosePhase > phaseDuration)
        {
            rnd = UnityEngine.Random.Range(1,7);
            if (phaseNumber == rnd)
            {
                rnd = UnityEngine.Random.Range(1, 7);
            }
            else
            {
                OnChoosePhasePerRndNumber();
                timeChoosePhase = 0;
            }
        }
    }
    void OnChoosePhasePerRndNumber()
    {
        phaseNumber = rnd;
        switch (phaseNumber)
        {
            case 1:
                phase = 1;
                phaseDuration = 1;
                direction = new Vector3(2.0f, 0.0f, 0.0f);
                break;
            case 2:
                phase = 2;
                phaseDuration = 10;
                direction = target.transform.position - transform.position;
                break;
            case 3:
                phase = 3;
                phaseDuration = 200;
                break;
            case 4:
                phase = 4;
                phaseDuration = 200;
                break;
            case 5:
                phase = 5;
                phaseDuration = 200;
                break;
            case 6:
                phase = 6;
                getBossCombat.didBossShotUltimAtt = false;
                getBossCombat.didBossShot = false;
                phaseDuration = 10;
                break;
        }
    }
    
    //phase4
    void OnMovementToTheMiddle()
    {
        float dist = Vector3.Distance(transform.position,
            /*new Vector3(0.0f, -1.0f, 0.0f)*/originPos);
        //Log.log("dist " + dist);
        //Log.log("SPEED " +getEnemyStats.Speed);
        direction = (originPos/*new Vector3(0.0f, -1.0f, 0.0f)*/ 
            - transform.position )/** speed * Time.deltaTime * 10*/;
        if (dist < 2.5)
        {
            getEnemyStats.SetSpeed(0);
            //direction = new Vector3(0.0f, 0.0f, 0.0f);
            getBossCombat.isOnTheMiddle = true;
        }
        else
        {
            getEnemyStats.SetSpeed(3);
            getBossCombat.isOnTheMiddle = false;
        }
    }
    //phase3
    void MoveZoneAttack()
    {
        if (zoneShooted == true)
        {
            if (i >= zoneContainer.Count)
            {
                zoneShooted = false;
                timeChoosePhase = 200;
                ChooseRndNumAfterDuration();
                zoneContainer.Clear();

                i = 0;
            }
            else
            {
                tempTimer += Time.deltaTime;
                if (tempTimer > 0.008f)
                {
                    transform.position = zoneContainer[i] - new Vector3(1.0f, 0.0f, 0.0f);
                    bossZonePos = transform.position;
                    //getBossCombat.Shoot3();
                    Shoot3(target);

                    i++;

                    tempTimer = 0;
                }
                //Log.log("i" + i + "zone count" + zoneContainer.Count);
            }
        }
    }

    //tmp
    //shoots mellee bullet during the zone attack phase
    public  void Shoot3(GameObject target)
    {
        GameObject bulletMelee = Resources.Load<GameObject>("Prefabs/Bullets/StraightShotM");

        //foreach (Transform t in sockets)
        //{
            //Vector3 bossZonePos = owner.GetGameObject().GetComponent<BRLastBossInput>().bossZonePos;
            Vector3 direction = bossZonePos - /*t.*/transform.position;
            direction = direction.normalized;

            GameObject bulletMeleeObjVertical = MonoBehaviour.Instantiate
                    (bulletMelee,/* t.*/transform.position + direction,
                    Quaternion.identity) as GameObject;
            Bullet bullet = bulletMeleeObjVertical.GetComponent<Bullet>();

        //bullet.GetComponent<StraightShotBullet>().bulletSpeed = 0;
        bulletMeleeObjVertical.transform.localScale = new Vector3(5.0f, 5.0f, 1.0f);

        bulletMeleeObjVertical.transform.localEulerAngles =/* t.*/transform.localEulerAngles;

            bullet.Init(gameObject.GetComponent<ICharacter>(), 
                direction,0.1f,10,0.1f ,Colors.Red,target);

        //}

    }

    //phase2
    void MeleeAttack()
    {
        direction = target.transform.position - transform.position;
        CheckIfOnMeleeAttackPosition();

        if (onMeleeAttackPosition == true)
        {
            getEnemyStats.SetSpeed(0);
            transform.position = transform.position;
            //direction = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else
        {
            transform.position=target.transform.position;
            getEnemyStats.SetSpeed(0);
        }

    }
    void CheckIfOnMeleeAttackPosition()
    {
        dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < 2)
        {
            onMeleeAttackPosition = true;
        }
    }
    void TransitionMovement()
    {
        timeTransitionMove += Time.deltaTime;
        if (timeTransitionMove > 1f)
        {
            direction *= -1;
            timeTransitionMove = 0;
        }
    }



    void PlayIntroScene()
    {
        //do it with animation
    }
}
