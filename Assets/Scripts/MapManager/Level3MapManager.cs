//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using UnityEngine.Tilemaps;
//using MankindGames;

//public class Level3MapManager : MapManager
//{
//    //note to do
//    //give a randome speed or randome attack speed to the charge enemies

//    public GameObject firstDoorBoss;
//    public GameObject secondDoorBoss;

//    public GameObject safeZoneLeft;
//    public GameObject safeZoneRight;


//    ICharacter firstBoss;

//    float timer1;
//    float timer2;

//    float timer3;
//    float timer4;

//    int rndSpeed;

//    protected override void Start()
//    {
//        base.Start();

//        phase = 0;

//        //timer1 = 19;// make the first enemy spawn directly without wiating the end of the timer 
//        //timer2 = 15;

//        SizeArrays();
//        for (int i = 0; i < 50; i++)//tmp
//        {
//            justOnce[i] = true;
//        }
//    }

//    void SizeArrays()
//    {
//        prefabs = new GameObject[200];
//        enemyArray = new ICharacter[200];

//        justOnce = new bool[200];
//    }

//    void Update()
//    {
//        /////tmp
//        //playerUI = GameObject.Find("PlayerStateUI(Clone)");
//        //    playerUI.SetActive(true);
//        //foreach(Transform child in playerUI.transform)
//        //{
//        //    child.gameObject.SetActive(true);
//        //}
//        //Log.log("PHASE " + phase);
//        Log.log("Aiming state " + isItAutoAiming);
//        //Log.log("player activate auto aim " + player.ActivateAutoAim);
//        //Log.log("enemySpeed " + enemySpeed);
//        //Log.log("just once " + justOnce[phase]);

//        //Log.log("player position " + gm.player.transform.position);
//        CheckPhaseCondition();
//    }

//    void CheckPhaseCondition()
//    {
//        // BEGINIG of the dialog part
//        isItAutoAiming = true;

//        if (gm.player.transform.position.y >= phaseLimitsPos[0].position.y)
//        {
//            phase = 1;

//            if (justOnce[phase] == true)
//            {
//                SpawnBossImage(1, spawnerPoints[0].position);
//                justOnce[phase] = false;
//            }
//        }

//        // END of the dialog part

//        // BEGINIG of the enemies's introduction part

//        if (gm.player.transform.position.y >= phaseLimitsPos[1].position.y)
//        {
//            phase = 2;

//            if (justOnce[phase] == true)
//            {
//                SpawnChargeEnemy(2, spawnerPoints[1].position);
//                justOnce[phase] = false;
//            }
//        }

//        if (gm.player.transform.position.y >= phaseLimitsPos[2].position.y)
//        {
//            phase = 3;
//            if (justOnce[phase] == true)
//            {

//                for (int i = 0; i < 2; i++)
//                {
//                    SpawnChargeEnemy(3, spawnerPoints[2].position);
//                    SpawnChargeEnemy(4, spawnerPoints[3].position);
//                }

//                justOnce[phase] = false;
//            }
//        }

//        if (gm.player.transform.position.y >= phaseLimitsPos[3].position.y)
//        {
//            phase = 4;
//            if (justOnce[phase] == true)
//            {
//                for (int i = 0; i < 2; i++)
//                {
//                    SpawnMissileZoneEnemy(5, spawnerPoints[4].position);
//                }

//                justOnce[phase] = false;
//            }
//        }

//        if (gm.player.transform.position.y >= phaseLimitsPos[4].position.y)
//        {
//            phase = 5;
//            if (justOnce[phase] == true)
//            {
//                for (int i = 0; i < 4; i++)
//                {
//                    SpawnChargeEnemy(6, spawnerPoints[5].position);
//                }
//                justOnce[phase] = false;
//            }
//        }

//        // END of the enemies's introduction part

//        // BEGINIG of the middle trap part

//        if (gm.player.transform.position.y >= phaseLimitsPos[5].position.y)
//        {
//            phase = 6;
//            if (justOnce[phase] == true)
//            {
//                SpawnChargeEnemy(7, spawnerPoints[6].position);

//                for (int i = 0; i < 2; i++)
//                {
                    
//                    SpawnRifleSideEnemy(8, spawnerPoints[7].position);
//                    SpawnRifleSideEnemy(9, spawnerPoints[8].position);

//                    SpawnRifleSideEnemy(12, spawnerPoints[11].position);
//                    SpawnRifleSideEnemy(13, spawnerPoints[12].position);
//                }

//                for (int i = 0; i < 3; i++)
//                {
//                    SpawnMissileZoneEnemy(10, spawnerPoints[9].position);//on the deep side
//                }
//                SpawnChargeEnemy(11, spawnerPoints[10].position);

//                justOnce[phase] = false;
//            }
//        }

//        if (gm.player.transform.position.y >= phaseLimitsPos[6].position.y)
//        {
//            phase = 7;
//            if (justOnce[phase] == true)
//            {
//                SpawnChargeEnemy(14, spawnerPoints[13].position);

//                for (int i = 0; i < 2; i++)
//                {

//                    SpawnRifleSideEnemy(15, spawnerPoints[14].position);

//                    SpawnRifleSideEnemy(16, spawnerPoints[15].position);

//                    SpawnRifleSideEnemy(17, spawnerPoints[17].position);
//                }
//                for (int i = 0; i < 3; i++)
//                {
//                    SpawnChargeEnemy(18, spawnerPoints[16].position);

//                }
//                justOnce[phase] = false;
//            }

//        }
//        if (gm.player.transform.position.y >= phaseLimitsPos[7].position.y)
//        {
//            phase = 8;
//            if (justOnce[phase] == true)
//            {
//                for (int i = 0; i < 5; i++)
//                {
//                    SpawnChargeEnemy(19, spawnerPoints[18].position);

//                }
//                for (int i = 0; i < 2; i++)
//                {

//                    SpawnRifleSideEnemy(20, spawnerPoints[19].position);
//                    for (int j = 0; j < 2; j++)
//                    {
//                        SpawnRifleSideEnemy(21, spawnerPoints[20].position);
//                        SpawnRifleSideEnemy(22, spawnerPoints[21].position);
//                        SpawnRifleSideEnemy(23, spawnerPoints[22].position);

//                    }
//                }
//                justOnce[phase] = false;
//            }
//        }
//        if (gm.player.transform.position.y >= phaseLimitsPos[8].position.y)
//        {
//            phase = 9;
//            if (justOnce[phase] == true)
//            {
//                for (int j = 0; j < 2; j++)
//                {
//                    SpawnRifleSideEnemy(24, spawnerPoints[23].position);
//                    SpawnRifleSideEnemy(25, spawnerPoints[24].position);

//                    SpawnRifleSideEnemy(26, spawnerPoints[25].position);
//                    SpawnRifleSideEnemy(27, spawnerPoints[26].position);

//                    SpawnRifleSideEnemy(28, spawnerPoints[27].position);
//                    SpawnRifleSideEnemy(29, spawnerPoints[28].position);

//                    SpawnRifleSideEnemy(32, spawnerPoints[31].position);
//                    SpawnRifleSideEnemy(33, spawnerPoints[32].position);

//                }
//                justOnce[phase] = false;
//            }
//        }
//        if (gm.player.transform.position.y >= phaseLimitsPos[9].position.y)
//        {
//            phase = 10;
//            if (justOnce[phase] == true)
//            {
//                SpawnFirstBoss(30, spawnerPoints[29].position);

//                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("RifleSide"))
//                {
//                    enemy.GetComponent<ICharacter>().GetStats().SetSpeed(2);
//                }

//                justOnce[phase] = false;
//            }
//            if (firstBoss.GetStats().HP <= 0)
//            {
//                firstDoorBoss.SetActive(false);
//            }
//        }
//        //clean phase
//        if (gm.player.transform.position.y >= phaseLimitsPos[10].position.y)
//        {
//            phase = 11;
//            if (justOnce[phase] == true)
//            {
//                DisableEverythingBeforeBoss();

//                justOnce[phase] = false;
//            }
//        }

//        if (gm.player.transform.position.y >= phaseLimitsPos[11].position.y)
//        {
//            phase = 12;
//            if (justOnce[phase] == true)
//            {
//                secondDoorBoss.SetActive(true);
//                SpawnFinalBoss(31, spawnerPoints[30].position);

//                safeZoneLeft.SetActive(true);
//                safeZoneRight.SetActive(true);
//                justOnce[phase] = false;
//            }
//        }
//    }

//    public override void ActivatePhase()
//    {   
//        if (phase == 0)
//        {
//            isItAutoAiming = true;
//        }
//    }

//    void DisableEverythingBeforeBoss()
//    {
//        // for optimisation
//        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
//        {
//            if (obj.transform.position.y < 190.0f && obj.transform.name == "HpOnly(Clone)" ||
//                obj.transform.position.y < 190.0f
//                && obj.name.Contains("Tutorial") == true)
//            {
//                obj.SetActive(false);
//            }
//            if (obj.transform.position.y < 180.0f && obj.layer == 10 ||
//               obj.transform.position.y < 180.0f && obj.layer == 9)//disable walls
//            {
//                obj.SetActive(false);   
//            }
//        }
//    }

//    void SpawnFinalBoss(int i, Vector3 pos)
//    {
//        enemyHP = 3000;
//        enemyMaxHP = 3000;
//        enemyAttack = 3;
//        enemySpeed = 5;

//        enemyColor = Colors.Green;
//        OnLoadPrefab(i, "Green/GRFusinedBoss");
//        SpawnEnemy(i, pos);
//        firstBoss = enemyArray[i];

//    }

//    void SpawnFirstBoss(int i, Vector3 pos)
//    {
//        enemyHP = 2000;
//        enemyMaxHP = 2000;
//        enemyAttack = 2;
        
//        enemyColor = Colors.Green;
//        OnLoadPrefab(i, "Green/GreenBoss");
//        SpawnEnemy(i, pos);
//        firstBoss = enemyArray[i];
//    }

//    void SpawnChargeEnemy(int i, Vector3 pos)
//    {
//        enemyHP = 200;
//        ChooseARandomSpeedForTheEnemies();
//        enemyAttack = 2;

//        enemyColor = Colors.Green;
//        OnLoadPrefab(i, "Green/EnemyCharge");
//        SpawnEnemy(i, pos);
//    }
//    void SpawnMissileZoneEnemy(int i, Vector3 pos)
//    {
//        enemyHP = 300;
//        enemyMaxHP = 300;
//        enemySpeed = 0.0f;
//        enemyAttack = 10;
//        enemyColor = Colors.Green;
//        OnLoadPrefab(i, "Green/GREnemyMissileZone");
//        SpawnEnemy(i, pos);
//    }
//    void SpawnRifleSideEnemy(int i, Vector3 pos)
//    {
//        enemyHP = 100;
//        enemySpeed = 0.0f;
//        enemyAttack = 2;
//        enemyColor = Colors.Green;
//        OnLoadPrefab(i, "Green/RifleSidesGreen");
//        SpawnEnemy(i, pos);
//    }
//    void SpawnBossImage(int i,Vector3 pos)
//    {
//        enemyColor = Colors.Green;
//        OnLoadPrefab(i, "NPCTutorial");//tmp name
//        SpawnEnemy(i, pos);
//    }

//    void ChooseARandomSpeedForTheEnemies()
//    {
//        rndSpeed = UnityEngine.Random.Range(1, 5);
//        enemySpeed = rndSpeed;
//    }
//}
