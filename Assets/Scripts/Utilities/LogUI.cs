using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogUI : MonoBehaviour
{
    Text txt;
    float time;
    bool isShowing;
    public float lifeTime = 3.0f;
    public static LogUI Instance {get; private set;}

    // Start is called before the first frame update
    void Awake(){
        Instance = this;
        txt = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
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
