//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using UnityEngine.Tilemaps;
//using MankindGames;

//public class Level5MapManager : MapManager
//{
//    public GameObject[] doorGroup;
//    ICharacter boss;

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
//        if (gm.player.transform.position.y >= phaseLimitsPos[0].position.y)
//        {
//            phase = 1;

//            if (justOnce[phase] == true)
//            {
                
//                SpawnBoss(1, spawnerPoints[0].position, "Blue/FourHandsBoss");
//                OpenDoor(0);
//                justOnce[phase] = false;
//            }

//            if (boss.GetStats().HP <= 0)
//            {
//                OpenDoor(1);
//            }
//        }
//        if (gm.player.transform.position.y >= phaseLimitsPos[1].position.y)
//        {
//            phase = 2;

//            if (justOnce[phase] == true)
//            {

//                SpawnBoss(2, spawnerPoints[1].position, "Blue/OneHandBoss");
//                OpenDoor(2);
//                justOnce[phase] = false;
//            }

//            if (boss.GetStats().HP <= 0)
//            {
//                OpenDoor(3);
//            }

//        }
//        if (gm.player.transform.position.y >= phaseLimitsPos[2].position.y)
//        {
//            phase = 3;

//            if (justOnce[phase] == true)
//            {

//                SpawnBoss(3, spawnerPoints[2].position, "Blue/NoHandBoss");
//                OpenDoor(4);
//                justOnce[phase] = false;
//            }

//            if (boss.GetStats().HP <= 0)
//            {
//                OpenDoor(5);
//            }
//        }
//        if (gm.player.transform.position.y >= phaseLimitsPos[3].position.y)
//        {
//            phase = 4;

//            if (justOnce[phase] == true)
//            {
//                SpawnBoss(4, spawnerPoints[3].position, "Blue/LastBoss");
//                OpenDoor(6);
//                justOnce[phase] = false;
//                disableForTest();
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

//    void SpawnBoss(int i, Vector3 pos,string name)
//    {
//        enemyHP = 3000;
//        enemyMaxHP = 3000;
//        enemyAttack = 1;
//        //enemySpeed = 5;

//        enemyColor = Colors.Blue;
//        OnLoadPrefab(i, name);
//        SpawnEnemy(i, pos);
//        boss = enemyArray[i];
//    }

//    void OpenDoor(int i)
//    {
//        doorGroup[i].SetActive(false);
//    }
//    void disableForTest()
//    {
//        // for optimisation
//        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
//        {
            
//            if (obj.transform.position.y < 130.0f && obj.name.Contains("Tutorial") == true )//disable walls
//            {
//                obj.SetActive(false);
//            }
//        }
//    }
    
//}
