using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MankindGames;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject menuPanel;

    public string nextStage; //needs to be saved

    bool showingMenu;
    Inventory inventory;
    public TextMeshProUGUI coinsTxt;
    public TextMeshProUGUI lifeTxt;
    public TextMeshProUGUI stageNameTxt;
    public RawImage video;
    public string stageName;
    int currentCoin;
    int currentLife;

    void Start()
    {
        nextStage = "Tutorial";//tmp
        //SetCoinsAndLifeText();
        //DisplayTheNameOfTheNextStage();
        DisplayTheVideoOfTheNextStage();
    
    }
    
    void SetCoinsAndLifeText()
    {
        currentCoin = inventory.Credits;
        currentLife = inventory.Life;
        coinsTxt.text = currentCoin.ToString();
        lifeTxt.text = currentLife.ToString();
        //to do with kim
        //need a load for 
    }

    void DisplayTheNameOfTheNextStage()
    {
        stageNameTxt.text = stageName;
        //to do with kim
    }
    void DisplayTheVideoOfTheNextStage()
    {
        video.texture = Resources.Load<Texture>("Texture For Videos/video");
        //to do with kim
    }

   

    public void Play()
    {
        SceneManager.LoadScene(nextStage);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
