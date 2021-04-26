using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MiniStageExit : Interactable
{
    //tutorial
    public GameObject[] tutorialEnterPoints;

    GameObject phaseControllerTrigger;
    GameObject playerObj;
    GameObject transitionBlackScreen;
    Animator transitionBlackScreenAnimator;
    CameraControl camControl;

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
                i = GameObject.Find("Essential").transform.Find("MapManager").GetComponent<MapManager>().
                        phase - 2;
            }
        }   
    }
    IEnumerator TransitionTime(float duration)
    {
        float timer = 0.0f;
        while (timer < duration+5.0f)
        {
            if (timer >= duration)
            {
                playerObj.transform.position =
                phaseControllerTrigger.transform.position - new Vector3(0.0f, 4.0f, 0.0f);
            }
            timer += Time.deltaTime;
        }
        yield return null;
    }
}
