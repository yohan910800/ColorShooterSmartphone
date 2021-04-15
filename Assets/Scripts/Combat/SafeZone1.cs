using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class SafeZone1 : MonoBehaviour
{

    //tmp for levle3
    public Player getPlayer;
    public int getPhase;
    float invincibilityTimer;
    bool startDestroy;
    float timer;

    void Start()
    {
        getPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        getPhase = GameObject.FindGameObjectWithTag("Boss").
            GetComponent<GRP4FusinedBossCombat>().phase;
    }

    void Update()
    {
        //Log.log("get phase" + getPhase);
        
        if (startDestroy == true)
        {
            timer += Time.deltaTime;
            if (timer > 8.5f)
            {
                Destroy(gameObject.transform.parent.gameObject);

                timer = 0.0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameObject.FindGameObjectWithTag("Boss") != null)
            {

                getPhase = GameObject.FindGameObjectWithTag("Boss").
                    GetComponent<GRP4FusinedBossCombat>().phase;
            }
            if (getPhase== 2)
            {
                getPlayer.isInvincible = true;
                startDestroy = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameObject.FindGameObjectWithTag("Boss") != null)
            {
                getPhase = GameObject.FindGameObjectWithTag("Boss").
               GetComponent<GRP4FusinedBossCombat>().phase;
                if (getPhase == 2)
                {
                    getPlayer.isInvincible = true;
                    startDestroy = true;
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        getPlayer.isInvincible = false;
    }
}
