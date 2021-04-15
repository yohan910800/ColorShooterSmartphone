using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using UnityEngine.UI;
public class TutorialUIController : Interactable
{
    MapManager mapManager;
    public GameObject UIelement;
    public GameObject phaseWalls;
    public GameObject useRndUIElement;
    public GameObject stopTimeUIElement;
    public GameObject miniStageExitObj;
    public GameObject miniStageExit;
    public GameObject closePhaseWall;
    List<GameObject> enemies = new List<GameObject>();

    protected override void Start()
    {
        base.Start();
        if (GameObject.Find("MapManager").GetComponent<MapManager>() != null)// edited_dohan- caused an error in redCityMap. This would fix it while un-effecting other scenes
        {
            mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        }
        miniStageExit = GameObject.Find("MiniStageExit");
    }

    private void Update()
    {
        //Log.log("array count " + mapManager.enemyArray.Count);
        //Log.log("enemy dead count" + mapManager.enemyDeadCount);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //player.GetStateUI().activeMeleeButtonForTutorial = true;
        if (UIelement.name != "KillenemiesTXT" && gameObject.name == "KillEnemyExplanetion")
        {
            UIelement.SetActive(true);
        }
        if(UIelement.name=="FingerColor"&& gameObject.name == "FingerColor")
        {
            //UIelement.SetActive(true);
        }
        if ( gameObject.name == "ChooseRNDWeaponExplanetion")
        {
            useRndUIElement.SetActive(true);
            UIelement.SetActive(true);
        }
        if (gameObject.name == "Red/GreyExplanetion")
        {
            UIelement.SetActive(true);
        }
        if (gameObject.name == "Blue/CyanExplanetion")
        {
            UIelement.SetActive(true);
        }

            //if (gameObject.name == "Blue/CyanExplanetion")
            //{
            //    GameObject.Find("TriggerTutoriialGroup"). transform.GetChild(2)
            //        .gameObject.SetActive(false);
            //}

    }
    void OnTriggerStay2D(Collider2D collision)
    {
        //Log.log("enemicount" + mapManager.enemyArray.Count);
        //Log.log("enemi dead count " + mapManager.enemyDeadCount);
        //Log.log("name of the trigger  " + gameObject.name);

        switch (gameObject.name)
        {
            case "KillEnemyExplanetion":

                if (mapManager.enemyDeadCount== 5 || mapManager.enemyDeadCount == 3)
                {
                    UIelement.SetActive(false);
                    gameObject.SetActive(false);
                    GameObject.Find("TriggerTutoriialGroup").transform.GetChild(2)
                        .gameObject.SetActive(true);
                }
                break;
            case "ChooseRNDWeaponExplanetion":

                if (collision.name == "Player")
                {
                    if (collision.GetComponent<ICharacter>().GetStats().Energy < 20.0f)
                    {
                        useRndUIElement.SetActive(false);
                        if (player.GetActiveWeapon().ToString() != "SingleShooter32x32")
                        {
                            //phaseWalls.SetActive(false);
                            miniStageExit.transform.position=
                                miniStageExitObj.transform.position;

                            useRndUIElement.SetActive(false);
                            UIelement.SetActive(false);
                        }
                        //UIelement.SetActive(false);
                        GameObject.Find("TriggerTutoriialGroup").transform.GetChild(7)
                       .gameObject.SetActive(true);
                        //gameObject.SetActive(false);
                    }
                }
                break;
            case "KillEnemyExplanetion2":

                if (mapManager.enemyArray.Count == mapManager.enemyDeadCount)
                {
                    //phaseWalls.SetActive(false);
                    miniStageExit.transform.position =
                                miniStageExitObj.transform.position;
                    UIelement.SetActive(false);
                }

                break;
            case "Blue/CyanExplanetion":
                //UIelement.SetActive(true);
                if (mapManager.phase >4)
                {
                    UIelement.SetActive(false);
                }
                break;
            case "FingerColorBlue/CyanZone":

                UIelement.SetActive(true);

                break;
           
            
            case "Red/GreyExplanetion":

                //UIelement.SetActive(true);
                if (mapManager.gm.WorldColor != Colors.Brown)
                {
                    //phaseWalls.SetActive(false);
                    miniStageExit.transform.position =
                                miniStageExitObj.transform.position;
                    UIelement.SetActive(false);
                }

                break;


            case "StopTimeTrigger":

                if (mapManager.gm.player.GetComponent<PlayerCombatV1>().isMeleeModeActivate == false)
                {
                    UIelement.SetActive(true);

                    Time.timeScale = 0.1f;
                }
                else
                {
                    UIelement.GetComponent<Image>().enabled = false;
                    if (mapManager.enemyArray.Count == mapManager.enemyDeadCount)
                    {
                        UIelement.transform.Find("Text (TMP)").gameObject.SetActive(false);
                        UIelement.transform.Find("Text (TMP) (1)").gameObject.SetActive(false);
                        UIelement.transform.Find("enemy pos").gameObject.SetActive(false);
                        UIelement.transform.Find("melee mode").gameObject.SetActive(false);
                        UIelement.transform.Find("Text (TMP) (4)").gameObject.SetActive(false);
                    }
                    Log.log("array count " + mapManager.enemyArray.Count);
                    Log.log("enemy dead count" + mapManager.enemyDeadCount);
                    Time.timeScale = 1;
                }

                break;

            case "KillEnemyExplanetion3":

                if (mapManager.enemyArray.Count == mapManager.enemyDeadCount)
                {

                    //stopTimeUIElement.SetActive(true);

                    //phaseWalls.SetActive(false);
                    miniStageExit.transform.position =
                                miniStageExitObj.transform.position;
                    UIelement.SetActive(false);
                    gameObject.SetActive(false);
                }

                break;
            case "KillEnemyExplanetion4":
                if (mapManager.enemyDeadCount == 26/*mapManager.enemyDeadCount*/||
                    mapManager.enemyDeadCount == 11|| mapManager.enemyDeadCount == 6)
                {
                    //phaseWalls.SetActive(false);

                    if (mapManager.enemyDeadCount != 0)
                    {
                        miniStageExit.transform.position =
                                    miniStageExitObj.transform.position;
                    }
                }
                else
                {
                    phaseWalls.SetActive(true);
                }
                break;
            case "KillEnemyExplanetion5":
                if (mapManager.enemyArray.Count == mapManager.enemyDeadCount)
                {
                    //phaseWalls.SetActive(false);
                    miniStageExit.transform.position =
                                miniStageExitObj.transform.position;
                }
                else
                {
                    phaseWalls.SetActive(true);

                }
                break;
            case "KillEnemyExplanetion6":
                if (mapManager.enemyArray.Count == mapManager.enemyDeadCount)
                {
                    //phaseWalls.SetActive(false);
                    miniStageExit.transform.position =
                                miniStageExitObj.transform.position;
                }
                else
                {
                    phaseWalls.SetActive(true);

                }
                break;
            case "KillEnemyExplanetion7":
                if (mapManager.enemyArray.Count == mapManager.enemyDeadCount)
                {
                    //phaseWalls.SetActive(false);
                    miniStageExit.transform.position =
                                miniStageExitObj.transform.position;
                }
                else
                {
                    phaseWalls.SetActive(true);

                }
                break;
            case "KillEnemyExplanetion8":
                if (mapManager.enemyArray.Count == mapManager.enemyDeadCount)
                {
                    //phaseWalls.SetActive(false);
                    miniStageExit.transform.position =
                                miniStageExitObj.transform.position;
                }
                else
                {
                    phaseWalls.SetActive(true);

                }
                break;
            case "KillEnemyExplanetion9":
                if (mapManager.enemyArray.Count == mapManager.enemyDeadCount)
                {
                    //phaseWalls.SetActive(false);
                    miniStageExit.transform.position =
                                miniStageExitObj.transform.position;
                }
                else
                {
                    phaseWalls.SetActive(true);

                }
                break;
        }

        if (UIelement.name == "FingerColorMeleeAttack")
        {
            if (mapManager.gm.player.GetStats().Energy == 100)
            {
                UIelement.SetActive(true);
            }
            else
            {
                UIelement.SetActive(false);
            }
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        switch (UIelement.name)
        {
            case "FingerColor":
                UIelement.SetActive(false);
                break;

            case "KillenemiesTXT":
                break;

            case "BlueCyanExplanationTXT":
                UIelement.SetActive(false);
                break;
            case "ChooseRNDWeaponExplanetion":
                UIelement.SetActive(false);
                useRndUIElement.SetActive(false);
                break;


            case "RedGreyExplanationTXT":
                if (mapManager.gm.WorldColor != Colors.Brown)
                {
                    UIelement.SetActive(false);
                }
                break;
        }

        if (UIelement.name == "KillenemiesTXT" && 
            gameObject.name == "KillEnemyExplanetion")
        {
            if (mapManager.enemyDeadCount == 6)
            {
                UIelement.SetActive(false);
            }

        }

        else if (UIelement.name == "KillenemiesTXT" 
            && gameObject.name == "KillEnemyExplanetion2")
        {
            if (mapManager.enemyDeadCount == 12)
             {
                UIelement.SetActive(false);
             }
        }
       
        else if (UIelement.name == "KillenemiesTXT" && gameObject.name == "KillEnemyExplanetion3")
        {
            if (mapManager.enemyDeadCount == 16)
            {
                UIelement.SetActive(false);
            }
        }
    }

    
}
