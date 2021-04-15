using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class TentacleCollider : MonoBehaviour
{

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 0)
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<CircleCollider2D>());
        }
        if (collision.gameObject.layer == 14)
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<CircleCollider2D>());
        }
        //Log.log("collide "+collision.gameObject.layer);
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 12)
        {
            gameObject.transform.parent.GetComponent<BulletTentacle>().OnWallPosition = true;

            GetComponent<CircleCollider2D>().enabled = false;
            //Log.log("name " + gameObject.transform.parent.name);
        }
        else if (collision.gameObject.tag == "Player")
        {
            //GetComponent<CircleCollider2D>().enabled = false;
            Physics2D.IgnoreCollision(collision.collider, GetComponent<CircleCollider2D>());

        }
    }
}
