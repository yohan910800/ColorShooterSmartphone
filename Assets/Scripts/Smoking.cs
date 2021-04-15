using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using MankindGames;
public class Smoking : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject playerObj;
    Animator animator;
   public  Light2D musularLight;
    bool justOnce;

    void Start()
    {
        playerObj =GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float dist=Vector3.Distance(transform.position,playerObj.transform.position);
        if (dist < 5.0f)
        {
            if (justOnce == false)
            {
                StartCoroutine(Bright(4.0f));
                animator.SetTrigger("TearSmokingTrigger");
                
                Destroy(gameObject, 15.0f);
                justOnce = true;
            }
        }
        
    }
    IEnumerator Bright(float duration)
    {
        float timer = 0.0f;
        while (timer < duration+11.0f)
        {
            if (timer > duration-3.0f)
            {
                GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
                GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
                GetComponent<Animator>().enabled = false;
                if (timer < duration + 2.0f)
                {
                    Log.log("UUUUUUUPPPP");
                    musularLight.intensity += 1 * Time.deltaTime * 5.0f;
                    musularLight.pointLightOuterRadius += 1f * Time.deltaTime * 7.0f;
                }
                else if (timer > duration + 4.0f)
                {
                    Log.log("DOOOOOOOWWWWWNNNN");
                    musularLight.intensity -= 1 * Time.deltaTime * 3.3f;
                    musularLight.pointLightOuterRadius -= 1f * Time.deltaTime * 5.0f;
                }
            }
            timer += Time.deltaTime;
            yield return null;
        }

    }
}
