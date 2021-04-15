using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using UnityEngine.SceneManagement;
//added_dohan------------------
using System.Linq;

public class SaveSystemManager : MonoBehaviour
{
    public GameObject MapManagerObj;
    public GameObject playerObj;
    ICharacter character;
    AutoSaveData autoSaveData;
    HubSaveData hubSaveData;
    PlayerCombatV1 playerComb;


    public void InitSaveData()
    {

        playerComb = playerObj.GetComponent<PlayerCombatV1>();
        character = playerObj.GetComponent<ICharacter>();


        if (File.Exists(Application.persistentDataPath + "/playerSavedata.test"/*Application.dataPath + "/savedata/HubData.test"*/))
        {
            hubSaveData = SaveSystem.Loadhubsavedata();
            character.GetInventory().SetCredits(hubSaveData.credits);
            Debug.Log("HubDataLoaded" + hubSaveData.credits);

        }
        else
        {
            hubSaveData = new HubSaveData(10);
            character.GetInventory().SetCredits(hubSaveData.credits);
        }

        if (File.Exists(Application.persistentDataPath + "/playerSavedata.test"/*Application.dataPath + "/savedata/autoSaveData.test"*/))
        {

            LoadAndSetPlayer(SaveSystem.LoadAutoSaveData());

        }
        else { CreateAutoSaveData(); }

        playerComb.weapons[0] = character.GetActiveWeapon();
    }
    private void Update()
    {
        //Log.log("data " + data.phase);
        Log.log("just once tutorial rnd btn " + character.GetGameObject().
            GetComponent<Player>().justOnceTutorialRandomBtn);
    }
    public void OnHitSaveData(Collider2D col)
    {
        SetAndSaveData(col);

    }

    void SetAndSaveData(Collider2D col)
    {
        // Debug.Log(col.transform.position + "" + autoSaveData + " " + stats.Energy);
        AutoSaveData data = new AutoSaveData(
            MapManagerObj.GetComponent<MapManager>().phase,
            getWeaponIndex(playerComb.weapons[0]),
            getWeaponIndex(playerComb.weapons[1]),
            playerComb.weapons[0].doesItHavePoisonBullet
            ,
            playerComb.doesBonusMineIsActive,
            playerComb.weapons[0].doesItHaveDoubleShot,
             //MapManagerObj.GetComponent<MapManager>().enemyDeadCount
             character.GetStats().Energy
            , character.GetStats().HP, character.GetStateUI().doesPlayerAlreayUseOneLife
            ,
            character.GetInventory().Life,
            character.GetInventory().Credits

            , character.GetGameObject().GetComponent<Player>().didThePlayerOverComeTheArea6
            , character.GetGameObject().GetComponent<Player>().justOnceTutorialRandomBtn
            , character.GetGameObject().
            GetComponent<Player>().tierIndex,
            character.GetGameObject().GetComponent<Player>().meleeBtnPressedForTheFirsTime
            );

        SaveSystem.SaveAutoSaveData(data);
        hubSaveData.credits = character.GetInventory().Credits;//temp
        SaveSystem.Savehubsavedata(hubSaveData);//temp
                                                // Debug.Log("auto save point reached. autosaved. phase=" + data.phase);
    }

    void LoadAndSetPlayer(AutoSaveData data)
    {
        //1. set player position DONE

        GameObject spawnarea = GameObject.Find("playerSpawnphase" + (data.phase /*+ 1*/).ToString()); ;
        Log.log("data " + data.phase);
        character.GetGameObject().transform.position = spawnarea.transform.position;

        // set enemy deadcount DONE

        //MapManagerObj.GetComponent<MapManager>().enemyDeadCount = data.deadcount;
        // Debug.Log("EnemyDeadCount="+data.deadcount);
        // Debug.Log(MapManagerObj.GetComponent<MapManager>().enemyDeadCount);

        // set weapons DONE
        Weapon unusedparameter = character.GetInventory().Weapons[1];
        Debug.Log("weapon1index=" + data.weapon1index);
        /*playerComb.OnChooseWeapon*/
        ActivateWeapon(data.weapon1index/*, unusedparameter*/);
        playerComb.OnChooseWeapon2(data.weapon2index, unusedparameter);

        // set powerups (mostly bools) DONE
        if (data.MinePowerUp)
        {
            playerComb.doesBonusMineIsActive = true;

        }
        if (data.PoisonPowerUp)
        {
            playerComb.weapons[0].doesItHavePoisonBullet = true;
        }
        if (data.doubleShot)
        {
            playerComb.weapons[0].doesItHaveDoubleShot = true;
        }

        character.GetStats().SetHP(SaveSystem.LoadAutoSaveData().currenthp);

        // set energy DONE
        //character.GetStats().setEnergy(data.eng);

        if (data.didThePlayerOverComeTheArea6)
        {
            character.GetGameObject().GetComponent<Player>().didThePlayerOverComeTheArea6 = true;
        }
        if (data.justOnceTutorialRandomBtn)
        {
            character.GetGameObject().GetComponent<Player>().justOnceTutorialRandomBtn = true;
        }

        character.GetGameObject().GetComponent<Player>().SetTierNum(data.tierIndex);
        //Debug.Log("Savefile energy=" + data.eng + "current energy=" + stats.Energy);
        character.GetStats().setEnergy(data.eng);

        character.GetGameObject().GetComponent<Player>().meleeBtnPressedForTheFirsTime = true;

        // Debug.Log("PlayerDataLoaded");
    }
    void ActivateWeapon(int index/*,Weapon weapon*/)
    {
        //weapon.Init(character, singleSocket);
        //playerScript.AddWeapon(weapon);
        character.ActivateWeapon(character.GetInventory().Weapons[index]);
    }
    void ActivateWeapon2(Weapon weapon2)
    {
        //weapon2.Init(character, singleSocket2);
        //playerScript.AddWeapon(weapon2);
        //playerScript.ActivateAllWeapons(weapon2);
    }
    private void CreateAutoSaveData()
    {
        autoSaveData = new AutoSaveData(0, 0, 0, false, false, false, /*0,*/ 0, SaveSystem.LoadStats().HP, false, 0, 0, false, false, 0, false);
        SaveSystem.SaveAutoSaveData(autoSaveData);
        //LoadAndSetPlayer(autoSaveData);
        Debug.Log("created new autosave data");
        //Debug.Log("save file doesn`t exist, created new savedata");
    }
    int getWeaponIndex(Weapon weapon)
    {

        Debug.Log("Weapon index compare start-----------------------");
        for (int x = 0; x < 9; x++)
        {
            if (weapon == character.GetInventory().Weapons[x])
            {
                Debug.Log("subject=" + weapon + "inventory weapon" + x + character.GetInventory().Weapons[x] + "Match!!!!!!!!!!!!!!!!");
                Debug.Log("Weapon index compare end-----------------------");
                return x;
            }

            Debug.Log("subject=" + weapon + "inventory weapon" + x + character.GetInventory().Weapons[x] + "No Match");
        }
        Debug.Log("can`t get weapons index");
        Debug.Log("Weapon index compare end-----------------------");
        return 0;
    }



    public void setStats(Stats newStats)
    {
        character.SetStats(newStats);

    }
}
