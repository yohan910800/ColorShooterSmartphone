using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MankindGames;
public class CoindGoesToPlayer : Interactable
{

    GameObject playerObj;
    int creditAmount;
    bool justOnceDisableAnimator;
    GameManager gm;
    // Start is called before the first frame update
    protected override void Start()
    {
        gm = GameObject.Find("Essential").transform.Find("GameManager").GetComponent<GameManager>();
        playerObj = GameObject.Find("Player");
        StartCoroutine(GoToPLayer(3.0f));
        switch (SceneManager.GetActiveScene().name) {

            case "EnemyTest":
                creditAmount = 1;
                break;
            case "Tutorial":
                creditAmount = 1;
                break;
            case "Stage1":
                creditAmount = 3;
                break;
            case "Stage2":
                creditAmount = 5;
                break;
            case "Stage3":
                creditAmount = 6;
                break;
            case "Stage4":
                creditAmount = 7;
                break;
            case "Stage5":
                creditAmount = 8;
                break;
        }
    }

    IEnumerator GoToPLayer(float duration)
    {
        float timer = 0.0f;

        while (timer <=duration + 10)
        {
            if (timer >= duration)
            {
                if (justOnceDisableAnimator == false)
                {
                    gameObject.transform.parent.GetComponent<Animator>().enabled = false;
                    justOnceDisableAnimator = true;
                }
                    transform.position += (playerObj.transform.position-
                    transform.position ) *Time.deltaTime*10.0f;
            }
            else
            {
            }
            timer += Time.deltaTime;
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
           
            playerObj.GetComponent<ICharacter>().GetInventory().AddCredits(creditAmount);
            playerObj.GetComponent<Player>().GetUpdateCredits();//addedYohan;
            gm.audioManager.Play("GetCoin");
            Destroy(gameObject);
        }
    }
}
