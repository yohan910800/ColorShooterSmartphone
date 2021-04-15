using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MankindGames;
using TMPro;
using UnityEngine.SceneManagement;

public class GameFinishController : MonoBehaviour
{
    GameManager gm;
    MapManager mapManager;
    public TextMeshProUGUI stageCredits;
    public TextMeshProUGUI stageLifes;
    public TextMeshProUGUI stageName;
    public TextMeshProUGUI areaCleared;
    int areaClearedIndex;
    int maxAreaIndex;
    float timer;
    float delayAmount;
    float creditValue;
    float maxCreditValue;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        mapManager = GameObject.Find("Essential").gameObject.transform.Find("MapManager").GetComponent<MapManager>();
        areaClearedIndex = mapManager.phase;
        maxAreaIndex = mapManager.maxAreaIndex;
        delayAmount = 0.3f;

        stageName.text = SceneManager.GetActiveScene().name;

        gm.audioManager.Pause("MainBgm");
        gm.audioManager.Pause("BossBgm");
        gm.audioManager.Play("GameFinishBgm");
        timer= - 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        maxCreditValue = gm.player.GetInventory().Credits;

        areaCleared.text = "Area cleared : " + areaClearedIndex + "/" + maxAreaIndex;
            
        timer += Time.deltaTime ;

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
    }
}
