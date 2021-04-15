using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MankindGames;

public class FloatingDialogUI : MonoBehaviour
{
    
    // Start is called before the first frame update
    Vector2 playerPos;
    Player player;
    //MapManager mapManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        //transform.position = transform.position + new Vector3(0.0f, 2.0f, 0.0f);
        
    }

    void Update()
    {
        ScaleAcoordingToPlayerPos();
    }
    void ScaleAcoordingToPlayerPos()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);
        if (dist > 0.5f)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(2 + 1.0f / dist, 2 + 1.0f / dist);
            GetComponent<TextMeshPro>().fontSize = 4 + 2 / dist;
            gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector3(2 + 1.0f / dist, 2 + 1.0f / dist);
        }
    }
}
