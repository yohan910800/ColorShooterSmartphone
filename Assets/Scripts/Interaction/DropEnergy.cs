using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEnergy : MonoBehaviour
{
    float timer = 2.0f;
    // Update is called once per frame
    void Start()
    {
        Destroy(gameObject.transform.parent.gameObject,1.0f);
        gameObject.transform.parent.gameObject.transform.parent =
            GameObject.Find("Player").transform;
    }
}
