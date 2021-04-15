//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using UnityEngine.Tilemaps;
//using MankindGames;

//public class Level1Manager : MapManager
//{
//    // to do 
//    //- give a variable  size wich is increasing with infit spawner for the lists instead of 200

//    // Start is called before the first frame update


//    public GameObject lastWallBeforeTirthPart;
//    public GameObject playerUI;
//    public GameObject blackWall;

//    //public List<Vector3Int> tilesToErase;
//    //public Tilemap myTilemap;

//    float timer1;
//    float timer2;

//    float timer3;
//    float timer4;

//    int rndSpeed;
//    int rndHP;
//    int rndScale;
//    int rndAtt;
//    int rnd;

//    bool stopFirstInfinitSpawn;

//    int infinitEnemyNum1 = 1;
//    int infinitEnemyNum2 = 1;
//    int infinitEnemyNum3 = 2;

//    int att = 3;
//    float meleeAttackEnemyScale = 1;

//    protected override void Start()
//    {
//        base.Start();

//        SpawnFollowerNPC();
//        phase = 0;

//        timer1 = 0;// make the first enemy spawn directly without wiating the end of the timer 
//        timer2 = 0;

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
//    void SpawnFollowerNPC()
//    {
//        enemyHP = 20;
//        enemyColor = Colors.Blue;
//        enemySpeed = 5;// need to be set after the enemy spawn
//        OnLoadPrefab(0, "FollowerNPC/Follower1");
//        SpawnEnemy(0, player.transform.position);

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
//        //Log.log("Aiming state " + isItAutoAiming);
//        //Log.log("player activate auto aim " + player.ActivateAutoAim);
//        //Log.log("enemySpeed " + enemySpeed);
//        //Log.log("just once " + justOnce[phase]);

//        //Log.log("player position " + gm.player.transform.position);
//        CheckPhaseCondition();
//    }

//    void CheckPhaseCondition()
//    {
//        if (gm.player.transform.position.y >= phaseLimitsPos[0].position.y)//First part
//        {

//            phase = 1;
//            if (justOnce[phase] == true)
//            {
//                isItAutoAiming = true;
//                SpawnInfinitEnemy1();
//            }
//        }
//        if (gm.player.transform.position.y > phaseLimitsPos[1].position.y)
//        {

//            phase = 2;
//            if (justOnce[phase] == true)// first pack
//            {
//                FirstPartPacksOfEnemies(3, 6,
//                    spawnerPoints[2].transform.position, spawnerPoints[3].transform.position
//                    );
//                justOnce[phase] = false;

//            }
//        }
//        if (gm.player.transform.position.y > phaseLimitsPos[2].position.y)
//        {
//            phase = 3;
//            if (justOnce[phase] == true)// second pack
//            {
//                FirstPartPacksOfEnemies(4, 8,
//                    spawnerPoints[4].transform.position, spawnerPoints[5].transform.position
//                  );


//                justOnce[phase] = false;
//            }
//        }
//        if (gm.player.transform.position.y > phaseLimitsPos[3].position.y)//thirth pack
//        {
//            phase = 4;
//            if (justOnce[phase] == true)
//            {
//                FirstPartPacksOfEnemies(5, 10,
//                    spawnerPoints[6].transform.position, spawnerPoints[7].transform.position
//                   );

//                justOnce[phase] = false;
//            }
//        }
//        if (gm.player.transform.position.y > phaseLimitsPos[4].position.y)//fourth pack
//        {
//            phase = 5;
//            if (justOnce[phase] == true)
//            {
//                FirstPartPacksOfEnemies(2, 4,
//                    spawnerPoints[8].transform.position, spawnerPoints[9].transform.position
//                    );

//                justOnce[phase] = false;
//            }
//        }

//        if (gm.player.transform.position.y > phaseLimitsPos[5].position.y)//emprisoned enemy  part1
//        {
//            phase = 6;
//            if (justOnce[phase] == true)
//            {
//                stopFirstInfinitSpawn = true;


//                SpawnEmprisonedEnemy(5, 10,//first prison
//                    spawnerPoints[10].transform.position, spawnerPoints[11].transform.position
//                    );

//                SpawnEmprisonedEnemy(2, 4,// second prison
//                    spawnerPoints[12].transform.position, spawnerPoints[13].transform.position
//                    );

//                CheckIfDisableAreaPrison();

//                justOnce[phase] = false;
//            }
//        }
//        if (gm.player.transform.position.y > phaseLimitsPos[6].position.y)//emprisoned enemy  part2
//        {
//            phase = 7;
//            if (justOnce[phase] == true)
//            {

//                SpawnEmprisonedEnemy(2, 4,//first prison
//                    spawnerPoints[14].transform.position, spawnerPoints[15].transform.position
//                    );

//                SpawnEmprisonedEnemy(2, 4,//second prison
//                    spawnerPoints[16].transform.position, spawnerPoints[17].transform.position
//                    );

//                FirstPartPacksOfEnemies(2, 2,//right side
//                    spawnerPoints[18].transform.position, spawnerPoints[19].transform.position
//                    );

//                SpawnEmprisonedEnemy(10, 20,//next to the exit
//                    spawnerPoints[20].transform.position, spawnerPoints[21].transform.position
//                    );

//                justOnce[phase] = false;
//            }
//        }
//        if (gm.player.transform.position.y > phaseLimitsPos[7].position.y)//Second part 
//        {
//            justOnce[1] = false;//disable CalculateFrequencesOfSpawn()
//            phase = 8;
//            if (justOnce[phase] == true)
//            {
//                SpawnInfiniteEnemies(spawnerPoints[22].transform.position);
//                SpawnInfiniteEnemies(spawnerPoints[23].transform.position);
//                SpawnInfiniteEnemies(spawnerPoints[24].transform.position);
//                SpawnInfiniteEnemies(spawnerPoints[25].transform.position);
//                IncreaseInfinitEnemyNumEvery5Sec();

//            }
//        }

//        if (gm.player.transform.position.y > phaseLimitsPos[8].position.y)//thirth part
//        {
//            phase = 9;

//            if (justOnce[phase] == true)
//            {

//                DisableFirstAndSecondPhase();

//                SpawnTentacleAttackEnemy(15, spawnerPoints[26].transform.position);
//                SpawnTentacleAttackEnemy(15, spawnerPoints[27].transform.position);
//                SpawnTentacleAttackEnemy(15, spawnerPoints[28].transform.position);

//                SpawnTentacleAttackEnemy(15, spawnerPoints[29].transform.position);
//                SpawnTentacleAttackEnemy(15, spawnerPoints[30].transform.position);

//                SpawnTentacleAttackEnemy(15, spawnerPoints[31].transform.position);
//                SpawnTentacleAttackEnemy(15, spawnerPoints[32].transform.position);

//                justOnce[phase] = false;
//            }
//        }
//        if (gm.player.transform.position.y > phaseLimitsPos[9].position.y)
//        {
//            phase = 10;

//            if (justOnce[phase] == true)
//            {

//                FirstPartPacksOfEnemies(10, 20,
//                    spawnerPoints[33].transform.position,
//                    spawnerPoints[34].transform.position);

//                justOnce[phase] = false;
//            }
//        }
//        if (gm.player.transform.position.y > phaseLimitsPos[10].position.y)
//        {
//            phase = 11;

//            if (justOnce[phase] == true)
//            {
//                FirstPartPacksOfEnemies(5, 10,
//                    spawnerPoints[35].transform.position,
//                    spawnerPoints[36].transform.position);

//                SpawnTentacleAttackEnemy(15, spawnerPoints[37].transform.position);
//                SpawnTentacleAttackEnemy(15, spawnerPoints[38].transform.position);
//                SpawnTentacleAttackEnemy(15, spawnerPoints[39].transform.position);

//                justOnce[phase] = false;
//            }
//        }
//        if (gm.player.transform.position.y > phaseLimitsPos[11].position.y)
//        {
//            phase = 12;

//            if (justOnce[phase] == true)
//            {
//                SpawnBoss(21, spawnerPoints[40].transform.position);
//                SpawnLittleSister(22, spawnerPoints[41].transform.position);

//                blackWall.SetActive(true);

//                justOnce[phase] = false;
//            }
//        }
//        if (gm.player.transform.position.y > phaseLimitsPos[12].position.y)
//        {
//            phase = 13;
//            if (justOnce[phase] == true)
//            {
//                DisableEverythingBeforeBoss();

//                justOnce[phase] = false;
//            }
//        }
//        if (phase == 13)
//        {
//            // stop aiming if boss is dead
//            if (GameObject.FindGameObjectWithTag("Boss") == null)
//            {
//                //isItAutoAiming = false;
//                //player.ActivateAutoAim = isItAutoAiming;
//            }
//            else
//            {

//            }
//        }
//    }




//    ///////////////////
//    /// INFINIT Spawners
//     ///////////////////

//    void SpawnInfiniteEnemies(Vector3 pos)
//    {
//        timer3 += Time.deltaTime;

//        if (timer3 >= 5)

//        {
//            for (int i = 0; i < infinitEnemyNum3; i++)

//            {
//                SpawnMelleeAttackEnemy(1, pos);
//            }
//            timer3 = 0;
//        }

//    }
//    void IncreaseInfinitEnemyNumEvery5Sec()
//    {
//        timer4 += Time.deltaTime;
//        if (timer4 >= 5)
//        {
//            Log.log("infinit enemy " + infinitEnemyNum3);
//            infinitEnemyNum3++;
//            timer4 = 0;

//        }
//    }

//    void SpawnInfinitEnemy1()
//    {
//        if (stopFirstInfinitSpawn == false)
//        {
//            timer1 += Time.deltaTime;
//            if (timer1 >= 5.0f)
//            {
//                for (int i = 0; i <= infinitEnemyNum1; i++)
//                {
//                    SpawnMelleeAttackEnemy(1, spawnerPoints[0].transform.position/*new Vector3(-8.0f, -4.0f, 0.0f)*/);
//                }

//                //infinitEnemyNum1++;
//                timer1 = 0;
//            }
//            timer2 += Time.deltaTime;
//            if (timer2 > 5)
//            {
//                for (int i = 1; i <= infinitEnemyNum2; i++)
//                {
//                    SpawnMelleeAttackEnemy(i, spawnerPoints[1].transform.position
//                        - new Vector3(-i, 0.0f, 0.0f)/*new Vector3(9.0f - i, 13.0f, 0.0f)*/);
//                }
//                //infinitEnemyNum2++;
//                timer2 = 0;
//            }
//        }
//    }
//    /// /////////////////////////////////////////////



//    // we might not need a trigger zone to activate phase
//    //public override void ActivatePhase()
//    //{
//    //    if (phase == 0)
//    //    {
//    //        isItAutoAiming = true;
//    //    }

//    //}

//    void FirstPartPacksOfEnemies(int numEnemyLeft, int numEnemyRight,
//        Vector3 posLeft, Vector3 posRight)
//    {
//        for (int i = 1; i < numEnemyLeft; i++)
//        {
//            SpawnMelleeAttackEnemy(i, posLeft + new Vector3(i, 0.0f, 0.0f));
//        }
//        for (int j = numEnemyLeft; j < numEnemyRight; j++)
//        {
//            SpawnMelleeAttackEnemy(j, posRight + new Vector3(-j, 0.0f, 0.0f));
//        }
//    }

//    void SpawnEmprisonedEnemy(int numEnemyLeft, int numEnemyRight,
//        Vector3 posLeft, Vector3 posRight)
//    {
//        for (int i = 1; i < numEnemyLeft; i++)
//        {
//            SpawnMelleeAttackEnemy(i, posLeft);
//        }
//        for (int j = numEnemyLeft; j < numEnemyRight; j++)
//        {
//            SpawnMelleeAttackEnemy(j, posRight);
//        }
//    }

//    /// //////////////////////////

//    void SpawnLittleSister(int i, Vector3 pos)
//    {
//        enemyHP = 100;
//        enemySpeed = 1.0f;
//        enemyScale = new Vector3(1.0f, 1.0f, 1.0f);
//        enemyColliderScale = new Vector3(0.9f, 0.9f);

//        enemyColor = Colors.Red;
//        OnLoadPrefab(i, "NPCTutorial");

//        SpawnEnemy(i, pos);

//    }

//    void SpawnBoss(int i, Vector3 pos)
//    {
//        enemyHP = 5000;
//        enemyMaxHP = 5000;
//        enemySpeed = 1.0f;
//        // boss attack set in the blackBossCombatScript
//        enemyScale = new Vector3(2.0f, 2.0f, 1.0f);
//        enemyColliderScale = new Vector3(0.9f, 0.9f);

//        enemyColor = Colors.Black;
//        OnLoadPrefab(i, "Black/BlackBoss");
//        SpawnEnemy(i, pos);
//    }

//    void SpawnTentacleAttackEnemy(int i, Vector3 pos)
//    {
//        enemyHP = 300;
//        enemyMaxHP = 300;
//        enemySpeed = 1.0f;
//        enemyScale = new Vector3(1.0f, 1.0f, 1.0f);
//        enemyColliderScale = new Vector3(0.9f, 0.9f);
//        enemyAttack = 4;
//        enemySpeed = 1.0f;
//        enemyColor = Colors.Black;
//        OnLoadPrefab(i, "Black/EnemySideStepTentacle");
//        SpawnEnemy(i, pos);
//    }

//    void SpawnMelleeAttackEnemy(int i, Vector3 pos)
//    {
//        ChooseARandomStrengthForEnemy();
//        enemyScale = new Vector3(meleeAttackEnemyScale, meleeAttackEnemyScale, 1.0f);
//        enemyAttack = att;

//        enemyColor = Colors.Black;
//        OnLoadPrefab(i, "MeleeAttackPathFinding");
//        SpawnEnemy(i, pos);
//    }



//    /// ///////////////////////////////
//    /// 

//    void ChooseARandomStrengthForEnemy()
//    {
//        rnd = UnityEngine.Random.Range(1, 3);
//        Log.log("rnd " + rnd);
//        if (rnd == 1)
//        {
//            meleeAttackEnemyScale = rnd;
//            att = rnd;
//            enemyHP = 50;
//            enemyMaxHP = 50;
//            enemySpeed = 4 / rnd;
//        }
//        else
//        {
//            rnd = UnityEngine.Random.Range(2, 4);
//            Log.log("rnd2 " + rnd);
//            meleeAttackEnemyScale = rnd;
//            att = 2 * rnd;
//            enemyHP = rnd * 50;
//            enemyMaxHP = rnd * 50;
//            enemySpeed = 4 / rnd;
//            enemyColliderScale = new Vector3(0.9f, 0.9f) / 2;
//            Log.log("hp " + enemyHP);
//        }
//    }

//    void DisableFirstAndSecondPhase()
//    {

//        lastWallBeforeTirthPart.GetComponent<SpriteRenderer>().color = Color.black;
//        lastWallBeforeTirthPart.GetComponent<ColorObject>().color = Colors.None;
//        lastWallBeforeTirthPart.GetComponent<BoxCollider2D>().enabled = true;
//        foreach (GameObject gameObj in FindObjectsOfType<GameObject>())
//        {
//            if (gameObj.transform.position.y < phaseLimitsPos[8].position.y - 5.0f
//                && gameObj.name.Contains("Tutorial") == true ||
//                gameObj.transform.position.y < phaseLimitsPos[8].position.y - 5.0f
//                && gameObj.name == "HpOnly(Clone)")
//            {
//                gameObj.SetActive(false);
//            }
//        }
//        justOnce[8] = false;

//    }


//    void DisableEverythingBeforeBoss()
//    {
//        timer1 = 0.0f;
//        timer2 = 0.0f;
//        timer3 = 0.0f;
//        // for optimisation
//        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
//        {
//            if (obj.transform.position.y < 312.0f && obj.transform.name == "HpOnly(Clone)")
//            {
//                obj.SetActive(false);
//            }
//            if (obj.transform.position.y < 300.0f && obj.layer == 10 ||
//               obj.transform.position.y < 300.0f && obj.layer == 9 ||
//               obj.transform.position.y < 300.0f && obj.name.Contains("Tutorial") == true
//               )//disable walls
//            {
//                obj.SetActive(false);
//            }
//        }
//    }

//    void CheckIfDisableAreaPrison()
//    {
//        if (gm.WorldColor == Colors.Green)
//        {
//            foreach (GameObject gameObj in FindObjectsOfType<GameObject>())
//            {
//                if (gameObj.name.Contains("AreaPrison") == true)
//                {
//                    gameObj.SetActive(false);
//                }
//            }
//        }
//        else
//        {
//            foreach (GameObject gameObj in FindObjectsOfType<GameObject>())
//            {
//                if (gameObj.name.Contains("AreaPrison") == true)
//                {
//                    gameObj.SetActive(true);
//                }
//            }
//        }
//    }
//    //erase tiles
//    //public void eraseTiles()
//    //{
//    //    for (int i = 0; i < tilesToErase.Count; i++)
//    //    {
//    //        myTilemap.SetTile(tilesToErase[i], null);
//    //    }
//    //}

//}
