using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightIntensityController : MonoBehaviour
{
    public GameObject globalLightObj;
    Light2D globalLight;
    float intensity;
    void Start()
    {
        globalLight = globalLightObj.GetComponent<Light2D>();
        intensity = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        globalLight.intensity += (intensity*0.005f);
        if (globalLight.intensity > 1 || globalLight.intensity < 0.5f)
        {
            intensity *=-1;
        }
        
        
    }
}
