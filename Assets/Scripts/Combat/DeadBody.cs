using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : MonoBehaviour
{
    public float lifeTime;
    BoxCollider2D col;
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        Destroy(gameObject,lifeTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(col, collision.collider, true);
        }
    }

}
