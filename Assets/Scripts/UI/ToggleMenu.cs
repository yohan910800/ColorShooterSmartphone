using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MankindGames;
using TMPro;

public class ToggleMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject MiddleGroup;
    bool showingMenu;


    public void ToggleMenus()
    {
        showingMenu = !showingMenu;
        menuPanel.SetActive(showingMenu);
        MiddleGroup.SetActive(!showingMenu);
    }

    public void ResetCondition()
    {
        showingMenu = false;
    }
}
