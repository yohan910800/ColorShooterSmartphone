using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class StickBullet : MonoBehaviour
{
    float time;
    OneHandBossCombat enemyCombatScript;

    private void Start()
    {
        enemyCombatScript = transform.parent.parent.parent.parent.GetComponent<OneHandBossCombat>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyCombatScript != null)
        {
            if (collision.tag == "Player")
            {
                //collision.GetComponent<Player>().GetHit(50);
                enemyCombatScript.isHiting = true;
            }
        }
        else
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Player>().GetHit(20);
            }
        }
    }
    private void Update()
    {
        transform.localPosition = new Vector3(0.71f,0,0);
        if (enemyCombatScript != null)
        {
            if (enemyCombatScript.isHiting == true)
            {
                time += Time.deltaTime;
                if (time > 3)
                {
                    enemyCombatScript.isHiting = false;
                    time = 0;
                }
            }
        }

        else return;

        
    }
}
