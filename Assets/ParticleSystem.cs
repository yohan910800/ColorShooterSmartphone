using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem : MonoBehaviour
{
    float timer;
    float duration;

   
    
    void Start()
    {
        timer = 0;
        duration = 0.2f;


    }

    // Update is called once per frame
    void Update()
    {
        if (timer > duration)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
    }
}
