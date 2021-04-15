using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MankindGames;
using TMPro;
using UnityEngine.SceneManagement;

public class StageClearController : MonoBehaviour
{
    GameManager gm;
    public TextMeshProUGUI stageCredits;
    public TextMeshProUGUI stageName;
    float timer;
    float delayAmount;
    float creditValue;
    float maxCreditValue;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        delayAmount = 0.3f;
        gm.audioManager.Pause("MainBgm");
        gm.audioManager.Pause("BossBgm");
        gm.audioManager.Play("GameFinishBgm");
        
    }

    void Update()
    {
            maxCreditValue = gm.player.GetInventory().Credits;
            timer += Time.deltaTime * 5.0f;

            if (timer >= delayAmount)
            {
                if (creditValue < maxCreditValue)
                {
                    gm.audioManager.Play("Coin1");
                    creditValue++;
                    stageCredits.text = "x " + creditValue.ToString();
                    timer = 0f;
                }
            }
        stageName.text = SceneManager.
            GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex+1).name + " unlock !";
    }
}
