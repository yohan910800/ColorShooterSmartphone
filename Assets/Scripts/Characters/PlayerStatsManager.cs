using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{

    /*
    Note 

    this script is for hub scene.
    it can set player stats.
    upgrade methods are linked to canvas/stats panel/ stats(attack etc)/ stats btn.
    
    
    */
    Stats stats;

    private void Start()
    {
        load();
    }
    public void UpgradeHP(int amount)
    {
        if (stats.HP < 100)
        {
            stats.SetMaxHP(100 + amount);

        }
        else
        {
            stats.SetMaxHP(stats.HP + amount);
            stats.SetHP(stats.maxHP);
        }

        SaveSystem.SaveStats(stats);
    }
    public void UpgradeAttack(int amount)
    {
        stats.SetAttack(stats.Attack + amount);
        SaveSystem.SaveStats(stats);
    }
    public void UpgradeDefence(int amount)
    {
        stats.SetDefence(stats.Defence + amount);
        SaveSystem.SaveStats(stats);
    }
    public void UpgradeCriticalChance(int amount)
    {
        stats.SetCriticalChance(stats.CriticalChance + amount);
        SaveSystem.SaveStats(stats);
    }

    //for testing ↓＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
    public void resetStats()
    {
        stats.SetMaxHP(100);
        stats.SetHP(stats.maxHP);
        stats.SetAttack(0);
        stats.SetCriticalChance(0);
        stats.SetDefence(0);
        SaveSystem.SaveStats(stats);
    }
    public void save()
    {
        SaveSystem.SaveStats(stats);
    }
    public void load()
    {
        if (SaveSystem.LoadStats() != null)
        {
            stats = SaveSystem.LoadStats();
            Debug.Log(stats + "    SaveFile Found");

        }
        else
        {
            SaveSystem.SaveStats(stats = new Stats(0, 0, 0, 0, 0, 0));

            Debug.Log("saveFile Not Found. new stats file created");
        }
    }
}