using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class ChangeColorZone : MonoBehaviour
{
    public PathFinderAI meleeEnemyScript;
    GameManager gm;
    GameObject particleObj;
    SpriteRenderer sprite;
    Collider2D col;

    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        particleObj = transform.GetChild(0).transform.gameObject;
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        
        if(Vector2.Distance(transform.position, gm.player.transform.position) < 10)
        {
            sprite.enabled = true;
            col.enabled = true;
            particleObj.SetActive(true);
        }
        else
        {
            sprite.enabled = false;
            col.enabled = false;
            particleObj.SetActive(false);
        }

    }
    void MeleEnemyReScanPath()
    {
        meleeEnemyScript.EnemyScanPath();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            gm.ActiveChangeColor();
            //MeleEnemyReScanPath();
        }
    }


  
}
