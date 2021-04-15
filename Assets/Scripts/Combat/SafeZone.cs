using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class SafeZone : MonoBehaviour
{
    Player getPlayer;
    Ranged1 getBoss;
    float invincibilityTimer;
    void Start()
    {
        Destroy(gameObject, 11f);
        getPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //getBoss = GameObject.Find("LastBoss(Clone)").GetComponent<Ranged1>();
        getBoss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Ranged1>();
    }

    void Update()
    {
        if (getBoss != null)
        {
            if (getBoss.phase != 6)
            {
                Destroy(gameObject);
            }
            invincibilityTimer += Time.deltaTime;
            if (invincibilityTimer >= 10)
            {
                getPlayer.isInvincible = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.activeInHierarchy == true)
            {
                if (GameObject.FindGameObjectWithTag("UltimateBullet") != null)
                {
                    Log.log("SAFE");

                    getPlayer.isInvincible = true;
                    //GameObject.FindGameObjectWithTag("UltimateBullet").
                    //    GetComponent<CircleCollider2D>().enabled = false;
                    Log.log("OnStay" + getPlayer.isInvincible);
                    GameObject.FindGameObjectWithTag("UltimateBullet").
                    GetComponent<LastBossUltimateBullet>().hitInARow = false;
                }
            }
            else
            {
                //Log.log("Onexit" + getPlayer.isInvincible);
                getPlayer.isInvincible = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            getPlayer.isInvincible = false;
            GameObject.FindGameObjectWithTag("UltimateBullet").
                    GetComponent<CircleCollider2D>().enabled = true;
            Log.log("OnExit" + getPlayer.isInvincible);
        }
    }
}
