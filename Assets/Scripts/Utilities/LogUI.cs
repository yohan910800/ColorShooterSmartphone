using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogUI : MonoBehaviour
{
    public float lifeTime = 3.0f;
    public static LogUI Instance { get; private set; }
    Text txt;
    float time;
    bool isShowing;
    
    void Awake(){
        Instance = this;
        txt = GetComponentInChildren<Text>();
    }
    void Update(){
        if(isShowing){
            time -= Time.unscaledDeltaTime;
            if(time<=0){
                isShowing = false;
                txt.text = "";
            }
        }
    }

    void Print(string str){
        txt.text = isShowing ? txt.text+=str : str;
        txt.text += "\n";
        isShowing = true;
        time = lifeTime;
    }

    public void Log(string str){
        Debug.Log(str);
        Print(str);
    }
}
