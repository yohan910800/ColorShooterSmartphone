using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MankindGames;
using UnityEngine.UI;
using TMPro;

public class TutorialMapManager : MapManager
{

    public GameObject invisibleWallTop;// get an invisible wall on the top of the trigger zone.
    public GameObject invisibleWallBottom; //get an invisible wall on the bottom of the trigger zone
    public GameObject[] tutorialSigns;
    public GameObject[] tutorialPhaseWalls;
    public GameObject stopTimeMeleeModeUIElement;
    public GameObject bossNameUI;

    public List<GameObject> enemyInTheArea=new List<GameObject>();

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
    int phaseoffset;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //if (File.Exists(/*Application.dataPath + "/savedata/autoSaveData.test"*/))
        //{
        //    phase = SaveSystem.LoadAutoSaveData().phase;
        //}
        //else phase = 0;
        //if (phase < 0) phase = 0;


        FindObjectOfType<AudioManager>().Play("MainBgm");
        OnLoadPrefab(0, "MeleeAttackPathFinding");
        OnLoadPrefab(1, "Red/Ranged1_R_RGO");
        OnLoadPrefab(2, "Tutorial/FireBrunch");

        SetTutorialUI();
        dropedEnergy = 10;
        dropedCredits = 10;
        bossNameUI= GameObject.Find("TutorialUI").transform.Find("BossName").gameObject;

        //if (File.Exists(Application.dataPath + "/savedata/autoSaveData.test"))
        //{
        //    phaseoffset = SaveSystem.LoadAutoSaveData().phase - 2;
        //    MoveTriggerZoneToTheNextPosition(phaseoffset);

        //}
        if (phase != 0)
        {
            phase = phase - 1;
        }

    }
    //void FirstLoadData()
    //{
    //    if (File.Exists(Application.dataPath + "/savedata/autoSaveData.test"))
    //    {
    //        phase = SaveSystem.LoadAutoSaveData().phase;
    //    }
    //    else phase = 0;
    //    if (phase < 0) phase = 0;
    //}
    //void SecondLoadData()
    //{
    //    if (File.Exists(Application.dataPath + "/savedata/autoSaveData.test"))
    //    {
    //        phaseoffset = SaveSystem.LoadAutoSaveData().phase - 2;
    //        MoveTriggerZoneToTheNextPosition(phaseoffset);
            
    //    }
    //    if (phase != 0)
    //    {
    //        phase = phase - 1;
    //    }
    //}
    void SetTutorialUI()
    {
        tutorialUI = GameObject.Find("TutorialUI");
        fingerColor = tutorialUI.transform.GetChild(0).gameObject;
        fingerInventory = tutorialUI.transform.GetChild(1).gameObject;
        killEnemyText = tutorialUI.transform.GetChild(2).gameObject;
        blueCyanExplanetionText = tutorialUI.transform.GetChild(3).gameObject;
        redGreyExplanetionText = tutorialUI.transform.GetChild(4).gameObject;
    }
    private void Update()
    {
        //Log.log("phase " + phase);
        //Log.log("phaseOfsset " + phaseoffset);
        //turn that into a coroutine later
        if (phase == 2)
        {
            phase = 3;
        }
    }
    public override void ActivatePhase(int phase)//check in wich part of the tutorial we are 
    {
        switch (phase)
        {
            case 0://Area1
                MoveTriggerZoneToTheNextPosition(0);
                killEnemyText.SetActive(true);
                
                for (int i = 0; i < 2; i++)
                {
                    SpawnEnemyTutorial(0,spawnerPoints[0].position+new Vector3(3.0f*i,0.0f,0.0f) );
                }
                
                
                break;
            case 1://Area2
                MoveTriggerZoneToTheNextPosition(2);// phase 2 deleted
                for (int i = 0; i < 2; i++)
                {
                    SpawnEnemyDistanceShoot(1, spawnerPoints[2].position+new Vector3(2.0f * i, 0.0f,0.0f));
                }
                SpawnEnemyTutorial(0, spawnerPoints[1].position + new Vector3(2.0f , 0.0f, 0.0f));
                

                
                break;
            case 2://Area3

                
                //for (int i = 0; i < 2; i++)
                //{
                //    SpawnEnemyTutorial(0, spawnerPoints[3].position);
                //}

                //MoveTriggerZoneToTheNextPosition(2);
                break;

            case 3://Area4
                MoveTriggerZoneToTheNextPosition(3);
                for (int i = 0; i < 2; i++)
                {
                    SpawnEnemyDistanceShoot(1, spawnerPoints[4].position);
                    SpawnEnemyDistanceShoot(1, spawnerPoints[5].position);
                    SpawnEnemyTutorial(0, spawnerPoints[3].position);
                }
                break;
            case 4://Area6
                MoveTriggerZoneToTheNextPosition(4);
                for (int i = 0; i < 2; i++)
                {
                    SpawnEnemyDistanceShoot(1, spawnerPoints[6].position);
                    SpawnEnemyDistanceShoot(1, spawnerPoints[7].position);
                }
                break;

            case 5://Area7
                MoveTriggerZoneToTheNextPosition(5);
                player.didThePlayerOverComeTheArea6 = true;
                GameObject.Find("TriggerTutoriialGroup").transform.GetChild(5)
                    .GetComponent<TutorialUIController>().stopTimeUIElement.SetActive(true);

                for (int i = 0; i < 5; i++)
                {
                    SpawnEnemyTutorialHightHP(0, spawnerPoints[8].position+new Vector3(2.0f*(i/2),0.0f,0.0f));
                    //SpawnEnemyTutorial(1, spawnerPoints[8].position);
                    //SpawnEnemyTutorial(1, spawnerPoints[9].position);
                }
                
                break;

            case 6://Area8
                MoveTriggerZoneToTheNextPosition(6);
                dropedEnergy = 5;
                for (int i = 0; i < 2; i++)
                {
                    SpawnEnemyTutorialHightHP(0, spawnerPoints[9].position);
                    SpawnEnemyDistanceShoot(1, spawnerPoints[9].position);
                    SpawnEnemyDistanceShoot(1, spawnerPoints[10].position);
                }

                
                break;



            case 7://Area9

                for (int i = 0; i < 2; i++)
                {
                    SpawnEnemyTutorialHightHP(0, spawnerPoints[11].position+new Vector3(2.0f*i,0.0f,0.0f));
                }

                MoveTriggerZoneToTheNextPosition(7);
                break;
            case 8://Area10
                for (int i = 0; i < 3; i++)
                {
                    SpawnEnemyTutorialHightHP(0, spawnerPoints[12].position+ new Vector3(2.0f * i, 0.0f, 0.0f));
                }


                MoveTriggerZoneToTheNextPosition(8);
                break;
            case 9://Area11

                for (int i = 0; i < 6; i++)
                {
                    SpawnEnemyTutorial(0, spawnerPoints[14].position+new Vector3(i,0.0f,0.0f));
                }
                for (int i = 0; i < 3; i++)
                {
                    SpawnEnemyDistanceShoot(1, spawnerPoints[13].position);
                }

                MoveTriggerZoneToTheNextPosition(9);
                break;
            case 10://Area12

                for (int i = 0; i < 6; i++)
                {
                    SpawnEnemyTutorialHightHP(0, spawnerPoints[15].position + new Vector3(i, 0.0f, 0.0f));
                }
                for (int i = 0; i < 3; i++)
                {
                    SpawnEnemyDistanceShoot(1, spawnerPoints[16].position);
                    SpawnEnemyDistanceShoot(1, spawnerPoints[17].position);
                }

                MoveTriggerZoneToTheNextPosition(10);
                break;

            case 11://Area13

                for (int i = 0; i < 6; i++)
                {
                    SpawnEnemyTutorialHightHP(0, spawnerPoints[18].position + new Vector3(i, 0.0f, 0.0f));
                }
                for (int i = 0; i < 3; i++)
                {
                    SpawnEnemyDistanceShoot(1, spawnerPoints[19].position);
                    SpawnEnemyDistanceShoot(1, spawnerPoints[20].position);
                }

                MoveTriggerZoneToTheNextPosition(11);
                break;
            case 12://Area14
                FindObjectOfType<AudioManager>().Pause("MainBgm");
                FindObjectOfType<AudioManager>().Play("BossBgm");
                SpawnTutorialBoss(2, spawnerPoints[21].position);
                StartCoroutine(PresentBoss(5.0f));
                triggerZone.SetActive(false);
                StartCoroutine(CheckIfGameIsClear(20.0f));//start infinit loop
                break;
        }
    }
    //infinit loop
    IEnumerator CheckIfGameIsClear(float duration)
    {
        float timer=0.0f;
        float waitTimer = 0.0f;
        GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
        GameObject gameClearPanel=Resources.Load<GameObject>("Prefabs/StateUIs/StageClearPanel");
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
        while (time < duration+2)
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
    void SpawnEnemyTutorial(int i, Vector3 pos)
    {
        //ChooseARandomStrengthForEnemy();
        enemyScale = new Vector3(/*meleeAttackEnemyScale*/1.5f, 1.5f/*meleeAttackEnemyScale*/, 1.0f);
        enemyAttack = att;
        enemySpeed = 4.0f;
        enemyMaxHP = 20;
        enemyHP = 20;
        enemyDropedEnergy = dropedEnergy;
        enemyDropedCredits = dropedCredits;
        enemyColor = Colors.Red;
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
    void SpawnEnemyTutorialHightHP(int i, Vector3 pos)
    {
        //ChooseARandomStrengthForEnemy();
        enemyScale = new Vector3(2.0f, 2.0f, 1.0f);
        enemyAttack = 1;
        enemySpeed = 4.0f;
        enemyMaxHP = 50;
        enemyHP = 50;
        enemyDropedEnergy = dropedEnergy;
        enemyDropedCredits = dropedCredits;
        enemyColor = Colors.Red;
        SpawnEnemy(i, pos);
    }
    void SpawnTutorialBoss(int i, Vector3 pos)
    {
        //ChooseARandomStrengthForEnemy();
        enemyScale = new Vector3(3.0f, 3.0f, 1.0f);
        enemyAttack = 4;
        enemySpeed = 4.0f;
        enemyMaxHP = 3000;
        enemyHP = 3000;
        enemyDropedEnergy = dropedEnergy;
        enemyDropedCredits = dropedCredits;
        enemyColor = Colors.Red;
        SpawnEnemy(i, pos);
    }
}
