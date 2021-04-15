using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class AutoSaveData 
{
    
    public int phase;
    public int currenthp;
   
    public int weapon1index;
    public int weapon2index;
    public bool PoisonPowerUp;
    public bool MinePowerUp;
    public bool doubleShot;
    //public int deadcount;
    public int eng;
    public bool didplayeruseonelife;
    public int life;
    public int coin;
    public bool didThePlayerOverComeTheArea6;
    public bool justOnceTutorialRandomBtn;
    public int tierIndex;
    public bool meleeBtnPressedForTheFirsTime;

    public AutoSaveData(int phas,int weap1index,int weap2index,bool Poison,
        bool Mine,bool dshot/*,int deadc*/,int energy,int curhp,
        bool ifusedlife,int lifeinventory,int coininventory,
        bool overComeTheArea6Condition,bool joTutorialrndBtnCondition,int tIndex,
       bool meleePressedFirstTime)
    {
        //player init
        currenthp = curhp;
        eng = energy;
        didplayeruseonelife = ifusedlife;

        //playercombat or player(temp
        MinePowerUp = Mine;

        //weapon init
        PoisonPowerUp = Poison;
        doubleShot = dshot;
        weapon1index = weap1index;
        weapon2index = weap2index;
        
        //mapmanager
        phase =phas;
        //deadcount = deadc;
       
        //inventory
        life = lifeinventory;
        coin = coininventory;

        //stateui condition
        didThePlayerOverComeTheArea6 = overComeTheArea6Condition;
        justOnceTutorialRandomBtn = joTutorialrndBtnCondition;

        tierIndex = tIndex;
        meleeBtnPressedForTheFirsTime = meleePressedFirstTime;


    }
}
