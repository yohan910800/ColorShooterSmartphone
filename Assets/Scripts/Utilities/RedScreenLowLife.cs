using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedScreenLowLife : MonoBehaviour
{
    GameObject player;
    

    void Start()
    {

        player = GameObject.Find("Player");
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
         
        if (player.GetComponent<Player>().GetStats().HP <=30)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            transform.position = player.transform.position;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        
    }
}
