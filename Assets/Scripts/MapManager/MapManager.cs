using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using MankindGames;

public class MapManager : MonoBehaviour
{

    public int maxAreaIndex;
    public Transform[] spawnerPoints;//size defin in the inspectore
    public Transform[] phaseLimitsPos;//size defin in the inspectore

    public GameObject triggerZone;
    //tmp
    public Vector2 interactPos;// this is used to get the position of the interactible Trigger


    public List<ICharacter> enemyArray=new List<ICharacter>();//this is used to disable all enemie of the phase 1

    public GameObject[] prefabs;// get the prefabs wich instantiate through the game

    public int dialogZoneCounter;// it use in the "Interactible" script to decide wich NPc says wich line


    GameObject prefab;

    public GameManager gm;

    public Player player;


    public int phase = 0;// this is used to to decide wich enemy is loaded 

    //tmp maybe
    public float enemySpeed;
    public int enemyHP;
    public int enemyMaxHP;
    public int enemyAttack;
    public int enemyDropedEnergy;
    public int enemyDropedCredits;
    public Vector3 enemyScale;
    public Vector3 enemyColliderScale = new Vector3(0.9f, 0.9f);
    public Colors enemyColor;
    //cinematic

    //public GameObject playerDeadBody;
    //public GameObject[] playerMainBody;
    public GameObject diedAnimKindOfParticle;

    //

    public int enemyDeadCount;//this variable count the number of enemy dead in the map 
                              //public bool isItAutoAiming;// is used to check if the player should auto aim or not 
                              //int enemyCount; // to give a unique name to the enemy. used to give a unique line
    protected List<string> enemyNameList=new List<string>();

    int enemyCount;
    
    protected virtual void Start()
    {
        triggerZone = GameObject.Find("PhaseControllerTrigger");
        
        player = GameObject.Find("Player").GetComponent<Player>();

        phase = 0;
        SizeArrays();
        prefab = prefabs[0];

        //cinematic

        //playerDeadBody = GameObject.Find("Player").transform.Find("PlayerDeadBody").gameObject;
        //playerMainBody = new GameObject[6];

        //playerMainBody[0] = GameObject.Find("Player").transform.Find("Body").gameObject;
        //playerMainBody[1] = GameObject.Find("Player").transform.Find("Face").gameObject;
        //playerMainBody[2] = GameObject.Find("Player").transform.Find("Hand1").gameObject;
        //playerMainBody[3] = GameObject.Find("Player").transform.Find("Hand2").gameObject;
        //playerMainBody[4] = GameObject.Find("Player").transform.Find("Foot1").gameObject;
        //playerMainBody[5] = GameObject.Find("Player").transform.Find("Foot2").gameObject;

        diedAnimKindOfParticle = GameObject.Find("RelieveAnimation");

        StartCoroutine(DisableMovementForCinematic(4.0f));
    }
    public IEnumerator DisableMovementForCinematic(float duration)
    {
        float time = 0.0f;
        //bool justOnceBossName = false;
        //if (phase != 0)
        //{
        //    time = duration;
        //}
        while (time < duration + 2)
        {
            if (time < duration)
            {
                player.GetComponent<MovementV1>().enabled = false;
                player.GetComponent<PlayerCombatV1>().enabled = false;
                GameObject.Find("MainCamera").GetComponent<CameraControl>().enabled = false;
                GameObject.Find("MainCamera").GetComponent<Animator>().enabled = true;
                //if (justOnceBossName == false)
                //{

                //    justOnceBossName = true;
                //}
            }
            else
            {
                GameObject.Find("MainCamera").GetComponent<CameraControl>().enabled = true;

                GameObject.Find("MainCamera").GetComponent<Animator>().enabled = false;

                foreach (GameObject partOfBody in player.playerMainBody)
                {
                    partOfBody.SetActive(true);
                }
                player.playerDeadBody.SetActive(false);
                diedAnimKindOfParticle.SetActive(false);
                player.GetComponent<MovementV1>().enabled = true;
                player.GetComponent<PlayerCombatV1>().enabled = true;
            }
            time += Time.deltaTime;
            yield return null;
        }
    }
    void SizeArrays()
    {
        prefabs = new GameObject[200];
    }
     void Update()
    {
        Debug.Log("Current phase"+phase);
    }



    public virtual void ActivatePhase(int phase)//check in wich part of the tutorial we are 
    {

        switch (phase)
        {

            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;

        }

    }
    protected virtual void OnLoadPrefab(int i, string enemyName)
    {
        enemyNameList.Add(enemyName);
        //foreach (string name in enemyNameList)
        //{
            prefabs[i] = Resources.Load<GameObject>("Prefabs/Characters/" + enemyNameList[i]/*name*/);
        //}
    }

    protected virtual void SpawnEnemy(int i, Vector2 pos)
    {
        ICharacter enemy = Instantiate(prefabs[i], pos, Quaternion.identity).GetComponent<ICharacter>();
        gm.AddEnemy(enemy);
        enemy.SetColor(enemyColor);

        enemy.Init();

        enemy.OnDeathEvent += OnEnemyDeath;
        enemyArray.Add( enemy);
        enemy.GetGameObject().name = "TutorialSoldier" + enemyCount.ToString();// the name of the enemy and the name of Json file wich containes the lines of the enemy are the same
        enemyCount++;

        enemy.GetGameObject().GetComponent<BoxCollider2D>().size = enemyColliderScale;
        enemy.GetGameObject().transform.localScale = enemyScale;
        enemy.GetStats().SetBaseSpeed(enemySpeed);// needed for the enemies in Level1 need also to be set in the inspector to get the hp bar update
        enemy.GetStats().SetAttack(enemyAttack);
        enemy.GetStats().SetMaxHP(enemyMaxHP);
        enemy.GetStats().SetHP(enemyHP);
        enemy.GetDrops().SetEnergy(enemyDropedEnergy);
        enemy.GetDrops().SetCredits(enemyDropedCredits);
        enemy.GetStateUI().Refresh();

    }

    public virtual void MoveTriggerZoneToTheNextPosition(int positionIndex)
    {
        
    }
    public virtual void OnEnemyDeath(ICharacter character)
    {
        character.OnDeathEvent -= OnEnemyDeath;
        
            enemyDeadCount++;
    }

    public virtual void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
