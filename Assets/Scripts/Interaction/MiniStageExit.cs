using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MiniStageExit : Interactable
{
    GameObject phaseControllerTrigger;
    GameObject playerObj;
    GameObject transitionBlackScreen;
    Animator transitionBlackScreenAnimator;
    CameraControl camControl;


    //tutorial
    public GameObject[] tutorialEnterPoints;
    int i;

    protected override void Start()
    {
        phaseControllerTrigger = GameObject.Find("PhaseControllerTrigger");
        transitionBlackScreen = GameObject.Find("TransitionAnimationUI").transform.GetChild(1).gameObject;
        transitionBlackScreenAnimator = transitionBlackScreen. GetComponent<Animator>();
        transitionBlackScreenAnimator.SetTrigger("TransitionAnimTrigger");
        camControl = GameObject.Find("MainCamera").GetComponent<CameraControl>();
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {

            playerObj=collision.gameObject;
            transitionBlackScreenAnimator.SetTrigger("TransitionAnimTrigger");
            StartCoroutine(TransitionTime(2.0f));
            camControl.SetTheCameraOnTargetPosInstantly();
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                //i++;
                i = GameObject.Find("Essential").transform.Find("MapManager").GetComponent<MapManager>().
                        phase - 2;
            }
        }   
    }
    private void Update()
    {
        //Debug.Log("mini stage i " + i);
    }
    IEnumerator TransitionTime(float duration)
    {
        float timer = 0.0f;
        while (timer < duration+5.0f)
        {
            if (timer >= duration)
            {
                //if (SceneManager.GetActiveScene().name != "Tutorial")
                //{
                    playerObj.transform.position =
                   phaseControllerTrigger.transform.position
                   -
                   new Vector3(0.0f, 4.0f, 0.0f);
                //}
                //else
                //{

                //    playerObj.transform.position =
                //   tutorialEnterPoints[i].transform.position -
                //   new Vector3(0.0f, 3.0f, 0.0f);
                //}
            }
            
            timer += Time.deltaTime;
        }
        yield return null;
    }
}
