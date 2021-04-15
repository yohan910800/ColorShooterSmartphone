using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class TimerBeforStopGame : MonoBehaviour
{
    float timerBeforeGoBackToHub;
    TextMeshProUGUI txt;
    void Start()
    {
        timerBeforeGoBackToHub = 5.0f;
        txt = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        timerBeforeGoBackToHub -= Time.deltaTime /** 90*/;
        txt.text = /*Mathf.Round(*/"" + (int)timerBeforeGoBackToHub;
        if (timerBeforeGoBackToHub <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
