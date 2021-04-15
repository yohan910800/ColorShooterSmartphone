//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using System.Linq;
//using MankindGames;

//public class OldMapManager : MonoBehaviour
//{

//    public Transform[] spawnerPoints;//size defin in the inspectore
//    public Transform[] phaseLimitsPos;//size defin in the inspectore

//    public GameObject triggerZone;
//    //public GameObject[] tutorialSigns;
//    //tmp
//    public Vector2 interactPos;// this is used to get the position of the interactible Trigger

//    //public Vector2[] spawnPos;

//    //public ICharacter[] enemyArray;//this is used to disable all enemie of the phase 1
//    public List<ICharacter> enemyArray;//this is used to disable all enemie of the phase 1

//    //public 

//    //public GameObject invisibleWallTop;// get an invisible wall on the top of the trigger zone.
//    //public GameObject invisibleWallBottom; //get an invisible wall on the bottom of the trigger zone.
//    //public GameObject permanentWall;//get an invisible wall permanent wall wich prevent the player from going back to the village
//    public GameObject[] prefabs;// get the prefabs wich instantiate through the game

//    public int dialogZoneCounter;// it use in the "Interactible" script to decide wich NPc says wich line
    

//    //public GameObject[] instantiatedGameObject;
//    GameObject prefab;

//    public GameManager gm;
    
//    public Player player;

//    public int phase=0;// this is used to to decide wich enemy is loaded 

//    //tmp maybe
//    public float enemySpeed;
//    public int enemyHP;
//    public int enemyMaxHP;
//    public int enemyAttack;
//    public Vector3 enemyScale;
//    public Vector3 enemyColliderScale = new Vector3(0.9f,0.9f);
//    public Colors enemyColor;


//    public int enemyDeadCount;//this variable count the number of enemy dead in the map 
//    //public bool isItAutoAiming;// is used to check if the player should auto aim or not 
//    //int enemyCount; // to give a unique name to the enemy. used to give a unique line

//    //public bool isItTriggerActive;
//    //public bool[] justOnce; // it is used in the ActivatePhase() function to activate a phase juste one time0
    

//    //float timer;
//    //float timerDisable;

//    protected virtual void Start()
//    {
//        //enemyScale = new Vector3(1.0f, 1.0f, 1.0f);
//        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        
//        //isItTriggerActive = true;
//        phase = 0;
//        SizeArrays();
//        //for (int i = 0; i < 7; i++)
//        //{
//        //    justOnce[i] = true;
//        //}
//        prefab = prefabs[0];
//    }

//    //later
//    void SizeArrays()
//    {
//        prefabs = new GameObject[200];
//        //enemyArray = new ICharacter[200];
//        //justOnce = new bool[200];
//    }

//    //void Update()
//    //{


//    //    DisableTutorialUI();
//    //    CheckPhaseCondition();
//    //    CheckIfTutorialShouldBeDisplay();

//    //    //foreach (ICharacter enemy in enemyArray)
//    //    //{
//    //    //    if (enemy != null)
//    //    //    {
//    //    //        enemy.GetGameObject().GetComponent<Ranged1>().TextFollowEnemy();
//    //    //    }
//    //    //}

//    //}
//    //void SizeArrays()
//    //{
//    //    spawnPos = new Vector2[5];
//    //    prefabs = new GameObject[200];
//    //    enemyArray = new ICharacter[200];
//    //    justOnce = new bool[7];
//    //}

//    //void RescaleTriggerZone()
//    //{
//    //    Vector2 tmp=triggerZone.transform.localScale;
//    //    tmp.y = 40.6f;
//    //    triggerZone.transform.localScale = tmp;

//    //    //Vector2 tmp2 = triggerZone.transform.position;
//    //    //tmp2.y = 64.6f;
//    //    //triggerZone.transform.position = tmp;
//    //}

//    //void MoveTriggerZoneToward(int forwardDist)
//    //{
//    //   Vector2 tmp = triggerZone.transform.position;
//    //   tmp.y = interactPos.y + forwardDist;
//    //   triggerZone.transform.position = tmp;
//    //}





//    //void DisableTutorialUI()
//    //{
//    //    if (phase == 5)
//    //    {
//    //        if (player.GetBulletColor() == Colors.Red)
//    //        {
//    //            DisableBulletColorFingerTutorial();
//    //        }
//    //    }
//    //}
//    //void CheckPhaseCondition()
//    //{
//    //   if (gm.player.transform.position.y>phaseLimitsPos[0].position.y&&phase == 0)
//    //   {
//    //       phase = 1;
//    //   }
//    //   else if (enemyDeadCount == 5&&phase==1)
//    //   {
//    //       phase = 2;
//    //       //RescaleTriggerZone();
//    //       MoveTriggerZoneToward(12);
//    //       permanentWall.SetActive(true);//close the acces to the village
//    //   }

//    //   else if (gm.player.transform.position.y > phaseLimitsPos[1].position.y && phase==2)
//    //   {
//    //       DisableColorFingerTutorial();
//    //       phase = 3;
//    //       MoveTriggerZoneToward(18);
//    //   }
//    //   else if (enemyDeadCount==6&&phase==3)
//    //   {
//    //       phase = 4;
//    //       invisibleWallBottom.SetActive(false);
//    //       MoveTriggerZoneToward(12);
//    //   }
//    //   else if (gm.player.transform.position.y> phaseLimitsPos[2].position.y && phase == 4)
//    //   {
//    //       DisableColorFingerTutorial();
//    //       EnableBulletColorFingerTutorial();

//    //       phase = 5;
//    //       MoveTriggerZoneToward(20);
//    //   }
//    //   else if (enemyDeadCount == 8 /*tmp*/&& phase == 5)
//    //   {
//    //       phase =6;
//    //       MoveTriggerZoneToward(12);
//    //   }
//    //   else if (enemyDeadCount == 13 && phase == 6)
//    //   {
//    //       Log.log("FINISH" );
//    //   }
//    //}



//    //void ClearTheArray()
//    //{
//    //    for (int i = 0; i < spawnPos.Length; i++)
//    //    {
//    //        spawnPos[i] = new Vector2(0.0f, 0.0f);
//    //        if (phase==1)
//    //        {
//    //            enemyArray[i].Kill();
//    //        }
//    //        //instantiatedGameObject[i] = null;
//    //        prefabs[i] = null;
//    //    }
//    //}


//    public virtual void ActivatePhase(int phase)//check in wich part of the tutorial we are 
//    {

//        switch (phase) {

//            case 0:
//                break;
//            case 1:
//                break;
//            case 2:
//                break;
//            case 3:
//                break;
//            case 4:
//                break;
//            case 5:
//                break;
//            case 6:
//                break;
//            case 7:
//                break;


//        }




//        //if (phase == 0)
//        //{
//        //    //isItAutoAiming = false;
//        //    //spawnPos = new Vector2[5];
//        //    ////prefabs = new GameObject[5];
//        //    //if (justOnce[phase] == true)
//        //    //{
//        //    //    for (int i = 0; i < 5; i++)
//        //    //    {
//        //    //        SpawnNPC(i, spawnerPoints[i].position, Colors.Blue);
//        //    //    }
//        //    //    enemyDeadCount -= 5;
//        //    //    justOnce[phase] = false;
//        //    //}
//        //}

//        //else if (phase == 1)
//        //{
//        //    //if (justOnce[phase] == true)
//        //    //{
//        //    //    isItAutoAiming = true;
//        //    //    permanentWall.SetActive(true);

//        //    //    ClearTheArray();

//        //    //    for (int i = 0; i < 3; i++)
//        //    //    {
//        //    //        SpawnSimpleShootEnemy(5, spawnerPoints[5].position, Colors.Red);
//        //    //    }
//        //    //    for (int i = 0; i < 2; i++)
//        //    //    {
//        //    //        SpawnMeleeAttackEnemey(6, spawnerPoints[6].position, Colors.Red);
//        //    //    }

//        //    //    justOnce[phase] = false;
//        //    //}
//        //}
//        //else if (phase == 2)//green wall phase
//        //{
//        //    //if (justOnce[phase] == true)
//        //    //{
//        //    //    //isItTriggerActive = true;
//        //    //    ClearTheArray();

//        //    //    isItAutoAiming = false;
//        //    //    invisibleWallTop.SetActive(false);

//        //    //    SpawnSimpleShootEnemy(7, spawnerPoints[7].position, Colors.Blue);

//        //    //    justOnce[phase] = false;
//        //    //}
//        //}
//        //else if (phase == 3)//mini boss phase
//        //{
//        //    //if (justOnce[phase] == true)
//        //    //{
//        //    //    ClearTheArray();

//        //    //    invisibleWallTop.SetActive(true);
//        //    //    isItAutoAiming = true;
//        //    //    SpawnMiniBoss(8, spawnerPoints[8].position, Colors.Blue);
//        //    //    //OnLoadPrefab(0, "Green/EnemyCharge");
//        //    //    //SpawnEnemy(0, spawnPos[0]);

//        //    //    justOnce[phase] = false;

//        //    //}
//        //}
//        //else if (phase == 4)// embuscade phase
//        //{
//        //    //if (justOnce[phase] == true)
//        //    //{
//        //    //    //isActivating = true;
//        //    //    ClearTheArray();
//        //    //    isItAutoAiming = false;
//        //    //    invisibleWallBottom.SetActive(false);
//        //    //    invisibleWallTop.SetActive(false);
//        //    //    prefabs[0] = null;

//        //    //    SpawnSimpleShootEnemy(9, spawnerPoints[9].position, Colors.Blue);
//        //    //    SpawnSimpleShootEnemy(10, spawnerPoints[10].position, Colors.Blue);
//        //    //    SpawnSimpleShootEnemy(11, spawnerPoints[11].position, Colors.Blue);
              
//        //    //    justOnce[phase] = false;

//        //    //}
//        //}
//        //else if (phase == 5)//two melee attack npc phase
//        //{
//        //    //if (justOnce[phase] == true)
//        //    //{
//        //    //    ClearTheArray();
//        //    //    isItAutoAiming = true;

//        //    //    for (int i = 0; i < 2; i++)
//        //    //    {
//        //    //        SpawnMeleeAttackEnemey(12, spawnerPoints[12].position, Colors.Blue);
//        //    //    }

//        //    //    justOnce[phase] = false;
//        //    //}
//        //}
//        //else if (phase == 6)// last phase
//        //{
//        //    //if (justOnce[phase] == true)
//        //    //{
//        //    //    ClearTheArray();
//        //    //    isItAutoAiming = true;

//        //    //    for (int i = 0; i < 2; i++)
//        //    //    {
//        //    //        SpawnMeleeAttackEnemey(13, spawnerPoints[13].position, Colors.Blue);
//        //    //    }
//        //    //    for (int j = 0; j < 3; j++)//needs meleeattack enemy to be complet
//        //    //    {
//        //    //        SpawnSimpleShootEnemy(14, spawnerPoints[14].position, Colors.Red);
//        //    //    }

//        //    //    justOnce[phase] = false;
//        //    //}
//        //}
//    }
//    protected virtual void OnLoadPrefab(int i,List<string> enemyName)
//    {


//        foreach (string name in enemyName)
//        {
//            prefabs[i]=Resources.Load<GameObject>("Prefabs/Characters/"+enemyName);
//        }

//    }
    
//    protected virtual void SpawnEnemy(int i,Vector2 pos)
//    {
//            ICharacter enemy = Instantiate(prefabs[i], pos, Quaternion.identity).GetComponent<ICharacter>();
//        //if (isItAutoAiming != false)
//        //{
//            gm.AddEnemy(enemy);
//        //}
//            enemy.SetColor(enemyColor);
            
//            enemy.Init();
            
//            //enemy.ActivateAutoAim = isItAutoAiming;
//            enemy.OnDeathEvent += OnEnemyDeath;
//            enemyArray[i] = enemy;
//            enemy.GetGameObject().name = "TutorialSoldier" + enemyCount.ToString();// the name of the enemy and the name of Json file wich containes the lines of the enemy are the same
//            enemyCount++;

//            enemy.GetGameObject().GetComponent<BoxCollider2D>().size = enemyColliderScale;
//            enemy.GetGameObject().transform.localScale = enemyScale;
//            enemy.GetStats().SetSpeed(enemySpeed);// needed for the enemies in Level1 need also to be set in the inspector to get the hp bar update
//            enemy.GetStats().SetAttack(enemyAttack);
//            enemy.GetStats().SetMaxHP(enemyMaxHP);
//            enemy.GetStats().SetHP(enemyHP);
//            enemy.GetStateUI().Refresh();

//        //enemyArray[i].GetGameObject().GetComponent<Ranged1>().ShowDialogText();
//    }

//    public  virtual void OnEnemyDeath(ICharacter character)
//    {
//        character.OnDeathEvent -= OnEnemyDeath;
//        if (isItAutoAiming == true)
//        {
//            enemyDeadCount++;
//        }
//        else
//        {
//            return;
//        }
//    }

//    void CheckIfTutorialShouldBeDisplay()
//    {
//        if (phase == 2 || phase == 4)
//        {
//            EnableColorFingerTutorial();
//        }

//        else if (phase == 6)
//        {
//            // active tutorial finger acording to the player pos
//            if (player.transform.position.y >= 133)
//            {
//                EnableLittleFingerTargetTutorial();
//                //disable it after 3 second
//                timerDisable += Time.deltaTime;
//                if (timerDisable > 3)
//                {
//                    DisableLittleFingerTargetTutorial();
//                }
//            }
//        }
//    }

//    void EnableBulletColorFingerTutorial()
//    {
//        tutorialSigns[2].SetActive(true);
//    }
//    void DisableBulletColorFingerTutorial()
//    {
//        tutorialSigns[2].SetActive(false);
//    }
//    void EnableLittleFingerTargetTutorial()
//    {
//        tutorialSigns[1].SetActive(true);
//    }
//    void DisableLittleFingerTargetTutorial()
//    {
//        tutorialSigns[1].SetActive(false);
//    }
//    void EnableColorFingerTutorial()
//    {
//        tutorialSigns[0].SetActive(true);
//    }
//    void DisableColorFingerTutorial()
//    {
//        tutorialSigns[0].SetActive(false);
//    }

//    void SpawnNPC(int i, Vector3 pos, Colors enemyCol)
//    {
//        enemyColor = enemyCol;
//        enemyMaxHP = 10;
//        enemyHP = 10;
//        enemyAttack = 1;
//        enemySpeed = 0.0f;
//        OnLoadPrefab(i, "NPCTutorial");
//        SpawnEnemy(i, pos);
//    }

//    void SpawnSimpleShootEnemy(int i, Vector3 pos, Colors enemyCol)
//    {
//        enemyColor = enemyCol;
//        enemyMaxHP = 10;
//        enemyHP = 10;
//        enemyAttack = 1;

//        enemySpeed = 0.0f;
        
//        OnLoadPrefab(i, "Ranged1_R_RGO");
//        SpawnEnemy(i, pos);

//        //momentanly disable for because other dialogs are not set on other npc
//    //enemyArray[i].GetGameObject().GetComponent<Ranged1>().jsonFileDialogName =
//    //                    enemyArray[i].GetGameObject().name;
//    //    enemyArray[i].GetGameObject().GetComponent<Ranged1>().ActivateDialogFromMapManager();

//    }

//    void SpawnMeleeAttackEnemey(int i, Vector3 pos, Colors enemyCol)
//    {
//        enemyColor = enemyCol;
//        enemyMaxHP = 10;
//        enemyHP = 10;
//        enemySpeed = 1.0f;
//        enemyAttack = 1;

//        OnLoadPrefab(i, "MeleeAttackPathFinding");
//        SpawnEnemy(i, pos);
//    }
//    void SpawnMiniBoss(int i, Vector3 pos, Colors enemyCol)
//    {
//        enemyColor = enemyCol;
//        enemyMaxHP = 50;
//        enemyHP = 50;
//        enemySpeed = 1.0f;
//        enemyAttack = 2;

//        OnLoadPrefab(i, "Green/EnemyCharge");
//        SpawnEnemy(i, pos);
//    }

//    public virtual void Reset()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//    }
//}
