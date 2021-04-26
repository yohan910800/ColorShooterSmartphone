using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AddManager : MonoBehaviour
{
    /*will be activated after the save system is done*/

    private string store_id = /*"3921875"*/"3913601";

    //void Awake()

    //{

    //    Advertisement.Initialize(store_id, true);

    //}
    void Start()

    {

        //Advertisement.Initialize(store_id, true);

    }

    //  public void DisplayVideoAdd()
    //{
    //      if (Advertisement.IsReady("video"))
    //      {
    //          Advertisement.Show("video");
    //      }
    //  }

    private void Update()
    {
        
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if (Advertisement.IsReady("video"))
        //    {
        //        Advertisement.Show("video");
        //    }
        //    else
        //    {
        //        //Debug.Log("000000000");
        //    }
        //    //Debug.Log("add");
        //    //DisplayVideoAdd();
        //}
    }
}
