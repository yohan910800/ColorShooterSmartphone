using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Stage1MapManager : MapManager
{

    public GameObject invisibleWallTop;// get an invisible wall on the top of the trigger zone.
    public GameObject invisibleWallBottom; //get an invisible wall on the bottom of the trigger zone
    public GameObject[] tutorialSigns;
    public GameObject[] phaseWalls;
    public GameObject stopTimeMeleeModeUIElement;
    public GameObject bossNameUI;
    
    public GameObject miniStageExit;
    public GameObject[] miniStageExitGroup;
    public List<GameObject> enemyInTheArea = new List<GameObject>();

    public GameObject permanentWall;//get an invisible wall permanent wall wich prevent the player from going back to the village
    //public int enemyDeadCount;//this variable count the number of enemy dead in the map 

    GameObject tutorialUI;
    GameObject fingerColor;
    GameObject fingerInventory;
    GameObject killEnemyText;
    GameObject blueCyanExplanetionText;
    GameObject redGreyExplanetionText;

    int rndSpeed;
    int rndHP;
    int rndScale;
    int rndAtt;
    int rnd;
    int att = 1;
    int dropedEnergy;
    int dropedCredits;
    float meleeAttackEnemyScale = 1;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();


        //if (File.Exists(Application.dataPath + "/savedata/autoSaveData.test"))
        //{
        //    phase = SaveSystem.LoadAutoSaveData().phase;

        //}
        //else phase = 0;
        //if (phase < 0) phase = 0;


        FindObjectOfType<AudioManager>().Play("MainBgm");
        phase = 0;
        OnLoadPrefab(0, "MeleeAttackStage1");
        OnLoadPrefab(1, "Red/Ranged1_R_RGO");
        OnLoadPrefab(2, "OneHandRoundMeleeWeaponEnemy");
        OnLoadPrefab(3, "Red/Ranged1_R_RGOMultiColor");
        OnLoadPrefab(4, "MuscularMeleeAttack"); 
        OnLoadPrefab(5, "Blue/FourHandsBoss");
        bossNameUI = GameObject.Find("TransitionAnimationUI").transform.Find("BossName").gameObject;
        //OnLoadPrefab(3, "Tutorial/FireBrunch");

        dropedEnergy = 5;
        dropedCredits = 10;

        //int phaseoffset;
        //if (File.Exists(Application.dataPath + "/savedata/autoSaveData.test"))
        //{
        //    phaseoffset = SaveSystem.LoadAutoSaveData().phase - 1;
        //    MoveTriggerZoneToTheNextPosition(phaseoffset
        //      );

        //}

        //change hair sprite of the player according to wich stage he is playing here

        miniStageExit = GameObject.Find("MiniStageExit");
        
    }
    
    void Update()
    {
        //Log.log("phase " +phase);
        //Log.log("enemyarraycount "+ enemyArray.Count);
        //Log.log("dead count  " + enemyDeadCount);
        if (enemyArray.Count == enemyDeadCount)
        {
            if (phase != 0)
            {
                miniStageExit.transform.position = 
                    miniStageExitGroup[phase - 1].transform.position;
                //phaseWalls[phase - 1].SetActive(false);
            }
        }
    }
    public override void ActivatePhase(int phase)//check in wich part of the tutorial we are 
    {
        switch (phase)
        {
            default:
                Debug.Log("Phase Does not exist");
                break;

            case 0://Area1


                for (int i = 0; i < 2; i++)
                {
                    SpawnEnemyStage1(0, spawnerPoints[0].position + new Vector3(3.0f * i, 0.0f, 0.0f));
                    SpawnEnemyStage1(0, spawnerPoints[1].position + new Vector3(3.0f * i, 0.0f, 0.0f));
                    SpawnEnemyStage1(0, spawnerPoints[2].position + new Vector3(3.0f * i, 0.0f, 0.0f));
                }

                MoveTriggerZoneToTheNextPosition(0);
                break;
            case 1://Area2

                for (int i = 0; i < 2; i++)
                {
                    SpawnEnemyRoundOnHand(2, spawnerPoints[4].position + 
                        new Vector3(2.0f * i, 0.0f, 0.0f));
                    SpawnEnemyDistanceShoot(1, spawnerPoints[4].position +
                        new Vector3(2.0f * i, 0.0f, 0.0f));
                }
                for (int i = 0; i < 4; i++)
                {
                    SpawnEnemyDistanceShoot(1, spawnerPoints[4].position +
                        new Vector3(2.0f * i, 0.0f, 0.0f));
                }

                
                SpawnEnemyStage1(0, spawnerPoints[3].position +
                    new Vector3(2.0f, 0.0f, 0.0f));


                MoveTriggerZoneToTheNextPosition(1);
                break;
            case 2://Area3

                for (int i = 0; i < 2; i++)
                {
                    SpawnEnemyStage1(2, spawnerPoints[5].position +
                    new Vector3(2.0f, 0.0f, 0.0f));
                }
                for (int i = 0; i < 8; i++)
                {
                    SpawnEnemyStage1(0, spawnerPoints[5].position +
                    new Vector3(2.0f, 0.0f, 0.0f));
                }

                MoveTriggerZoneToTheNextPosition(2);
                break;

            case 3://Area4
                for (int i = 0; i < 7; i++)
                {

                    SpawnEnemyDistanceMultiColorShoot(3, spawnerPoints[6].position +
                    new Vector3(2.0f, 0.0f, 0.0f));
                    SpawnEnemyStage1(0, spawnerPoints[7].position +
                    new Vector3(2.0f, 0.0f, 0.0f));
                    SpawnEnemyDistanceMultiColorShoot(3, spawnerPoints[8].position +
                    new Vector3(2.0f, 0.0f, 0.0f));

                }

                    //for (int i = 0; i < 2; i++)
                    //{
                    //    SpawnEnemyDistanceShoot(1, spawnerPoints[4].position);
                    //    SpawnEnemyDistanceShoot(1, spawnerPoints[5].position);
                    //}
                    MoveTriggerZoneToTheNextPosition(3);
                break;
            case 4://Area5
                for (int i = 0; i < 2; i++)
                {
                    SpawnEnemyStage1HightHP(1, spawnerPoints[9].position);
                    SpawnEnemyStage1HightHP(2, spawnerPoints[10].position);
                    SpawnEnemyStage1HightHP(1, spawnerPoints[11].position);
                }
                MoveTriggerZoneToTheNextPosition(4);
                break;

            case 5://Area6
                for (int i = 0; i < 7; i++)
                {
                    SpawnEnemyStage1HightHP(2, spawnerPoints[12].position +
                        new Vector3(1.0f * i, 0.0f, 0.0f));
                }
                
                MoveTriggerZoneToTheNextPosition(5);
                break;

            case 6://Area7

                for (int i = 0; i < 4; i++)
                {
                    //SpawnEnemyTutorialHightHP(0, spawnerPoints[9].position);
                    SpawnEnemyStage1(0, spawnerPoints[13].position);
                   
                }
                for (int i = 0; i < 5; i++)
                {
                    SpawnEnemyStage1HightHP(3, spawnerPoints[14].position
                        +new Vector3(3.0f * i, 0.0f, 0.0f)) ;
                }
                MoveTriggerZoneToTheNextPosition(6);
                break;

            case 7://Area8

                //for (int i = 0; i < 2; i++)
                //{
                    SpawnMuscularEnemy(4, spawnerPoints[15].position + new Vector3(2.0f , 0.0f, 0.0f));
                    //SpawnEnemyStage1HightHP(2, spawnerPoints[16].position + new Vector3(2.0f * i, 0.0f, 0.0f));
                //}

                MoveTriggerZoneToTheNextPosition(7);
                break;
            case 8://Area9
                for (int i = 0; i < 3; i++)
                {
                    SpawnMuscularEnemy(4, spawnerPoints[17].position + new Vector3(2.0f * i, 0.0f, 0.0f));
                }
                MoveTriggerZoneToTheNextPosition(8);
                break;
            case 9://Area10
                FindObjectOfType<AudioManager>().Pause("MainBgm");
                FindObjectOfType<AudioManager>().Play("BossBgm");
                SpawnStage1Boss(5, spawnerPoints[18].position);

                StartCoroutine(PresentBoss(5.0f));
                triggerZone.SetActive(false);
                StartCoroutine(CheckIfGameIsClear(20.0f));//start infinit loop

                MoveTriggerZoneToTheNextPosition(9);
                break;
            
            //case 10://Area11

            //    for (int i = 0; i < 6; i++)
            //    {
            //        //SpawnEnemyTutorialHightHP(0, spawnerPoints[15].position + new Vector3(i, 0.0f, 0.0f));
            //    }
            //    for (int i = 0; i < 3; i++)
            //    {
            //        SpawnEnemyDistanceShoot(1, spawnerPoints[16].position);
            //        SpawnEnemyDistanceShoot(1, spawnerPoints[17].position);
            //    }

            //    MoveTriggerZoneToTheNextPosition(10);
            //    break;

            //case 11://Area12

            //    for (int i = 0; i < 6; i++)
            //    {
            //        //SpawnEnemyTutorialHightHP(0, spawnerPoints[18].position + new Vector3(i, 0.0f, 0.0f));
            //    }
            //    for (int i = 0; i < 3; i++)
            //    {
            //        SpawnEnemyDistanceShoot(1, spawnerPoints[19].position);
            //        SpawnEnemyDistanceShoot(1, spawnerPoints[20].position);
            //    }

            //    MoveTriggerZoneToTheNextPosition(11);
            //    break;
            //case 12://Area13
            //    FindObjectOfType<AudioManager>().Pause("TutorialMainBGM");
            //    FindObjectOfType<AudioManager>().Play("FireBrunchBGM");
            //    //SpawnTutorialBoss(2, spawnerPoints[21].position);
            //    StartCoroutine(PresentBoss(5.0f));
            //    triggerZone.SetActive(false);
            //    //MoveTriggerZoneToTheNextPosition(12);
            //    break;
            //case 13://Area14



            //    //MoveTriggerZoneToTheNextPosition(13);
            //    break;
        }

    }
    IEnumerator CheckIfGameIsClear(float duration)
    {
        float timer = 0.0f;
        float waitTimer = 0.0f;
        GameObject bossObj = GameObject.FindGameObjectWithTag("FourHandsBoss");
        GameObject gameClearPanel = Resources.Load<GameObject>("Prefabs/StateUIs/DemoEndPanel");
        bool justOnceDisplayGameCLearPanel = false;
        while (timer < duration)
        {
            if (bossObj == null)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= 3.0f)
                {
                    if (justOnceDisplayGameCLearPanel == false)
                    {
                        Instantiate(gameClearPanel, Vector3.zero, Quaternion.identity);
                        gm.audioManager.Pause("MainBgm");
                        gm.audioManager.Pause("BossBgm");
                        gm.audioManager.Play("GameFinishBgm");
                        justOnceDisplayGameCLearPanel = true;
                    }
                }
            }
            yield return null;
        }
    }

    public override void MoveTriggerZoneToTheNextPosition(int positionIndex)
    {
        triggerZone.transform.position = phaseLimitsPos[positionIndex].position;
    }

    public IEnumerator PresentBoss(float duration)
    {

        float time = 0.0f;
        bool justOnceBossName = false;
        while (time < duration + 2)
        {
            if (time < duration)
            {
                player.GetComponent<MovementV1>().enabled = false;
                player.GetComponent<PlayerCombatV1>().enabled = false;
                GameObject.Find("MainCamera").GetComponent<CameraControl>().enabled = false;
                GameObject.Find("MainCamera").GetComponent<Animator>().enabled = true;
                if (justOnceBossName == false)
                {
                    bossNameUI.SetActive(true);
                    justOnceBossName = true;
                }
            }
            else
            {
                GameObject.Find("MainCamera").GetComponent<CameraControl>().enabled = true;
                GameObject.Find("MainCamera").GetComponent<Animator>().enabled = false;

                bossNameUI.SetActive(false);
                player.GetComponent<MovementV1>().enabled = true;
                player.GetComponent<PlayerCombatV1>().enabled = true;
            }
            time += Time.deltaTime;
            yield return null;
        }
    }
    void SpawnEnemyRoundOnHand(int i, Vector3 pos)
    {
        //ChooseARandomStrengthForEnemy();
        enemyScale = new Vector3(/*meleeAttackEnemyScale*/2f, 2f/*meleeAttackEnemyScale*/, 1.0f);
        enemyAttack = att;
        enemySpeed = 4.0f;
        enemyMaxHP = 100;
        enemyHP = 100;
        enemyDropedEnergy = dropedEnergy;
        enemyDropedCredits = dropedCredits;
        enemyColor = Colors.Pink;
        SpawnEnemy(i, pos);
    }
    void SpawnEnemyStage1(int i, Vector3 pos)
    {
        //ChooseARandomStrengthForEnemy();
        enemyScale = new Vector3(/*meleeAttackEnemyScale*/1.5f, 1.5f/*meleeAttackEnemyScale*/, 1.0f);
        enemyAttack = att;
        enemySpeed = 4.0f;
        enemyMaxHP = 20;
        enemyHP = 20;
        enemyDropedEnergy = dropedEnergy;
        enemyDropedCredits = dropedCredits;
        enemyColor = Colors.Pink;
        SpawnEnemy(i, pos);

    }
    void SpawnEnemyDistanceShoot(int i, Vector3 pos)
    {
        //ChooseARandomStrengthForEnemy();
        enemyScale = new Vector3(/*meleeAttackEnemyScale*/1.5f, 1.5f/*meleeAttackEnemyScale*/, 1.0f);
        enemyAttack = att;
        enemySpeed = 1.0f;
        enemyMaxHP = 20;
        enemyHP = 20;
        enemyDropedEnergy = dropedEnergy;
        enemyDropedCredits = dropedCredits;
        enemyColor = Colors.Red;
        SpawnEnemy(i, pos);
    }
    void SpawnEnemyDistanceMultiColorShoot(int i, Vector3 pos)
    {
        //ChooseARandomStrengthForEnemy();
        enemyScale = new Vector3(/*meleeAttackEnemyScale*/1.5f, 1.5f/*meleeAttackEnemyScale*/, 1.0f);
        enemyAttack = att;
        enemySpeed = 1.0f;
        enemyMaxHP = 20;
        enemyHP = 20;
        enemyDropedEnergy = dropedEnergy;
        enemyDropedCredits = dropedCredits;
        enemyColor = Colors.Pink;
        SpawnEnemy(i, pos);
    }
    void SpawnEnemyStage1HightHP(int i, Vector3 pos)
    {
        //ChooseARandomStrengthForEnemy();
        enemyScale = new Vector3(2.0f, 2.0f, 1.0f);
        enemyAttack = 4;
        enemySpeed = 4.0f;
        enemyMaxHP = 200;
        enemyHP = 200;
        enemyDropedEnergy = dropedEnergy;
        enemyDropedCredits = dropedCredits;
        enemyColor = Colors.Pink;
        SpawnEnemy(i, pos);
    }
    void SpawnMuscularEnemy(int i, Vector3 pos)
    {
        //ChooseARandomStrengthForEnemy();
        enemyScale = new Vector3(4.0f, 4.0f, 1.0f);

        enemyAttack = 4;
        enemySpeed = 4.0f;
        enemyMaxHP = 1000;
        enemyHP = 1000;
        enemyDropedEnergy = dropedEnergy;
        enemyDropedCredits = dropedCredits;
        enemyColor = Colors.Pink;
        SpawnEnemy(i, pos);
    }
    void SpawnStage1Boss(int i, Vector3 pos)
    {
        //ChooseARandomStrengthForEnemy();
        enemyScale = new Vector3(3.0f, 3.0f, 1.0f);
        enemyAttack = 1;
        enemySpeed = 4.0f;
        enemyMaxHP = 5000;
        enemyHP = 5000;
        enemyDropedEnergy = dropedEnergy;
        enemyDropedCredits = dropedCredits;
        enemyColor = Colors.Pink;
        SpawnEnemy(i, pos);
    }
}
