using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System.Linq;
using System;

public class GameManager : MonoBehaviour {

    public event Action<Colors> WorldColorChange;
    
    // Get this from the inspector just for convinience
    public GameObject enemy1Prefab;
    public Color characterRed;
    public Color bulletRed;
    public Color characterBlue;
    public Color bulletBlue;
    public Color worldGreen;
    public Color characterGreen;
    public Color bulletGreen;
    public Color worldOrange;
    public Color bulletOrange;
    public Color characterOrange;
    public Color worldBrown;
    public Color bulletBrown;
    public Color characterBlack;
    public Color characterPink;

    // To be moved to a ui manager later
    public GameObject menuPanel;
    public Camera cam;// public yohan added
    public Player player;//yohan added public
    public bool showDamageText;
    public bool showDialogText = true;//yohan addded
    public Colors WorldColor { get; private set; }

    // List holding all the active enemies so we can check who is closer
    public List<ICharacter> ActiveEnemies { get; private set; }
    public bool hasEnemies;
    public AudioManager audioManager;

    CameraControl cameraControl;
    bool showingMenu;
    // Keeping track of the color index so we dont get an array index error
    int colorIndex=1;
    int changeColorTriggerIndex;

    void Awake(){
        Static.worldColors = new Dictionary<Colors, Color>();
        //Static.worldColors.Add(Colors.Green,worldGreen);
        Static.worldColors.Add(Colors.Orange,worldOrange);
        Static.worldColors.Add(Colors.Brown,worldBrown);

        Static.bulletColors = new Dictionary<Colors, Color>();
        Static.bulletColors.Add(Colors.Red,bulletRed);
        Static.bulletColors.Add(Colors.Blue,bulletBlue);
        Static.bulletColors.Add(Colors.Green,bulletGreen);
        Static.bulletColors.Add(Colors.Orange,bulletOrange);
        Static.bulletColors.Add(Colors.Brown,bulletBrown);

        Static.characterColors = new Dictionary<Colors, Color>();
        Static.characterColors.Add(Colors.Red,characterRed);
        Static.characterColors.Add(Colors.Blue,characterBlue);
        Static.characterColors.Add(Colors.Green,characterGreen);
        Static.characterColors.Add(Colors.Black,characterBlack);
        Static.characterColors.Add(Colors.Pink,characterPink);
        Static.characterColors.Add(Colors.Orange,characterOrange);
        // Initialize the enemy list
        ActiveEnemies = new List<ICharacter>();
    }
    
    void Start(){
        cam = Camera.main;
        // Set the color to the first one
        SetColor(Colors.Brown);
        // Spawn a couple bad guys
        if (hasEnemies){
            for(int i=0; i<3; i++){
                Vector3 pos = new Vector3(i-1, 1+i*1.5f, 0);
                ICharacter enemy = Instantiate(enemy1Prefab, pos, Quaternion.identity).GetComponent<ICharacter>();
                enemy.Init();
                AddEnemy(enemy);
            }
        }
        player = GameObject.Find("Player").GetComponent<Player>();
        player.Init();
        cameraControl = GameObject.Find("MainCamera").GetComponent<CameraControl>();

        if (player.initiated)
        {
            cameraControl.SetTarget(player);
        }
        audioManager = GameObject.Find("Essential").
            transform.Find("AudioManager").gameObject.GetComponent<AudioManager>();
    }

   
    public void ActiveChangeColor()
    {
        if (changeColorTriggerIndex == 0)
        {
            SetColor(Colors.Orange);
            changeColorTriggerIndex++;
        }
        else if (changeColorTriggerIndex == 1)
        {
            SetColor(Colors.Brown);
            changeColorTriggerIndex--;
        }
    }

    // Switches colors in order
    public void NextColor(int dir){
        colorIndex += dir;
        if(colorIndex >= Static.worldColors.Count) colorIndex = 0;
        if(colorIndex < 0) colorIndex = Static.worldColors.Count-1;
        SetColor(Static.worldColors.ElementAt(colorIndex).Key);
    }

    // Switches to a specific color
    public void SetColor(Colors color){
        cam.backgroundColor = Static.worldColors[color];
        WorldColor = color;
        if(WorldColorChange!=null) WorldColorChange(color);
    }

    public void AddEnemy(ICharacter enemy){
        ActiveEnemies.Add(enemy);
        enemy.OnDeathEvent+=RemoveEnemy;
    }

    public void RemoveEnemy(ICharacter enemy){
        enemy.OnDeathEvent-=RemoveEnemy;
        player.OnEnemyDeath(enemy);
        ActiveEnemies.Remove(enemy);
    }
}
