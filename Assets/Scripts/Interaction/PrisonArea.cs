using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class PrisonArea : Interactable
{
    public GameObject[] emprisonedEnemies;

    BoxCollider2D boxCol;
    
    int count;
    GameManager gm;

    void Start()
    {
        emprisonedEnemies =new GameObject[500];
        boxCol = GetComponent<BoxCollider2D>();
        gm=GameObject.Find("GameManager").
            GetComponent<GameManager>();
    }

    void Update()
    {
        MakeTheEnemyUnAimableIfInPrison();
    }

    void MakeTheEnemyUnAimableIfInPrison()
    {
        if (gm.WorldColor == Colors.Green)
        {
            boxCol.enabled = false;
            foreach (GameObject enemy in emprisonedEnemies)
            {
                if (enemy != null)
                {
                    enemy.tag = "Untagged";
                }
            }
        }
        else
        {
            boxCol.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            emprisonedEnemies[count]=collision.gameObject;
            count++;
            collision.gameObject.tag = "NotAimable";
        }
    }
     
        
     
}
