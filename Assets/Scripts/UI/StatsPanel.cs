using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MankindGames;
using TMPro;

public class StatsPanel : MonoBehaviour
{

    /*
    this script manage canvas/statsPanel.
    we can add animation and stuff in here.
    
    */
    Stats stats;
    public TextMeshProUGUI attackValue;
    public TextMeshProUGUI defValue;
    public TextMeshProUGUI critValue;
    public TextMeshProUGUI hpValue;
    int price;
    int usedIndex;
    public TextMeshProUGUI priceTxt;
    public Animator AttackValueAnimator;
    public Animator HpValueAnimator;
    public Animator DefenceValueAnimator;
    public Animator CriticalValueAnimator;

void Start() {
    updateText();
    Debug.Log(SaveSystem.LoadStats());
        IncreaseUsedIndex();
        price = 100 * usedIndex;
        priceTxt.text = "x"+price.ToString();
        
    }

    public void updateText(){

        stats=SaveSystem.LoadStats();
        attackValue.GetComponent<TextMeshProUGUI>().text=stats.Attack.ToString();
        defValue.GetComponent<TextMeshProUGUI>().text=stats.Defence.ToString();
        critValue.GetComponent<TextMeshProUGUI>().text=stats.CriticalChance.ToString()+"%";
        hpValue.GetComponent<TextMeshProUGUI>().text=stats.HP.ToString();
        AttackValueAnimator.SetTrigger("AttackValueTrigger");
        HpValueAnimator.SetTrigger("HpValueTrigger");
        DefenceValueAnimator.SetTrigger("DefenceValueTrigger");
        CriticalValueAnimator.SetTrigger("CriticalValueTrigger");
    price = 100 * usedIndex;
        priceTxt.text = "x" + price.ToString();
    }
    public void IncreaseUsedIndex()
    {
        usedIndex++;
    }

    public void DecreasePlayerCoins()
    {
        // to do with kim
    }
    
}
