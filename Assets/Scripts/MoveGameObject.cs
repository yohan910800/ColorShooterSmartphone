using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
public class MoveGameObject : MonoBehaviour
{
    float speed;
    void Start()
    {
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {

        transform.localPosition -= new Vector3(2.0f, 0.0f) * Time.deltaTime * speed;
        if (transform.localPosition.x > 18.0f || transform.localPosition.x < -18.0f)
        {
            speed *= -1;

        }
            
    }
}
